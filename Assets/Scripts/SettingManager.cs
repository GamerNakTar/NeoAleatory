using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public static float MasterVolume;

    public enum SettingType
    {
        MasterVolume,
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (!Instance)
        {
            Instance = this;
            Init();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SavePrefs();
    }

    private void Init()
    {
        MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        PlayerPrefs.Save();
    }

    public static void SetDefaultSetting()
    {
        MasterVolume = 1f;
    }

    public static float GetSetting(SettingType type)
    {
        switch (type)
        {
            case SettingType.MasterVolume:
                return GetMasterVolume();
            default:
                return 0;
        }
    }

    public static void SetSetting(SettingType type, float value)
    {
        switch (type)
        {
            case SettingType.MasterVolume:
                SetMasterVolume(value);
                break;
            default:
                Debug.Log("non float value given to float set setting");
                break;
        }
    }

    public static void SetMasterVolume(float volume)
    {
        MasterVolume = volume;
    }

    public static float GetMasterVolume()
    {
        return MasterVolume;
    }
}
