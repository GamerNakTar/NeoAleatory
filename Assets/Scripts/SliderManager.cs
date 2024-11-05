using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderManager : MonoBehaviour
{
    [SerializeField] private bool isSetting;
    [SerializeField] private SettingManager.SettingType settingType;
    [SerializeField] private float spriteScale;
    // should add stuff like cam, bar, sprites if needed
    [SerializeField] private GameObject sliderPrefab;
    [SerializeField] private float width;
    [SerializeField] private float height;

    private void Start()
    {
        GameObject slider = Instantiate(sliderPrefab, transform);
        CustomSlider sliderScript = slider.GetComponent<CustomSlider>();
        sliderScript.isSetting = isSetting;
        sliderScript.settingType = settingType;
        sliderScript.spriteScale = spriteScale;
        slider.GetComponent<IRectangle>().SetSize(width, height);
    }
}
