using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGuide : MonoBehaviour
{
    public GameObject dynamicTileBackground;
    private GameObject _background;
    private KeyGrid _keyGrid;

    // Start is called before the first frame update
    private void Start()
    {
        _background = Instantiate(dynamicTileBackground, transform);
        _background.transform.SetAsFirstSibling();
        _keyGrid = GetComponentInChildren<KeyGrid>();
        _keyGrid.background = _background;
        _keyGrid.dynamicTileBackground = _background.GetComponent<DynamicTileBackground>();
    }
}
