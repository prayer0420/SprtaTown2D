using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player Player { get; set; }  

    [SerializeField] private string playerName;
    public CharacterJobSO selectedJob;
    public CharacterJobSO[] characterJobs;


    public ObjectPool ObjectPool { get; private set; }

    public event Action OnPlayerInfoChanged;

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

    private void Start()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        OnPlayerInfoChanged?.Invoke();
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetSelectedJob(CharacterJobSO job)
    {
        selectedJob = job;
        OnPlayerInfoChanged?.Invoke();
    }

    public CharacterJobSO GetCharacterJob()
    {
        return selectedJob;
    }

    public string GetCharacterJobName()
    {
        return selectedJob != null ? selectedJob.characterJobName : "";
    }

    public Sprite GetCharacterImg()
    {
        
        return selectedJob != null ? selectedJob.characterImage : null;
    }

}
