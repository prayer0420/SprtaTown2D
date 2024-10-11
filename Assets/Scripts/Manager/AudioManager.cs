using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip Title;
    public AudioClip Game;

    public Scene currentScene;
    public AudioClip newClip = null;


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
        AudioManager.Instance.bgmSource.pitch = 1.0f;

    }
    public void SceneCheck()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();


        // 씬 이름에 따른 처리
        if (currentScene.name == "TitleScene")
        {
            newClip = Title;
            bgmSource.volume = 0.1f;
        }
        else if (currentScene.name == "GameScene")
        {
            newClip = Game;
            bgmSource.clip = Game;
            bgmSource.volume = 0.1f;
            bgmSource.Play();
        }

        if (bgmSource.clip != newClip)
        {
            bgmSource.Stop();
            bgmSource.clip = newClip;
            bgmSource.Play();
            AudioManager.Instance.bgmSource.pitch = 1.0f;
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneCheck();
    }

}
