using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KeyGrid : MonoBehaviour
{
    [Header("KeyUIs")]
    public GameObject leftKey;
    public GameObject rightKey;
    public GameObject upKey;
    public GameObject downKey;
    public GameObject jumpKey;

    [Header("Background")]
    public GameObject background;

    public float height;

    [Header("Offsets")][SerializeField] private float xSpacing;
    [SerializeField] private float ySpacing;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float blockOffset;

    [Header("Sprites")]
    public Sprite[] sprites;

    public float spriteScale;

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
        ResizeBackground();
    }

    public void ChangeSprites()
    {
        // maybe using Resources.Load is a better idea
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

        leftKey.GetComponent<RectTransform>().sizeDelta = leftSprite.bounds.size * spriteScale;
        rightKey.GetComponent<RectTransform>().sizeDelta = rightSprite.bounds.size * spriteScale;
        upKey.GetComponent<RectTransform>().sizeDelta = upSprite.bounds.size * spriteScale;
        downKey.GetComponent<RectTransform>().sizeDelta = downSprite.bounds.size * spriteScale;
        jumpKey.GetComponent<RectTransform>().sizeDelta = jumpSprite.bounds.size * spriteScale;
    }

    public void ChangePos()
    {
        // bottom row (left, down, right)
        leftKey.transform.localPosition = new Vector3(leftSprite.bounds.size.x * spriteScale / 2 + xOffset, -yOffset, 0);
        downKey.transform.localPosition = leftKey.transform.localPosition + new Vector3(leftSprite.bounds.size.x * spriteScale / 2 + xSpacing + downSprite.bounds.size.x * spriteScale / 2, 0, 0);
        rightKey.transform.localPosition = downKey.transform.localPosition + new Vector3(downSprite.bounds.size.x * spriteScale / 2 + xSpacing + rightSprite.bounds.size.x * spriteScale / 2, 0, 0);

        // top row (up)
        upKey.transform.localPosition = downKey.transform.localPosition + new Vector3(0, downSprite.bounds.size.y * spriteScale / 2 + ySpacing + upSprite.bounds.size.y * spriteScale / 2, 0);

        // jump & possibly dash row (jump, dash)
        jumpKey.transform.localPosition = rightKey.transform.localPosition + new Vector3(rightSprite.bounds.size.x * spriteScale / 2 + blockOffset + jumpSprite.bounds.size.x * spriteScale / 2, 0, 0);
    }

    public void ResizeBackground()
    {
        // horizontal resize
        float width = jumpKey.transform.position.x + jumpSprite.bounds.size.x * spriteScale / 2 + xOffset - transform.position.x;
        background.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        background.transform.localPosition = new Vector3(-Screen.currentResolution.width / 2 + width / 2, 0);
    }
}
