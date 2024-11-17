using UnityEngine;

public static class SaveSystem
{
    #region Checkpoint

    public static Vector3 LastCheckpointPosition = new Vector3(0, -2, 0);
    public static int LastCheckpointID = 0;
    public static Checkpoint LastCheckpoint;

    public static void SaveCheckpointPos(Vector3 position)
    {
        LastCheckpointPosition = position;

        PlayerPrefs.SetFloat("CheckpointX", position.x);
        PlayerPrefs.SetFloat("CheckpointY", position.y + 1f);
        PlayerPrefs.SetFloat("CheckpointZ", 0f);
        PlayerPrefs.Save();

        Debug.Log("Checkpoint Saved: " + position);
    }

    public static Vector3 LoadCheckpointPos()
    {
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            float z = PlayerPrefs.GetFloat("CheckpointZ");
            return new Vector3(x, y, z);
        }

        return LastCheckpointPosition;
    }

    public static void SaveCheckpointID(int checkpointID)
    {
        PlayerPrefs.SetInt("CheckpointID", checkpointID);
    }

    public static int LoadCheckpointID()
    {
        return PlayerPrefs.GetInt("CheckpointID");
    }

    #endregion
}
