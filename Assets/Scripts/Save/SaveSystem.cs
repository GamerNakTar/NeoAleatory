using UnityEngine;

public static class SaveSystem
{
    public static Vector3 lastCheckpointPosition = new Vector3(0, -2, 0);

    public static void SaveCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
        
        PlayerPrefs.SetFloat("CheckpointX", position.x);
        PlayerPrefs.SetFloat("CheckpointY", position.y);
        PlayerPrefs.SetFloat("CheckpointZ", 0f);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint Saved: " + position);
    }

    public static Vector3 LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            return new Vector3(x, y, z);
        }

        return lastCheckpointPosition;
    }
}
