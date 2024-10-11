using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
public class WeaponSO : EquipmentItemSO
{
    public enum WeaponType
    {
        Melee,
        Ranged
    }

    [Header("Weapon Info")]
    public WeaponType weaponType;
    public int damage;
    public float attackSpeed;

    [Header("Attack Data")]
    public AttackSO attackData;

    // 무기 유형에 따라 공격 방식 결정
    public void PerformAttack(GameObject attacker)
    {
        RangedAttackController shooting = attacker.GetComponent<RangedAttackController>();
        if (shooting == null)
        {
            Debug.LogError("RangedAttackController component missing on attacker.");
            return;
        }

        if (weaponType == WeaponType.Melee)
        {
            // 근접 공격 실행
            PlayMeleeAttack(attacker);
        }
        else if (weaponType == WeaponType.Ranged)
        {
            // 원거리 공격 실행
            shooting.ShootProjectile(attackData);
        }
    }

    private void PlayMeleeAttack(GameObject attacker)
    {
        // 근접 공격 애니메이션 실행
        Animator animator = attacker.GetComponent<Animator>();
        if (animator != null && attackData != null && attackData.anim != null)
        {
            animator.Play(attackData.anim.name);
        }

        // 근접 공격 논리 추가 (데미지 적용 등)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attacker.transform.position, attackData.size);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // 적에게 데미지 적용
                Creature enemyCreature = enemy.GetComponent<Creature>();
                if (enemyCreature != null)
                {
                    enemyCreature.TakeDamage(damage);
                }

                // 넉백 효과 적용
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (enemyRb != null && attackData.isOnknockBack)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - attacker.transform.position).normalized;
                    enemyRb.AddForce(knockbackDirection * attackData.knockbackPower, ForceMode2D.Impulse);
                }
            }
        }
    }

    // 근접 공격 범위 시각화 (디버깅 용도)
    private void OnDrawGizmosSelected()
    {
        if (weaponType == WeaponType.Melee && attackData != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.zero, attackData.size); // 위치를 고정된 값 대신 적절한 위치로 수정 필요
        }
    }
}

public class TopDownShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimDirection = (mousePosition - transform.position).normalized;
        }
    }

    public void ShootProjectile(RangedAttackSO attackData)
    {
        if (attackData == null) return;

        float projectilesAngleSpace = attackData.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = attackData.numberOfProjectilesPerShot;

        float minAngle = -((numberOfProjectilesPerShot / 2) * projectilesAngleSpace) + 0.5f * attackData.multipleProjectilesAngle;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            float randomSpread = UnityEngine.Random.Range(-attackData.spread, attackData.spread);
            angle += randomSpread;
            CreateProjectile(attackData, angle);
        }
    }

    private void CreateProjectile(RangedAttackSO attackData, float angle)
    {
        GameObject projectile = Instantiate(Resources.Load<GameObject>(attackData.bulletNameTag), projectileSpawnPosition.position, Quaternion.identity);
        ProjectileController attackController = projectile.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), attackData);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}