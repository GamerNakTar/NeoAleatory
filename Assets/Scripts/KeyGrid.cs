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

    public GameObject topLeftCorner;
    public GameObject topRightCorner;
    public GameObject bottomLeftCorner;
    public GameObject bottomRightCorner;
    public GameObject leftSide;
    public GameObject rightSide;
    public GameObject topSide;
    public GameObject bottomSide;
    public GameObject fill;

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

        _topLeftCornerRect = topLeftCorner.GetComponent<RectTransform>();
        _topRightCornerRect = topRightCorner.GetComponent<RectTransform>();
        _bottomLeftCornerRect = bottomLeftCorner.GetComponent<RectTransform>();
        _bottomRightCornerRect = bottomRightCorner.GetComponent<RectTransform>();
        _leftSideRect = leftSide.GetComponent<RectTransform>();
        _rightSideRect = rightSide.GetComponent<RectTransform>();
        _topSideRect = topSide.GetComponent<RectTransform>();
        _bottomSideRect = bottomSide.GetComponent<RectTransform>();
        _fillRect = fill.GetComponent<RectTransform>();

        _tileSize = topLeftCorner.GetComponent<Image>().sprite.bounds.size.x;
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

    #region Background

    private RectTransform _background;

    private RectTransform _topLeftCornerRect;
    private RectTransform _topRightCornerRect;
    private RectTransform _bottomLeftCornerRect;
    private RectTransform _bottomRightCornerRect;
    private RectTransform _leftSideRect;
    private RectTransform _rightSideRect;
    private RectTransform _topSideRect;
    private RectTransform _bottomSideRect;
    private RectTransform _fillRect;

    private float _tileSize;

    private void ResizeBackground()
    {
        // horizontal resize
        var width = _leftSprite.bounds.size.x * spriteScale + _downSprite.bounds.size.x * spriteScale + _rightSprite.bounds.size.x * spriteScale + _jumpSprite.bounds.size.x * spriteScale + xPadding * 2 + xSpacing * 2 + blockSpacing;
        var height = _downSprite.bounds.size.y * spriteScale + _upSprite.bounds.size.y * spriteScale + ySpacing +
                     yPadding * 2;
        _background.sizeDelta = new Vector2(width, height);
        background.transform.localPosition = new Vector3(transform.localPosition.x + width / 2, transform.localPosition.y - height / 2, 0);

        _topLeftCornerRect.sizeDelta = new Vector2(_tileSize * spriteScale, _tileSize * spriteScale);
        _topLeftCornerRect.localPosition = new Vector3(-width / 2 + _tileSize * spriteScale / 2, height / 2 - _tileSize * spriteScale / 2, 0);
        _topRightCornerRect.sizeDelta = new Vector2(_tileSize * spriteScale, _tileSize * spriteScale);
        _topRightCornerRect.localPosition = new Vector3(width / 2 - _tileSize * spriteScale / 2, height / 2 - _tileSize * spriteScale / 2, 0);
        _bottomLeftCornerRect.sizeDelta = new Vector2(_tileSize * spriteScale, _tileSize * spriteScale);
        _bottomLeftCornerRect.localPosition = new Vector3(-width / 2 + _tileSize * spriteScale / 2, -height / 2 + _tileSize * spriteScale / 2, 0);
        _bottomRightCornerRect.sizeDelta = new Vector2(_tileSize * spriteScale, _tileSize * spriteScale);
        _bottomRightCornerRect.localPosition = new Vector3(width / 2 - _tileSize * spriteScale / 2, -height / 2 + _tileSize * spriteScale / 2, 0);
        _leftSideRect.sizeDelta = new Vector2(_tileSize * spriteScale, height - (_tileSize * spriteScale * 2));
        _leftSideRect.localPosition = new Vector3(-width / 2 + _tileSize * spriteScale / 2, 0, 0);
        _rightSideRect.sizeDelta = new Vector2(_tileSize * spriteScale, height - (_tileSize * spriteScale * 2));
        _rightSideRect.localPosition = new Vector3(width / 2 - _tileSize * spriteScale / 2, 0, 0);
        _topSideRect.sizeDelta = new Vector2(width - (_tileSize * spriteScale * 2), _tileSize * spriteScale);
        _topSideRect.localPosition = new Vector3(0, height / 2 - _tileSize * spriteScale / 2, 0);
        _bottomSideRect.sizeDelta = new Vector2(width - (_tileSize * spriteScale * 2), _tileSize * spriteScale);
        _bottomSideRect.localPosition = new Vector3(0, -height / 2 + _tileSize * spriteScale / 2, 0);
        _fillRect.sizeDelta = new Vector2(width - _tileSize * spriteScale * 2, height - _tileSize * spriteScale * 2);
        _fillRect.localPosition = new Vector3(0, 0, 0);
    }

    #endregion
}
