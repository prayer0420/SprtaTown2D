using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager Instance;

    public CharacterJobSO[] characterJobs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public CharacterJobSO GetJob(int index)
    {
        if (index >= 0 && index < characterJobs.Length)
        {
            return characterJobs[index];
        }
        return null;
    }
}