using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float time;
    public float timerCycle;

    [SerializeField] private PlayerController player;
    [SerializeField] private KeyGrid keyGrid;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        CheckTimer();
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
}
