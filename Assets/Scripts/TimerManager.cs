using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float time;
    public float timerCycle;

    [SerializeField] private PlayerController player;
    [SerializeField] private KeyGrid keyGrid;

    [Header("UI")] public Image timerBar;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        CheckTimer();
        UpdateSlider();
    }

    public void CheckTimer()
    {
        if (time >= timerCycle)
        {
            time -= timerCycle;
            player.RandomizeKeys();
            keyGrid.UpdateKeyGrid();
        }
    }

    public void UpdateSlider()
    {
        timerBar.fillAmount = time / timerCycle;
    }
}
