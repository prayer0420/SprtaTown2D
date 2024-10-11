using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    [SerializeField] public string creatureName;
    public CharacterStatsHandler statHandler;
    public Transform MainSpriteObj;
    private HealthSystem healthSystem;

    // Animator Override Controller 추가

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        MainSpriteObj = this.transform.GetChild(0);

    }

    protected virtual void Start()
    {
        // 체력 시스템 초기화 및 이벤트 연결
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
        // 체력 시스템을 통해 데미지 처리
        if (healthSystem.ChangeHealth(-damage))
        {
            Debug.Log($"{GetCreatureName()}이(가) 사망했습니다.");
        }
        else
        {
            Debug.Log($"{GetCreatureName()}이(가) {damage}만큼의 피해를 입었습니다.");
        }
    }

    public virtual void Heal(int amount)
    {
        healthSystem.ChangeHealth(amount);
        Debug.Log($"{GetCreatureName()}이(가) {amount}만큼 치유되었습니다. 현재 체력: {healthSystem.CurrentHealth}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{GetCreatureName()}이(가) 사망했습니다.");
        // 추가적인 사망 처리 로직 작성
    }

    private void HandleDamage()
    {
        Debug.Log($"{GetCreatureName()}이(가) 피해를 입었습니다. 현재 체력: {healthSystem.CurrentHealth}");
    }

    private void HandleHeal()
    {
        Debug.Log($"{GetCreatureName()}이(가) 치유되었습니다. 현재 체력: {healthSystem.CurrentHealth}");
    }
}
