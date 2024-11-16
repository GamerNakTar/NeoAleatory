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

    private static float _masterVolume;

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
        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        SetMasterVolume(_masterVolume);
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetFloat("MasterVolume", _masterVolume);
        PlayerPrefs.Save();
    }

    public static void SetDefaultSetting()
    {
        _masterVolume = 1f;
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
        _masterVolume = volume * 100f - 80f;
        audioMixer.SetFloat("MasterVolume", _masterVolume);
    }

    public static float GetMasterVolume()
    {
        return (_masterVolume + 80f) / 100f;
    }
}
