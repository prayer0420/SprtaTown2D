using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    Title,
    Game,
    Town
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    public SceneState currentSceneState = SceneState.Title;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    // 특정 씬으로 전환하는 함수
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        SetSceneStateBySceneName(sceneName);
    }

    // 비동기 씬 전환 (로딩 중 다른 작업 가능)
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            // 로딩 진행률을 표시하거나, 로딩 화면 처리 가능
            Debug.Log("Loading progress: " + asyncLoad.progress);
            yield return null;
        }
        SetSceneStateBySceneName(sceneName);
    }

    // 씬 상태 설정
    private void SetSceneStateBySceneName(string sceneName)
    {
        switch (sceneName)
        {
            case "TitleScene":
                SetSceneState(SceneState.Title);
                break;
            case "GameScene":
                SetSceneState(SceneState.Game);
                break;
            case "TownScene":
                SetSceneState(SceneState.Town);
                break;
            default:
                Debug.LogWarning("Unknown scene: " + sceneName);
                break;
        }
    }

    public void SetSceneState(SceneState state)
    {
        currentSceneState = state;
        Debug.Log("씬 변환: " + currentSceneState);
    }

    public virtual void OnCharacterSelected(int characterIndex) { }

}
