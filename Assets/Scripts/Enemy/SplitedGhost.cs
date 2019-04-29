﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplitedGhost : MonoBehaviour
{
    Vector2 startPos,endPos;
    Rigidbody2D body;
    GameObject player;
    float speed = 700F;
    public float angle;
    int vecCnt = 0;
    Animator animator;
    IEnumerator ChangeVecCoroutine()
    {
        while (true)
        {
            UpdateDirection();
            endPos = (Vector2)player.transform.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 2F;
            transform.DOMove(endPos, 2F);
            angle += 0.5F *Mathf.PI;
            yield return new WaitForSeconds(2F);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(ChangeVecCoroutine());
    }

    static int dropCnt = 0;
    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        float r = Random.Range(0F, 1F);
        if (r <= 0.1F || dropCnt == 0)
            SeedItem.Generate(transform.position, Plant.Type.Consume);
        dropCnt++; 
    }
    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }
}
