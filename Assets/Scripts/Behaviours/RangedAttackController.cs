using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangedAttackController : MonoBehaviour 
{
    private MainController controller;

    [SerializeField] private Transform projectilSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    private void Awake()
    {
        controller = GetComponent<MainController>();
    }

    private void Start()
    {
        controller.OnAttackEvent += ShootProjectile;

        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimDirection = (mousePosition - transform.position).normalized;
        }
    }

    public void ShootProjectile(AttackSO attackSO)
    {
        //rangedAttackSO로 형변환 시도 후 null인경우 return
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if(rangedAttackSO == null) return;

        float projectilesAngleSpace = rangedAttackSO.multipleProjectilesAngle; //발사체 당 간격
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;  //한번에 몇 발 나갈지

        //아래서부터 위로 순차적으로 발사
        float minAngle = -((numberOfProjectilesPerShot / 2) * projectilesAngleSpace) + 0.5f * rangedAttackSO.multipleProjectilesAngle;

        for(int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackSO,angle);
        }
    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        GameObject projectile = GameManager.Instance.ObjectPool.SpawnFromPool(rangedAttackSO.bulletNameTag);

        projectile.transform.position = projectilSpawnPosition.position;

        ProjectileController attackController = projectile.GetComponent<ProjectileController>();
        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0f, 0f, angle) * v;
    }

}