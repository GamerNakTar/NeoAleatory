using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    private bool _waited = false;

    [SerializeField] private GameObject text;

    private void Start()
    {
        StartCoroutine(WaitBeforeAnyInput());
    }

    void Update()
    {
        if (Input.anyKeyDown && _waited)
        {
            SceneSwapper.SwapScene(SceneSwapper.Scene.Title);
        }
    }

    private IEnumerator WaitBeforeAnyInput()
    {
        // prevent instant return to title
        yield return new WaitForSeconds(1f);
        text.SetActive(true);
        _waited = true;
    }
}
