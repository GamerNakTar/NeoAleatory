using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RectangleManager : MonoBehaviour
{
    public GameObject sourceRectangle;
    private GameObject _instanceRectangle;

    public float width;
    public float height;
    public bool setAsFirstSibling = false;

    private void Start()
    {
        _instanceRectangle = Instantiate(sourceRectangle, transform);
        if (setAsFirstSibling)
        {
            _instanceRectangle.transform.SetAsFirstSibling();
        }
        _instanceRectangle.GetComponent<IRectangle>().SetSize(width, height);
    }
}
