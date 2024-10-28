using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SojuGauge : MonoBehaviour
{
    public GameObject dynamicTileBackground;
    private GameObject _background;

    public float width;
    public float height;

    // Start is called before the first frame update
    private void Start()
    {
        _background = Instantiate(dynamicTileBackground, transform);
        _background.transform.SetAsFirstSibling();
        _background.GetComponent<DynamicTileBackground>().SetBackground(width, height);
    }
}
