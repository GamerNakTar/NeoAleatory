using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class OptionWindow : MonoBehaviour
{
    public bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    private void OnEnable()
    {
        isOpen = true;
    }

    private void OnDisable()
    {
        isOpen = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
