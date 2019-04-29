﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMan : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    float speed = 600F;
    int vecCnt = 0;
    Animator animator;

    IEnumerator ChangeVecCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2F));
        while(true)
        {
            UpdateDirection();
            SetVec();
            yield return new WaitForSeconds(0.6F);

            vec = Vector2.zero;
            body.drag = 10F;
            yield return new WaitForSeconds(0.4F);
            body.drag = 1F;
        }
    }

    void SetVec()
    {
        if (!player)
        {
            vec = Vector2.zero;
            return;
        }
        Vector2 dPos = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dPos.y, dPos.x);
        vecCnt++;
        if (vecCnt % 2 == 0)
            angle += Random.Range(0.5F, 0.7F);
        else
            angle -= Random.Range(0.5F, 0.7F);
        vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(ChangeVecCoroutine());
        GS = FindObjectOfType<GameSystem>();
    }

    private void FixedUpdate()
    {
        body.AddForce(vec);
    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        if (GS.dayCnt == 1)
        {
            if (Random.Range(0F, 1F) <= 0.5F)
                SeedItem.Generate(transform.position, Plant.Type.Water);
        }
        else
        {
            if (Random.Range(0F, 1F) <= 0.2F)
                SeedItem.Generate(transform.position, Plant.Type.Water);
        }
    }


    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }

    void Update()
    {
        
    }
}
