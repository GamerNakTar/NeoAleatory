using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private ScriptableStats stats;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private FrameInput _frameInput;
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;

    [Header("Control Keys")] public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode jumpKey;
    public KeyCode dashKey;

    [Header("Randomness")] public bool allowOverlap;
    public bool turnRandomOff;

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    #endregion

    private float _time;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;

        SetKeysToDefault();

        SpawnAtLastCheckpoint();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }

    #region Input

    private void GatherInput()
    {
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetKeyDown(jumpKey),
            JumpHeld = Input.GetKey(jumpKey),
            Move = GetMove()
        };

        if (stats.snapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < stats.horizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < stats.verticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }
    }

    private Vector2 GetMove()
    {
        int right = Convert.ToInt32(Input.GetKey(rightKey));
        int left = Convert.ToInt32(Input.GetKey(leftKey));
        int up = Convert.ToInt32(Input.GetKey(upKey));
        int down = Convert.ToInt32(Input.GetKey(downKey));

        return new Vector2(right - left, up - down);
    }

    #endregion Input

    #region RandomKey

    public static KeyCode GetRandomKeyCode()
    {
        var keys = new List<KeyCode>()
        {
            KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O,
            KeyCode.P,
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
            KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
            KeyCode.Tab, KeyCode.CapsLock, KeyCode.LeftShift, KeyCode.LeftControl, KeyCode.LeftAlt, KeyCode.BackQuote,
            KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace, KeyCode.LeftBracket, KeyCode.RightBracket,
            KeyCode.Backslash, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Return,
            KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.RightShift, KeyCode.RightAlt, KeyCode.RightControl,
            KeyCode.Space
        };

        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }

    public int GetIndexOfKeyCode(KeyCode keyCode)
    {
        var keys = new List<KeyCode>()
        {
            KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
            KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M,
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
            KeyCode.Tab, KeyCode.CapsLock, KeyCode.LeftShift, KeyCode.LeftControl, KeyCode.LeftAlt, KeyCode.BackQuote,
            KeyCode.Minus, KeyCode.Equals, KeyCode.Backspace, KeyCode.LeftBracket, KeyCode.RightBracket,
            KeyCode.Backslash, KeyCode.Semicolon, KeyCode.Quote, KeyCode.Return,
            KeyCode.Comma, KeyCode.Period, KeyCode.Slash, KeyCode.RightShift, KeyCode.RightAlt, KeyCode.RightControl,
            KeyCode.Space
        };

        return keys.FindIndex(x => x.Equals(keyCode));
    }

    public void RandomizeKeys()
    {
        if (turnRandomOff) return;

        rightKey = GetRandomKeyCode();
        leftKey = GetRandomKeyCode();
        upKey = GetRandomKeyCode();
        downKey = GetRandomKeyCode();
        jumpKey = GetRandomKeyCode();
        dashKey = GetRandomKeyCode();

        if (allowOverlap) return;
        var currKeys = new List<KeyCode>()
        {
            rightKey, leftKey, upKey, downKey, jumpKey, dashKey
        };

        for (var i = 0; i < currKeys.Count; i++)
        {
            for (var j = i + 1; j < currKeys.Count; j++)
            {
                while (currKeys[i] == currKeys[j])
                {
                    currKeys[j] = GetRandomKeyCode();
                }
            }
        }
    }

    public void SetKeysToDefault()
    {
        rightKey = KeyCode.D;
        leftKey = KeyCode.A;
        upKey = KeyCode.W;
        downKey = KeyCode.S;
        jumpKey = KeyCode.Space;
        dashKey = KeyCode.LeftShift;
    }

    #endregion RandomKey

    #region Spawn

    private void SpawnAtLastCheckpoint()
    {
        transform.position = SaveSystem.LoadCheckpointPos();
    }

    #endregion

    private void FixedUpdate()
    {
        CheckCollisions();

        HandleJump();
        HandleDirection();
        HandleGravity();

        ApplyMovement();
    }

    #region Collisions

    private float _frameLeftGrounded = float.MinValue;
    private bool _grounded;

    private void CheckCollisions()
    {
        Physics2D.queriesStartInColliders = false;

        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, stats.grounderDistance, ~stats.playerLayer);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, stats.grounderDistance, ~stats.playerLayer);

        // Hit a Ceiling
        if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

        // Landed on the Ground
        if (!_grounded && groundHit)
        {
            _grounded = true;
            _coyoteUsable = true;
            _bufferedJumpUsable = true;
            _endedJumpEarly = false;
            GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
        }
        // Left the Ground
        else if (_grounded && !groundHit)
        {
            _grounded = false;
            _frameLeftGrounded = _time;
            GroundedChanged?.Invoke(false, 0);
        }

        Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
    }

    public bool IsGrounded()
    {
        return _grounded;
    }

    #endregion

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + stats.jumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + stats.coyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = stats.jumpPower;
        Jumped?.Invoke();
    }

    #endregion

    #region Horizontal

    private void HandleDirection()
    {
        if (_frameInput.Move.x == 0)
        {
            var deceleration = _grounded ? stats.groundDeceleration : stats.airDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * stats.maxSpeed, stats.acceleration * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Gravity

    private void HandleGravity()
    {
        if (_grounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = stats.groundingForce;
        }
        else
        {
            var inAirGravity = stats.fallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= stats.jumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -stats.maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion

    private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
    }
#endif
}

public struct FrameInput
{
    public bool JumpDown;
    public bool JumpHeld;
    public Vector2 Move;
}

public interface IPlayerController
{
    public event Action<bool, float> GroundedChanged;

    public event Action Jumped;
    public Vector2 FrameInput { get; }
}
