using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private RectTransform[] buttonRects;
    [SerializeField] private float yPadding;
    [SerializeField] private float ySpacing;
    [SerializeField] private float xPadding;
    public float width;
    public float height;

    public GameObject dynamicTileBackground;
    private GameObject _background;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
        SetButtonPositions();
        SetBackground();
    }

    private void Init()
    {
        buttonRects = GetComponentsInChildren<RectTransform>();
        _background = Instantiate(dynamicTileBackground, transform);
        _background.transform.SetAsFirstSibling();
    }

    private void SetButtonPositions()
    {
        var maxWidth = buttonRects[0].rect.width;
        foreach (var t in buttonRects)
        {
            if (t.rect.width > maxWidth)
            {
                maxWidth = t.rect.width;
            }
        }
        width = maxWidth + xPadding * 2;
        height = buttonRects.Length * 66 + (buttonRects.Length - 1) * ySpacing + yPadding * 2;
        buttonRects[0].localPosition = new Vector2(0, height / 2 - yPadding - 33);
        for (var i = 1; i < buttonRects.Length; i++)
        {
            buttonRects[i].localPosition = new Vector2(0, buttonRects[i-1].localPosition.y - ySpacing - 66);
        }
    }

    private void SetBackground()
    {
        _background.GetComponent<DynamicTileBackground>().SetBackground(width, height);
    }
}
