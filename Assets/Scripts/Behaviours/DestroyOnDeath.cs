using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rb;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rb = GetComponent<Rigidbody2D>();
        healthSystem.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        //캐릭터 반투명
        //컴퍼넌트 다 꺼주기
        rb.velocity = Vector2.zero;
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            //복사하고 값 넣고, 원본에 적용시킴
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach(Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = false;
        }

        //2초 뒤 파괴
        Destroy(gameObject, 2f);
    }
}
