using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SojuGauge : MonoBehaviour
{
    public DynamicTileBackground background;

    public float backgroundWidth;

    public float backgroundHeight;

    // Start is called before the first frame update
    void Start()
    {
        background.rectWidth = backgroundWidth;
        background.rectHeight = backgroundHeight;
        background.SetBackground(backgroundWidth, backgroundHeight);
    }
}
