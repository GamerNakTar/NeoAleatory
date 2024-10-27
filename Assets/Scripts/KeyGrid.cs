using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyGrid : MonoBehaviour
{
    [Header("KeyUIs")]
    public GameObject leftKey;
    public GameObject rightKey;
    public GameObject upKey;
    public GameObject downKey;
    public GameObject jumpKey;

    [Header("Offsets")][SerializeField] private float xPadding;
    [SerializeField] private float yPadding;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float blockOffset;

    [Header("Sprites")]
    public Sprite[] sprites;

    [SerializeField] private PlayerController playerController;

    private Sprite leftSprite;
    private Sprite rightSprite;
    private Sprite upSprite;
    private Sprite downSprite;
    private Sprite jumpSprite;

    private void Start()
    {
        UpdateKeyGrid();
    }

    public void UpdateKeyGrid()
    {
        ChangeSprites();
        ChangePos();
    }

    public void ChangeSprites()
    {
        // maybe using Resources.Load is a better idea
        Debug.Log(playerController.GetIndexOfKeyCode(playerController.leftKey));
        Debug.Log(sprites[playerController.GetIndexOfKeyCode(playerController.leftKey)]);
        leftSprite = sprites[playerController.GetIndexOfKeyCode(playerController.leftKey)];
        rightSprite = sprites[playerController.GetIndexOfKeyCode(playerController.rightKey)];
        upSprite = sprites[playerController.GetIndexOfKeyCode(playerController.upKey)];
        downSprite = sprites[playerController.GetIndexOfKeyCode(playerController.downKey)];
        jumpSprite = sprites[playerController.GetIndexOfKeyCode(playerController.jumpKey)];

        leftKey.GetComponent<Image>().sprite = leftSprite;
        rightKey.GetComponent<Image>().sprite = rightSprite;
        upKey.GetComponent<Image>().sprite = upSprite;
        downKey.GetComponent<Image>().sprite = downSprite;
        jumpKey.GetComponent<Image>().sprite = jumpSprite;

        leftKey.GetComponent<RectTransform>().sizeDelta = leftSprite.bounds.size * 200f;
        rightKey.GetComponent<RectTransform>().sizeDelta = rightSprite.bounds.size * 200f;
        upKey.GetComponent<RectTransform>().sizeDelta = upSprite.bounds.size * 200f;
        downKey.GetComponent<RectTransform>().sizeDelta = downSprite.bounds.size * 200f;
        jumpKey.GetComponent<RectTransform>().sizeDelta = jumpSprite.bounds.size * 200f;
    }

    public void ChangePos()
    {
        // bottom row (left, down, right)
        leftKey.transform.localPosition = new Vector3(leftSprite.bounds.size.x * 100f + xOffset, -yOffset, 0);
        downKey.transform.localPosition = leftKey.transform.localPosition + new Vector3(leftSprite.bounds.size.x * 100f + xPadding + downSprite.bounds.size.x * 100f, 0, 0);
        rightKey.transform.localPosition = downKey.transform.localPosition + new Vector3(downSprite.bounds.size.x * 100f + xPadding + rightSprite.bounds.size.x * 100f, 0, 0);

        // top row (up)
        upKey.transform.localPosition = downKey.transform.localPosition + new Vector3(0, downSprite.bounds.size.y * 100f + yPadding + upSprite.bounds.size.y * 100f, 0);

        // jump & possibly dash row (jump, dash)
        jumpKey.transform.localPosition = rightKey.transform.localPosition + new Vector3(rightSprite.bounds.size.x * 100f + blockOffset + jumpSprite.bounds.size.x * 100f, 0, 0);
    }
}
