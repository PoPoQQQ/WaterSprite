﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 navVec;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    GameObject boomerang;
    float basicSpeed = 300F, speedRate = 1F;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        boomerang = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/Boomerang");

        speedRate = Mathf.Lerp(1F, GS.EnemySpeedRate, 0.7F);

    }

    void UpdateDirection()
    {
        animator.SetFloat("X", navVec.x);
        animator.SetFloat("Y", navVec.y);
    }

    private void FixedUpdate()
    {
        float dist = (player.transform.position - transform.position).magnitude;
        if (dist >= 4F)
        {
            vec = EnemyNavigator.NavVec(transform.position) * basicSpeed * speedRate;
        }
        else
            vec = Vector2.zero;
        if (Time.time >= lastShootTime + 4F && dist <=5F)
            StartCoroutine(ShootCoroutine());
        body.AddForce(vec);
    }


    float lastShootTime = 0F;
    IEnumerator ShootCoroutine()
    {
        lastShootTime = Time.time;
        int shootCnt = 1;
        if (speedRate > 1.18F)
            shootCnt = 2;
        for (int i = 1; i <= shootCnt; i++)
        {
            Shoot();
            yield return new WaitForSeconds(0.8F);
        }
    }

    void Shoot()
    {
        Vector2 shootVec = player.transform.position - transform.position;
        shootVec.Normalize();
        var obj = GameObject.Instantiate(boomerang, transform.position, Quaternion.identity);
        obj.GetComponent<Boomerang>().basicVec = shootVec;
        obj.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(shootVec.y, shootVec.x) * Mathf.Rad2Deg);
    }

    void Update()
    {
    }
}