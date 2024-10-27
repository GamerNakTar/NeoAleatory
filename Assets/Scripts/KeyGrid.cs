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
    [Header("Key")]
    public GameObject leftKey;
    public GameObject rightKey;
    public GameObject upKey;
    public GameObject downKey;
    public GameObject jumpKey;

    // Background GameObject
    [Header("Background")]
    public GameObject background;

    [SerializeField] private DynamicTileBackground dynamicTileBackground;

    [Header("Offsets")][SerializeField] private float xSpacing;
    [SerializeField] private float ySpacing;
    [SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    [SerializeField] private float blockSpacing;

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
        Init();
        UpdateKeyGrid();
    }

    private void Init()
    {
        _leftImage = leftKey.GetComponent<Image>();
        _rightImage = rightKey.GetComponent<Image>();
        _upImage = upKey.GetComponent<Image>();
        _downImage = downKey.GetComponent<Image>();
        _jumpImage = jumpKey.GetComponent<Image>();

        _leftRect = leftKey.GetComponent<RectTransform>();
        _rightRect = rightKey.GetComponent<RectTransform>();
        _upRect = upKey.GetComponent<RectTransform>();
        _downRect = downKey.GetComponent<RectTransform>();
        _jumpRect = jumpKey.GetComponent<RectTransform>();

        _background = background.GetComponent<RectTransform>();
        dynamicTileBackground = background.GetComponent<DynamicTileBackground>();
    }

    public void UpdateKeyGrid()
    {
        ChangeSprites();
        ChangePos();
        ResizeBackground();
    }

    #region Sprite

    private Image _leftImage;
    private Image _rightImage;
    private Image _upImage;
    private Image _downImage;
    private Image _jumpImage;

    private RectTransform _leftRect;
    private RectTransform _rightRect;
    private RectTransform _upRect;
    private RectTransform _downRect;
    private RectTransform _jumpRect;

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

    #region Position

    private void ChangePos()
    {
        // bottom row (left, down, right)
        leftKey.transform.localPosition = new Vector3(_leftSprite.bounds.size.x * spriteScale / 2 + xPadding, -yPadding - _upSprite.bounds.size.y * spriteScale - ySpacing - _leftSprite.bounds.size.y * spriteScale / 2, 0);
        downKey.transform.localPosition = leftKey.transform.localPosition + new Vector3(_leftSprite.bounds.size.x * spriteScale / 2 + xSpacing + _downSprite.bounds.size.x * spriteScale / 2, 0, 0);
        rightKey.transform.localPosition = downKey.transform.localPosition + new Vector3(_downSprite.bounds.size.x * spriteScale / 2 + xSpacing + _rightSprite.bounds.size.x * spriteScale / 2, 0, 0);

        // top row (up)
        upKey.transform.localPosition = downKey.transform.localPosition + new Vector3(0, _downSprite.bounds.size.y * spriteScale / 2 + ySpacing + _upSprite.bounds.size.y * spriteScale / 2, 0);

        // jump & possibly dash row (jump, dash)
        jumpKey.transform.localPosition = rightKey.transform.localPosition + new Vector3(_rightSprite.bounds.size.x * spriteScale / 2 + blockSpacing + _jumpSprite.bounds.size.x * spriteScale / 2, 0, 0);
    }

    #endregion

    #region Background

    private RectTransform _background;

    private void ResizeBackground()
    {
        // horizontal resize
        var width = _leftSprite.bounds.size.x * spriteScale + _downSprite.bounds.size.x * spriteScale + _rightSprite.bounds.size.x * spriteScale + _jumpSprite.bounds.size.x * spriteScale + xPadding * 2 + xSpacing * 2 + blockSpacing;
        var height = _downSprite.bounds.size.y * spriteScale + _upSprite.bounds.size.y * spriteScale + ySpacing +
                     yPadding * 2;
        _background.sizeDelta = new Vector2(width, height);
        background.transform.localPosition = new Vector3(transform.localPosition.x + width / 2, transform.localPosition.y - height / 2, 0);
        dynamicTileBackground.SetBackground(width, height);
    }

    #endregion
}
