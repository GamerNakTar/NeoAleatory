using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KeyGrid : MonoBehaviour
{
    // KeyUI GameObjects
    private static GameObject _leftKey;
    private static GameObject _rightKey;
    private static GameObject _upKey;
    private static GameObject _downKey;
    private static GameObject _jumpKey;

    // Background GameObject
    private static GameObject _background;

    [Header("Offsets")][SerializeField] private float xSpacing;
    [SerializeField] private float ySpacing;
    [SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    [SerializeField] private float blockOffset;

    [Header("Sprites")]
    public Sprite[] sprites;

    public float spriteScale;

    [SerializeField] private PlayerController playerController;

    private Sprite _leftSprite;
    private Sprite _rightSprite;
    private Sprite _upSprite;
    private Sprite _downSprite;
    private Sprite _jumpSprite;

    private void Start()
    {
        UpdateKeyGrid();
        _leftKey = GameObject.Find("LeftKey");
        _rightKey = GameObject.Find("RightKey");
        _upKey = GameObject.Find("UpKey");
        _downKey = GameObject.Find("DownKey");
        _jumpKey = GameObject.Find("JumpKey");
    }

    public void UpdateKeyGrid()
    {
        ChangeSprites();
        ChangePos();
        ResizeBackground();
    }

    #region Sprite

    private readonly Image _leftImage = _leftKey.GetComponent<Image>();
    private readonly Image _rightImage = _rightKey.GetComponent<Image>();
    private readonly Image _upImage = _upKey.GetComponent<Image>();
    private readonly Image _downImage = _downKey.GetComponent<Image>();
    private readonly Image _jumpImage = _jumpKey.GetComponent<Image>();

    private readonly RectTransform _leftRect = _leftKey.GetComponent<RectTransform>();
    private readonly RectTransform _rightRect = _rightKey.GetComponent<RectTransform>();
    private readonly RectTransform _upRect = _upKey.GetComponent<RectTransform>();
    private readonly RectTransform _downRect = _downKey.GetComponent<RectTransform>();
    private readonly RectTransform _jumpRect = _jumpKey.GetComponent<RectTransform>();

    private void ChangeSprites()
    {
        // maybe using Resources.Load is a better idea
        _leftSprite = sprites[playerController.GetIndexOfKeyCode(playerController.leftKey)];
        _rightSprite = sprites[playerController.GetIndexOfKeyCode(playerController.rightKey)];
        _upSprite = sprites[playerController.GetIndexOfKeyCode(playerController.upKey)];
        _downSprite = sprites[playerController.GetIndexOfKeyCode(playerController.downKey)];
        _jumpSprite = sprites[playerController.GetIndexOfKeyCode(playerController.jumpKey)];

        _leftImage.sprite = _leftSprite;
        _rightImage.sprite = _rightSprite;
        _upImage.sprite = _upSprite;
        _downImage.sprite = _downSprite;
        _jumpImage.sprite = _jumpSprite;

        _leftRect.sizeDelta = _leftSprite.bounds.size * spriteScale;
        _rightRect.sizeDelta = _rightSprite.bounds.size * spriteScale;
        _upRect.sizeDelta = _upSprite.bounds.size * spriteScale;
        _downRect.sizeDelta = _downSprite.bounds.size * spriteScale;
        _jumpRect.sizeDelta = _jumpSprite.bounds.size * spriteScale;
    }

    #endregion

    private void ChangePos()
    {
        // bottom row (left, down, right)
        _leftKey.transform.localPosition = transform.position + new Vector3(_leftSprite.bounds.size.x * spriteScale / 2 + xPadding, yPadding, 0);
        _downKey.transform.localPosition = _leftKey.transform.localPosition + new Vector3(_leftSprite.bounds.size.x * spriteScale / 2 + xSpacing + _downSprite.bounds.size.x * spriteScale / 2, 0, 0);
        _rightKey.transform.localPosition = _downKey.transform.localPosition + new Vector3(_downSprite.bounds.size.x * spriteScale / 2 + xSpacing + _rightSprite.bounds.size.x * spriteScale / 2, 0, 0);

        // top row (up)
        _upKey.transform.localPosition = _downKey.transform.localPosition + new Vector3(0, _downSprite.bounds.size.y * spriteScale / 2 + ySpacing + _upSprite.bounds.size.y * spriteScale / 2, 0);

        // jump & possibly dash row (jump, dash)
        _jumpKey.transform.localPosition = _rightKey.transform.localPosition + new Vector3(_rightSprite.bounds.size.x * spriteScale / 2 + blockOffset + _jumpSprite.bounds.size.x * spriteScale / 2, 0, 0);
    }

    #region Background

    private readonly RectTransform _backgroundRect = _background.GetComponent<RectTransform>();

    private void ResizeBackground()
    {
        // horizontal resize
        var width = _leftSprite.bounds.size.x * spriteScale + _downSprite.bounds.size.x * spriteScale + _rightSprite.bounds.size.x * spriteScale + _jumpSprite.bounds.size.x * spriteScale + xPadding * 2 + xSpacing * 2 + blockOffset;
        var height = _downSprite.bounds.size.y * spriteScale + _upSprite.bounds.size.y * spriteScale + ySpacing +
                     yPadding * 2;
        _backgroundRect.sizeDelta = new Vector2(width, height);
        _background.transform.localPosition = new Vector3(-Screen.currentResolution.width / 2 + width / 2, 0);
    }

    #endregion
}
