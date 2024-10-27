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
    private void Update()
    {
        time += Time.deltaTime;
        CheckTimer();
        UpdateSlider();
    }

    private void CheckTimer()
    {
        if (time < timerCycle) return;
        time -= timerCycle;
        player.RandomizeKeys();
        keyGrid.UpdateKeyGrid();
    }

    private void UpdateSlider()
    {
        timerBar.fillAmount = (timerCycle - time) / timerCycle;
    }
}
