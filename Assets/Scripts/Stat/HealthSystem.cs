using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private CharacterStatsHandler statHandler;

    private float timeSinceLastChange = float.MaxValue;  // ���������� ü�� ��ȭ�� �Ͼ �ð�
    private bool isAttacked = false;

    // �̺�Ʈ ����
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.GetMaxHealth(); // StatSystem���� �ִ� ü�� ��������

    private void Awake()
    {
        statHandler = GetComponent<Creature>().statHandler;  // Creature�� StatSystem ����
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;  // ���� �� �ִ� ü������ ����
    }

    private void Update()
    {
        // ������ ���� �� ���� �ð� ���� ���� ���� ó��
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

    // ü�� ���� �޼��� (ȸ�� �� ������ ó��)
    public bool ChangeHealth(float change)
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;  // ���� ������ ���� ������ ó������ ����
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;  // ü�� ��ȭ ����
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        // ��� ó��
        if (CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }

        // ü�� ȸ��
        if (change > 0f)
        {
            OnHeal?.Invoke();
        }
        // ������ ó��
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
