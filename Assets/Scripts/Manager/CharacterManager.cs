using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static CharacterManager Instance { get; private set; }

    // 플레이어와 NPC 목록
    private List<Creature> characters = new List<Creature>();

    // 캐릭터 추가/삭제 이벤트
    public event Action<Creature> OnCharacterAdded;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // 캐릭터 추가
    public void AddCharacter(Creature character)
    {
        characters.Add(character);
        OnCharacterAdded?.Invoke(character);
    }
    // 현재 캐릭터 목록 반환
    public List<Creature> GetCharacters()
    {
        return new List<Creature>(characters);
    }
}
