using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DynamicTileBackground : MonoBehaviour
{
    [SerializeField] private RectTransform topLeftCornerRect;
    [SerializeField] private RectTransform topRightCornerRect;
    [SerializeField] private RectTransform bottomLeftCornerRect;
    [SerializeField] private RectTransform bottomRightCornerRect;
    [SerializeField] private RectTransform leftSideRect;
    [SerializeField] private RectTransform rightSideRect;
    [SerializeField] private RectTransform topSideRect;
    [SerializeField] private RectTransform bottomSideRect;
    [SerializeField] private RectTransform fillRect;

    [SerializeField] private Image referenceImage;
    [SerializeField] private float spriteScale;
    private float _tileSize;
    private float _actualTileSize;

    public float rectWidth;
    public float rectHeight;

    // Start is called before the first frame update
    void Start()
    {
        _tileSize = referenceImage.sprite.bounds.size.x;
        _actualTileSize = _tileSize * spriteScale;
        Debug.Log(_actualTileSize);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
    }

    public void SetBackground(float width, float height)
    {
        topLeftCornerRect.sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        topLeftCornerRect.localPosition = new Vector3(-width / 2 + _actualTileSize / 2, height / 2 - _actualTileSize / 2, 0);
        topRightCornerRect.sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        topRightCornerRect.localPosition = new Vector3(width / 2 - _actualTileSize / 2, height / 2 - _actualTileSize / 2, 0);
        bottomLeftCornerRect.sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        bottomLeftCornerRect.localPosition = new Vector3(-width / 2 + _actualTileSize / 2, -height / 2 + _actualTileSize / 2, 0);
        bottomRightCornerRect.sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        bottomRightCornerRect.localPosition = new Vector3(width / 2 - _actualTileSize / 2, -height / 2 + _actualTileSize / 2, 0);
        leftSideRect.sizeDelta = new Vector2(_actualTileSize, height - (_actualTileSize * 2));
        leftSideRect.localPosition = new Vector3(-width / 2 + _actualTileSize / 2, 0, 0);
        rightSideRect.sizeDelta = new Vector2(_actualTileSize, height - (_actualTileSize * 2));
        rightSideRect.localPosition = new Vector3(width / 2 - _actualTileSize / 2, 0, 0);
        topSideRect.sizeDelta = new Vector2(width - (_actualTileSize * 2), _actualTileSize);
        topSideRect.localPosition = new Vector3(0, height / 2 - _actualTileSize / 2, 0);
        bottomSideRect.sizeDelta = new Vector2(width - (_actualTileSize * 2), _actualTileSize);
        bottomSideRect.localPosition = new Vector3(0, -height / 2 + _actualTileSize / 2, 0);
        fillRect.sizeDelta = new Vector2(width - _actualTileSize * 2, height - _actualTileSize * 2);
        fillRect.localPosition = new Vector3(0, 0, 0);
    }
}
