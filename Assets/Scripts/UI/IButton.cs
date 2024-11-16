using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButton
{
    public void OnClick();

    public void ReSize(int width, int height);
}