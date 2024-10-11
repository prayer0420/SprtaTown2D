using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private CharacterStatsHandler statHandler;

    private float timeSinceLastChange = float.MaxValue;  // 마지막으로 체력 변화가 일어난 시간
    private bool isAttacked = false;

    // 이벤트 선언
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.GetMaxHealth(); // StatSystem에서 최대 체력 가져오기

    private void Awake()
    {
        statHandler = GetComponent<Creature>().statHandler;  // Creature의 StatSystem 참조
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;  // 시작 시 최대 체력으로 설정
    }

    private void Update()
    {
        // 공격을 받은 후 일정 시간 동안 무적 상태 처리
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange > healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    // 체력 변경 메서드 (회복 및 데미지 처리)
    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;  // 무적 상태일 때는 데미지 처리되지 않음
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;  // 체력 변화 적용
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // 사망 처리
        if (CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }

        // 체력 회복
        if (change > 0f)
        {
            OnHeal?.Invoke();
        }
        // 데미지 처리
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
