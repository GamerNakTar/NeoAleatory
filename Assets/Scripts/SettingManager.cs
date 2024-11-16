using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    [Header("Volume")]
    public AudioMixer audioMixer;
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

    public void SetSetting(SettingType type, float value)
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

    public void SetMasterVolume(float volume)
    {
        MasterVolume = volume * 100f - 80f;
        audioMixer.SetFloat("MasterVolume", MasterVolume);
    }

    public static float GetMasterVolume()
    {
        return MasterVolume;
    }
}
