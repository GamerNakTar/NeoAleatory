using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static bool CursorIsOn;

    private void Start()
    {
        CursorIsOn = true;
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleCursor()
    {
        CursorIsOn = !CursorIsOn;
        Cursor.lockState = CursorIsOn ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public static void TurnCursorOn()
    {
        CursorIsOn = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public static void TurnCursorOff()
    {
        CursorIsOn = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
