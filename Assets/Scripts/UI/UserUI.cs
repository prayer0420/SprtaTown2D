using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UserUI : MonoBehaviour
{
    public GameObject content; // ScrollView�� Content
    public GameObject userNamePrefab; // TextMeshPro ������

    private Dictionary<Creature, GameObject> characterEntries = new Dictionary<Creature, GameObject>();

    private void OnEnable()
    {
        if (CharacterManager.Instance != null)
        {
            CharacterManager.Instance.OnCharacterAdded += OnCharacterAddedUI;
        }

        // UI�� Ȱ��ȭ�� �� ����Ʈ�� ������Ʈ
        UpdateUserList();
    }

    // ĳ���� �߰� �� ȣ��Ǵ� �޼���
    private void OnCharacterAddedUI(Creature character)
    {
        AddCharacterEntry(character);
    }

    // UI�� ĳ���� �׸� �߰�
    private void AddCharacterEntry(Creature character)
    {
        if (characterEntries.ContainsKey(character))
        {
            // �̹� �����ϴ� ��� ó������ ����
            return;
        }
        GameObject newUser = Instantiate(userNamePrefab, content.transform);
        TextMeshProUGUI userText = newUser.GetComponent<TextMeshProUGUI>();
        userText.text = character.GetCreatureName(); // �Ǵ� �ʿ��� �ٸ� �Ӽ�

        characterEntries.Add(character, newUser);
        Debug.Log("���� �߰�" + character.GetCreatureName());
        UIManager.Instance.UpdateAllUI();
    }

    // UI���� ĳ���� �׸� ����
    private void RemoveCharacterEntry(Creature character)
    {
        if (characterEntries.TryGetValue(character, out GameObject entry))
        {
            Destroy(entry);
            characterEntries.Remove(character);
        }
    }

    // ��ü ����Ʈ�� �����ϰų� Ư�� ���ǿ� ���� ������Ʈ
    public void UpdateUserList()
    {
        // ������ ��� UI �׸� ����
        foreach (var entry in characterEntries.Values)
        {
            Destroy(entry);
        }
        characterEntries.Clear();

        // CharacterManager�κ��� �ֽ� ĳ���� ��� ��������
        if (CharacterManager.Instance != null)
        {
            foreach (var character in CharacterManager.Instance.GetCharacters())
            {
                AddCharacterEntry(character);
            }
        }
    }
}
