using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Checkpoint : MonoBehaviour
{
    [FormerlySerializedAs("checkpointId")] public int checkpointID;
    public bool isActivated = false; // 체크포인트 활성화 여부
    public bool isSaved = false;

    private static Checkpoint _lastCheckpoint;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        if (!_lastCheckpoint && SaveSystem.LoadCheckpointID() == checkpointID)
        {
            _lastCheckpoint = this;
        }

        if (_lastCheckpoint == this)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            isSaved = true;
            isActivated = true;
        }

        Debug.Log("Checkpoint start, _lastCheckpoint: " + _lastCheckpoint);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            ActivateCheckpoint();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActivated)
        {
            DeActivateCheckpoint();
        }

    }

    private void ActivateCheckpoint()
    {
        isActivated = true;

        // 시각적 효과 추가 (예: 색 변경, 애니메이션)
        if (!isSaved)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        Debug.Log("Checkpoint Activated at: " + transform.position);
    }

    private void DeActivateCheckpoint()
    {
        isActivated = false;

        if (!isSaved)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        Debug.Log("Checkpoint Deactivated at: " + transform.position);
    }

    public void SaveCheckpoint()
    {
        SaveSystem.SaveCheckpointPos(transform.position);
        SaveSystem.SaveCheckpointID(checkpointID);

        spriteRenderer.color = Color.green;

        isSaved = true;

        if (!_lastCheckpoint)
        {
            _lastCheckpoint = this;
        }
        else if (_lastCheckpoint.checkpointID != checkpointID)
        {
            _lastCheckpoint.isSaved = false;
            _lastCheckpoint.DeActivateCheckpoint();
            _lastCheckpoint = this;
        }
    }
}
