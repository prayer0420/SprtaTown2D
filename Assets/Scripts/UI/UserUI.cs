using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UserUI : MonoBehaviour
{
    public GameObject content; // ScrollView의 Content
    public GameObject userNamePrefab; // TextMeshPro 프리팹

    private Dictionary<Creature, GameObject> characterEntries = new Dictionary<Creature, GameObject>();

    private void OnEnable()
    {
        // UI를 활성화할 때 리스트를 업데이트
//        UpdateUserList();
    }

    private void Start()
    {
        CharacterManager.Instance.OnCharacterAdded += OnCharacterAddedUI;
        UpdateUserList();
    }

    // 캐릭터 추가 시 호출되는 메서드
    private void OnCharacterAddedUI(Creature character)
    {
        AddCharacterEntry(character);
    }

    // UI에 캐릭터 항목 추가
    private void AddCharacterEntry(Creature character)
    {
        if (characterEntries.ContainsKey(character))
        {
            // 이미 존재하는 경우 처리하지 않음
            return;
        }
        GameObject newUser = Instantiate(userNamePrefab, content.transform);
        TextMeshProUGUI userText = newUser.GetComponent<TextMeshProUGUI>();
        userText.text = character.creatureName;

        characterEntries.Add(character, newUser);
        //Debug.Log("유저 추가" + character.creatureName);
        UIManager.Instance.UpdateAllUI();
    }

    // 전체 리스트를 갱신하거나 특정 조건에 따라 업데이트
    public void UpdateUserList()
    {
        // 기존의 모든 UI 항목 제거
        foreach (var entry in characterEntries.Values)
        {
            Destroy(entry);
        }
        characterEntries.Clear();

        // CharacterManager로부터 최신 캐릭터 목록 가져오기
        if (CharacterManager.Instance != null)
        {
            foreach (var character in CharacterManager.Instance.GetCharacters())
            {
                AddCharacterEntry(character);
            }
        }
    }
}
