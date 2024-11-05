using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IRectangle
{
    [SerializeField] private Camera cam;

    private RectTransform _myTransform;
    [SerializeField] private Image bar;
    [SerializeField] private Sprite dragSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Image[] images;
    [SerializeField] private RectTransform[] rectTransforms;

    private float _spriteX;
    private float _spriteY;
    [SerializeField] private float spriteScale;
    private float _minX;
    private float _maxX;
    private float _newX;
    private float _percent;
    private float _width;

    [SerializeField] private bool isSetting;
    [SerializeField] private SettingManager.SettingType settingType;

    public void OnDrag(PointerEventData eventData)
    {
        _newX = Mathf.Clamp(Input.mousePosition.x, _minX, _maxX);
        _percent = Mathf.Clamp01((_newX-_minX) / (_maxX-_minX));
        bar.fillAmount = _percent;

        // save setting value
        if (isSetting)
        {
            SettingManager.SetSetting(settingType, _percent);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        bar.sprite = dragSprite;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bar.sprite = defaultSprite;
    }

    private void Start()
    {
        cam = Camera.main;

        // get coords
        _myTransform = GetComponent<RectTransform>();
        _minX = cam.WorldToScreenPoint(_myTransform.position).x - _width / 2;
        _maxX = cam.WorldToScreenPoint(_myTransform.position).x + _width / 2;

        // get sprites and rectTransforms
        images = GetComponentsInChildren<Image>();
        rectTransforms = GetComponentsInChildren<RectTransform>();

        // initialize local variable
        _spriteX = images[0].sprite.bounds.size.x * spriteScale;
        _spriteY = images[0].sprite.bounds.size.y * spriteScale;

        _myTransform.sizeDelta = new Vector2(_width, _spriteY);

        Resize();

        bar.fillAmount = SettingManager.GetSetting(settingType);
    }

    private void Resize()
    {
        rectTransforms[1].sizeDelta = new Vector2(_width - _spriteX * 2, rectTransforms[1].sizeDelta.y);
        rectTransforms[1].anchoredPosition = new Vector2(0, 0);
        rectTransforms[2].sizeDelta = new Vector2(_width - _spriteX * 2, rectTransforms[2].sizeDelta.y);
        rectTransforms[2].anchoredPosition = new Vector2(0, 0);

        rectTransforms[3].sizeDelta = new Vector2(_spriteX, rectTransforms[3].sizeDelta.y);
        rectTransforms[3].anchoredPosition = Vector2.zero;
        rectTransforms[4].sizeDelta = new Vector2(_spriteX, rectTransforms[4].sizeDelta.y);
        rectTransforms[4].anchoredPosition = Vector2.zero;
    }

    public float GetPercent()
    {
        return _percent;
    }

    public void SetSize(float width, float height)
    {
        SetSize(width);
    }

    private void SetSize(float width)
    {
        _width = width;
    }
}
