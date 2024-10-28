using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DynamicTileBackground : MonoBehaviour
{
    [SerializeField] private RectTransform[] tileRects;

    [SerializeField] private Image referenceImage;
    [SerializeField] private float spriteScale;
    private float _tileSize;
    private float _actualTileSize;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
        // gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(rectWidth, rectHeight);
    }

    private void Init()
    {
        _tileSize = referenceImage.sprite.bounds.size.x;
        _actualTileSize = _tileSize * spriteScale;
        tileRects = GetComponentsInChildren<RectTransform>();
    }

    public void SetBackground(float width, float height)
    {
        if (tileRects.Length == 0)
        {
            Init();
        }
        tileRects[0].sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        tileRects[0].localPosition = new Vector3(-width / 2 + _actualTileSize / 2, height / 2 - _actualTileSize / 2, 0);
        tileRects[1].sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        tileRects[1].localPosition = new Vector3(width / 2 - _actualTileSize / 2, height / 2 - _actualTileSize / 2, 0);
        tileRects[2].sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        tileRects[2].localPosition = new Vector3(-width / 2 + _actualTileSize / 2, -height / 2 + _actualTileSize / 2, 0);
        tileRects[3].sizeDelta = new Vector2(_actualTileSize, _actualTileSize);
        tileRects[3].localPosition = new Vector3(width / 2 - _actualTileSize / 2, -height / 2 + _actualTileSize / 2, 0);
        tileRects[4].sizeDelta = new Vector2(_actualTileSize, height - (_actualTileSize * 2));
        tileRects[4].localPosition = new Vector3(-width / 2 + _actualTileSize / 2, 0, 0);
        tileRects[5].sizeDelta = new Vector2(_actualTileSize, height - (_actualTileSize * 2));
        tileRects[5].localPosition = new Vector3(width / 2 - _actualTileSize / 2, 0, 0);
        tileRects[6].sizeDelta = new Vector2(width - (_actualTileSize * 2), _actualTileSize);
        tileRects[6].localPosition = new Vector3(0, height / 2 - _actualTileSize / 2, 0);
        tileRects[7].sizeDelta = new Vector2(width - (_actualTileSize * 2), _actualTileSize);
        tileRects[7].localPosition = new Vector3(0, -height / 2 + _actualTileSize / 2, 0);
        tileRects[8].sizeDelta = new Vector2(width - _actualTileSize * 2, height - _actualTileSize * 2);
        tileRects[8].localPosition = new Vector3(0, 0, 0);
    }
}
