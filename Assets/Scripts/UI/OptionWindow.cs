using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class OptionWindow : MonoBehaviour
{
    public bool isOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    private void OnEnable()
    {
        Debug.Log("OptionWindow OnEnable");
        isOpen = true;
    }

    private void OnDisable()
    {
        Debug.Log("OptionWindow OnDisable");
        isOpen = false;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
