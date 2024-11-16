using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour, IButton
{
    public void OnClick()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ReSize(int width, int height)
    {
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
