using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //private static GameManager instance;
    public static GameManager Instance { get; private set; }


    public event Action<string> OnPlayerNameChanged;

    public string PlayerName;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
        Debug.Log("Player name set to: " + PlayerName);
        OnPlayerNameChanged?.Invoke(PlayerName);  
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }

}
