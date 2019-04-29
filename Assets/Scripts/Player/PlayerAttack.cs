﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float attackSpeed = 0.5f, bombSpeed = 5F;
    public float offsetY = 0f;
    public float shootCost = 0.5F, bombCost = 5F;
    public GameObject bulletPrefab, bombPrefab;
    float lastShootTime = -1123F, lastBombTime = -1234F;
    Player pl;
    GameSystem GS;

    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        pl = GetComponent<Player>();
    }

    void TryShoot()
    {
        if(Time.time >= lastShootTime + attackSpeed && pl.health > shootCost * pl.BulletConsumeRate())
            Shoot();
    }
    void Shoot()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 12F;
        bullet.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lastShootTime = Time.time;
        pl.CostHealth(shootCost * pl.BulletConsumeRate());
    }

    void TryBomb()
    {
        if (Time.time >= lastBombTime + bombSpeed && pl.health > bombCost * pl.BombConsumeRate())
            Bomb();
    }
    void Bomb()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bombObj = GameObject.Instantiate(bombPrefab, transform.position, Quaternion.identity);
        var bomb = bombObj.GetComponent<WaterBomb>();
        bomb.startPos = transform.position;
        bomb.endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastBombTime = Time.time;
        pl.CostHealth(bombCost * pl.BombConsumeRate());
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GS.dayOrNight == GameSystem.DayOrNight.Day)
            return;
        if (Input.GetMouseButton(0))
            TryShoot();
        if (Input.GetMouseButton(1))
            TryBomb();
    }
}
