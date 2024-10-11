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

    // Ư�� ������ ��ȯ�ϴ� �Լ�
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        SetSceneStateBySceneName(sceneName);
    }

    // �񵿱� �� ��ȯ (�ε� �� �ٸ� �۾� ����)
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            // �ε� ������� ǥ���ϰų�, �ε� ȭ�� ó�� ����
            Debug.Log("Loading progress: " + asyncLoad.progress);
            yield return null;
        }
        SetSceneStateBySceneName(sceneName);
    }

    // �� ���� ����
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
        Debug.Log("�� ��ȯ: " + currentSceneState);
    }

    public virtual void OnCharacterSelected(int characterIndex) { }

}
