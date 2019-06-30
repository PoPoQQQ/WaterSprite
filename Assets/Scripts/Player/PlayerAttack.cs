using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float attackSpeed = 0.5f, bombSpeed = 2F;
    public float offsetY = 0f;
    public float shootCost = 0.5F, bombCost = 5F;
    GameObject WaterBulletPrefab, WaterBombPrefab, FireBulletPrefab,LightningBulletPrefab;
    float lastShootTime = -1123F, lastBombTime = -1234F;
    Player pl;
    GameSystem GS;

    void Start()
    {
        LightningBulletPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Lightning/LightningBullet");
        FireBulletPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Fire/Fire");
        WaterBulletPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Water/Bullet");
        WaterBombPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Water/Bomb");
        GS = FindObjectOfType<GameSystem>();
        pl = GetComponent<Player>();
    }

    void TryShoot()
    {
        if (Time.time >= lastShootTime + attackSpeed && pl.health > shootCost * pl.BulletConsumeRate())
            LightningShoot();//FireShoot();//WaterShoot();
    }

    void LightningShoot()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject lb = GameObject.Instantiate(LightningBulletPrefab, transform.position+(Vector3)dir*0.5F, Quaternion.identity);
        lb.GetComponent<LightningBullet>().vec = dir;

        lastShootTime = Time.time;
        pl.CostHealth(2F);
    }
    void FireShoot()
    {
        Debug.Log("Fire!!");
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject fire = GameObject.Instantiate(FireBulletPrefab, transform.position, transform.rotation);
        fire.GetComponent<Fire>().CalcVel(dir);

        lastShootTime = Time.time;
        pl.CostHealth(shootCost * pl.BulletConsumeRate());
    }

    void WaterShoot()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bullet = GameObject.Instantiate(WaterBulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 12F;
        bullet.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lastShootTime = Time.time;
        pl.CostHealth(shootCost * pl.BulletConsumeRate());
    }

    void TryBomb()
    {
        if (Time.time >= lastBombTime + bombSpeed && pl.health > bombCost * pl.BombConsumeRate())
        {
            pl.CostHealth(bombCost * pl.BombConsumeRate());
            LightningBomb();//WaterBomb();
            lastBombTime = Time.time;
        }
    }

    void WaterBomb()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bombObj = GameObject.Instantiate(WaterBombPrefab, transform.position, Quaternion.identity);
        var bomb = bombObj.GetComponent<WaterBomb>();
        bomb.startPos = transform.position;
        bomb.endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    LightningChain LC;
    void LightningBomb()
    {
        if (LC == null)
            LC = new LightningChain();
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        LC.playerPos = transform.position;
        LC.aimPos = (Vector2)transform.position + Vector2.ClampMagnitude(dir, 4);
        LC.MakeChain();
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
