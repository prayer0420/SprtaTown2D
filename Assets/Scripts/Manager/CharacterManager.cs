using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static CharacterManager Instance { get; private set; }

    // �÷��̾�� NPC ���
    private List<Creature> characters = new List<Creature>();

    // ĳ���� �߰�/���� �̺�Ʈ
    public event Action<Creature> OnCharacterAdded;

    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // ĳ���� �߰�
    public void AddCharacter(Creature character)
    {
        characters.Add(character);
        OnCharacterAdded?.Invoke(character);
    }
    // ���� ĳ���� ��� ��ȯ
    public List<Creature> GetCharacters()
    {
        return new List<Creature>(characters);
    }
}
