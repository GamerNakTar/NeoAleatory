using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _target;

    [Header("References")]
    public GameObject player;

    private PlayerController _playerController;
    private Rigidbody _playerRigidbody;

    [Header("Settings")]
    public float moveSpeed;
    public float lookAheadDistance;
    public float lookAheadSpeed;
    public float lookAheadThreshold;
    public float maxVerticalOffset;

    private float _lookOffset;
    private bool _isFalling;

    // Start is called before the first frame update
    private void Start()
    {
        _target = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        _playerController = player.GetComponent<PlayerController>();
        _playerRigidbody = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        GetTarget();
        LookAhead();
        MoveCamera();
    }

    private void GetTarget()
    {
        // vertically follow when falling out of camera
        if (transform.position.y - player.transform.position.y > maxVerticalOffset)
        {
            _isFalling = true;
        }
        if (_isFalling)
        {
            _target.y = player.transform.position.y;
        }

        // vertically follow when grounded
        if (_playerController.IsGrounded())
        {
            _target.y = player.transform.position.y;
            _isFalling = false;
        }

        if (_target.y < 0)
        {
            _target.y = 0;
        }
    }

    private void MoveCamera()
    {
        transform.position = Vector3.Lerp(transform.position, _target, moveSpeed * Time.deltaTime);
    }

    private void LookAhead()
    {
        if (_playerRigidbody.velocity.x > lookAheadThreshold)
        {
            _lookOffset = Mathf.Lerp(_lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }
        else if (_playerRigidbody.velocity.x < -lookAheadThreshold)
        {
            _lookOffset = Mathf.Lerp(_lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }
        else
        {
            _lookOffset = Mathf.Lerp(_lookOffset, 0f, lookAheadSpeed * Time.deltaTime);
        }

        _target.x = player.transform.position.x + _lookOffset;
    }
}
