using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointButton : MonoBehaviour
{
    [SerializeField] private Checkpoint checkpoint;

    private KeyCode _keyCode;
    // Start is called before the first frame update
    void Start()
    {
        _keyCode = PlayerController.GetRandomKeyCode();

        checkpoint = GetComponentInParent<Checkpoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            checkpoint.SaveCheckpoint();
        }

        // for debug
        if (Input.GetKeyDown(KeyCode.E) && checkpoint.isActivated)
        {
            checkpoint.SaveCheckpoint();
        }
    }
}
