using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHornet : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 navVec;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    public GameObject bigHornetBullet;
    float basicSpeed = 900F, speedRate = 1F;
    float vRate = 0F, maxDelta;
    bool upward = false;
    int moveCnt = 0;    
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        vRate = Random.Range(0F, 1F);
        upward = Random.Range(0F, 1F) < 0.5F;
        moveCnt = Random.Range(0F, 1F) < 0.5F ? 0 : 1;
        ResetVec();
        speedRate = Mathf.Lerp(1F, GS.EnemySpeedRate, 0.7F);

    }

    void UpdateDirection()
    {
        animator.SetFloat("X", navVec.x);
        animator.SetFloat("Y", navVec.y);
    }

    private void FixedUpdate()
    {
        UpdateVRate();
        body.AddForce(vec * vRate);
    }

    void ResetVec()
    {
        navVec = EnemyNavigator.NavVec(transform.position);
        float randAngle;
        if ((player.transform.position - transform.position).magnitude >= 4F)
            randAngle = Random.Range(0.5F, 0.7F);
        else
            randAngle = Random.Range(0.45F, 0.55F) * Mathf.PI;
        if (moveCnt % 2 == 1)
            randAngle = -randAngle;
        moveCnt++;
        vec = EnemyNavigator.NavVec(transform.position, randAngle) * basicSpeed;
    }

    IEnumerator ShootCoroutine()
    {
        int shootCnt = 2;
        if (speedRate > 1.18F)
            shootCnt = 3;
        for (int i = 1; i <= shootCnt; i++)
        {
            Shoot();
            yield return new WaitForSeconds(0.5F);
        }
    }

    void Shoot()
    {
        Vector2 shootVec = player.transform.position - transform.position;
        shootVec.Normalize();
        float ang = Mathf.Atan2(shootVec.y, shootVec.x);

        var obj1 = GameObject.Instantiate(bigHornetBullet, transform.position, Quaternion.identity);
        obj1.GetComponent<DirectionAdjustion>().Adjust(ang * Mathf.Rad2Deg);
        obj1.GetComponent<Rigidbody2D>().velocity = shootVec * 7F;

        var obj2 = GameObject.Instantiate(bigHornetBullet, transform.position, Quaternion.identity);
        ang += Mathf.PI / 6F;
        shootVec = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
        obj2.GetComponent<DirectionAdjustion>().Adjust(ang * Mathf.Rad2Deg);
        obj2.GetComponent<Rigidbody2D>().velocity = shootVec * 7F;

        var obj3 = GameObject.Instantiate(bigHornetBullet, transform.position, Quaternion.identity);
        ang -= Mathf.PI / 3F;
        shootVec = new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
        obj3.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(shootVec.y, shootVec.x) * Mathf.Rad2Deg);
        obj3.GetComponent<Rigidbody2D>().velocity = shootVec * 7F;
    }
    void UpdateVRate()
    {
        if (upward)
        {
            vRate += 2.5F * Time.fixedDeltaTime;
            if (vRate > 1)
            {
                upward = false;
                Vector2 dist = (Vector2)(player.transform.position - transform.position);
                dist.x /= 1.3F;
                if (dist.magnitude <= 6F)
                    StartCoroutine(ShootCoroutine());
            }
        }
        else
        {
            vRate -= 2.5F * Time.fixedDeltaTime;
            if (vRate < 0)
            {
                upward = true;
                UpdateDirection();
                ResetVec();
            }
        }

    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        FindObjectOfType<LootSystem>().LootSeed(transform.position, Plant.Type.Goji, 0.06F);
    }
    void Update()
    {

    }
}
