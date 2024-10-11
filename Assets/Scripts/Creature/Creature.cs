using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] public string creatureName;
    public CharacterStatsHandler statHandler;
    public Transform MainSpriteObj;
    private HealthSystem healthSystem;

    // Animator Override Controller �߰�

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        MainSpriteObj = this.transform.GetChild(0);

    }

    protected virtual void Start()
    {
        // ü�� �ý��� �ʱ�ȭ �� �̺�Ʈ ����
        healthSystem.OnDeath += Die;
        healthSystem.OnDamage += HandleDamage;
        healthSystem.OnHeal += HandleHeal;

    }

    public string GetCreatureName()
    {
        return creatureName;    
    }
    public void SetCreatureName(string creatureName)
    {
        this.creatureName = creatureName;
    }
    public virtual void TakeDamage(int damage)
    {
        // ü�� �ý����� ���� ������ ó��
        if (healthSystem.ChangeHealth(-damage))
        {
            Debug.Log($"{GetCreatureName()}��(��) ����߽��ϴ�.");
        }
        else
        {
            Debug.Log($"{GetCreatureName()}��(��) {damage}��ŭ�� ���ظ� �Ծ����ϴ�.");
        }
    }

    public virtual void Heal(int amount)
    {
        healthSystem.ChangeHealth(amount);
        Debug.Log($"{GetCreatureName()}��(��) {amount}��ŭ ġ���Ǿ����ϴ�. ���� ü��: {healthSystem.CurrentHealth}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{GetCreatureName()}��(��) ����߽��ϴ�.");
        // �߰����� ��� ó�� ���� �ۼ�
    }

    private void HandleDamage()
    {
        Debug.Log($"{GetCreatureName()}��(��) ���ظ� �Ծ����ϴ�. ���� ü��: {healthSystem.CurrentHealth}");
    }

    private void HandleHeal()
    {
        Debug.Log($"{GetCreatureName()}��(��) ġ���Ǿ����ϴ�. ���� ü��: {healthSystem.CurrentHealth}");
    }
}
