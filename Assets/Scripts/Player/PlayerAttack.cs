using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float shootInterval = 0.5f, bombInterval = 6.5F;
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
        Player.Element e = pl.curElement;
        if (Time.time >= lastShootTime + shootInterval)
        {
            switch(e)
            {
                case Player.Element.Water:
                    WaterShoot();
                    break;
                case Player.Element.Fire:
                    FireShoot();
                    break;
                case Player.Element.Ice:
                    IceShoot();
                    break;
                case Player.Element.Electric:
                    ElectricShoot();
                    break;
            }
            if(!pl.limeBuffed)
            {
                if (e == Player.Element.Electric)
                    pl.CostElem(2F, e);// Cost 2, Return 1 if catched the boomerang
                else
                    pl.CostElem(1F, e);
            }
        }
    }

    void ElectricShoot()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject lb = GameObject.Instantiate(LightningBulletPrefab, transform.position+(Vector3)dir*0.5F, Quaternion.identity);
        lb.GetComponent<LightningBullet>().vec = dir;

        lastShootTime = Time.time;
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
    }
    void IceShoot()
    {
        Debug.Log("ICE SHOOT NOT FINISHED!!!");
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bullet = GameObject.Instantiate(WaterBulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * 12F;
        bullet.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        lastShootTime = Time.time;
    }
    void TryBomb()
    {
        bombInterval = 6.5F - 1F * pl.mulberryBuffCnt;
        Player.Element e = pl.curElement;
        if (Time.time >= lastBombTime + bombInterval && pl.elems[e] >= 5F)
        {
            switch (e)
            {
                case Player.Element.Water:
                    WaterBomb();
                    break;
                case Player.Element.Fire:
                    FireBomb();
                    break;
                case Player.Element.Ice:
                    IceBomb();
                    break;
                case Player.Element.Electric:
                    ElectricBomb();
                    break;
            }
            pl.CostElem(5F, e);
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
    void FireBomb()
    {
        Debug.Log("FIRE BOMB NOT FINISHED!!!");
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bombObj = GameObject.Instantiate(WaterBombPrefab, transform.position, Quaternion.identity);
        var bomb = bombObj.GetComponent<WaterBomb>();
        bomb.startPos = transform.position;
        bomb.endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void IceBomb()
    {
        Debug.Log("ICE BOMB NOT FINISHED!!!");
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.y -= offsetY;
        dir.Normalize();

        GameObject bombObj = GameObject.Instantiate(WaterBombPrefab, transform.position, Quaternion.identity);
        var bomb = bombObj.GetComponent<WaterBomb>();
        bomb.startPos = transform.position;
        bomb.endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    LightningChain LC;
    void ElectricBomb()
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
