using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileController :MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private bool isReady;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;

    public bool fxOnDestroy = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();

    }

    private void Update()
    {
        if (!isReady)
            return;

        currentDuration += Time.deltaTime;

        //화살 쏜지 오래됐으면(duration보다 크면) 없애고
        if(currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        rb.velocity = direction * attackData.speed;
    }

    private bool IsLyaerMatched(int layerMask, int objectLayer)
    {
        //충돌한 게임오브젝트의 레이어와 지정한 레이어마스크를 OR연산한 것이
        //지정한 레이어와 같다면 알맞게 충돌한 것
        return layerMask == (layerMask | (1<< objectLayer));
    }

    //RangedAttackSO의 데이터에 맞게 바꾸기 
    public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        //크기 바꾸기 
        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;

        spriteRenderer.color = attackData.projectileColor;

        transform.right = this.direction;

        isReady = true;
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if(createFx)
        {
            //Todo

        }
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //벽에 맞으면
        if (IsLyaerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            //벽에 맞은 지점
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
            DestroyProjectile(destroyPosition, fxOnDestroy);
        }
        //몬스터에 맞으면
        else if(IsLyaerMatched(attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                //플레이어의 데미지를 감소시키는 로직이 정상적으로 잘 됐는지 
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power);

                //공격이 잘 들어갔다면 넉백
                if (isAttackApplied && attackData.isOnknockBack)
                {
                    //넉백 적용
                    ApplyKnockback(collision);
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        Movement movement = collision.GetComponent<Movement>();
        if(movement != null)
        {
            movement.ApplyKnockback(transform, attackData.knockbackPower, attackData.knockbackTime);
        }

    }
}