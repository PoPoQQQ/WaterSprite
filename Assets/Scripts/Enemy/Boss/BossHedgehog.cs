using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHedgehog : MonoBehaviour
{
    public enum State { Idle,Shoot,Dash};
    public State state = State.Idle;
    float basicSpeed = 4000F;
    float speedRate = 1F;
    Vector2 vec = Vector2.zero;
    GameObject player;
    GameObject bullet;
    Sprite[] bulletSprite;
    Vector2[] bulletVec = {Vector2.up, new Vector2(0.717F,0.717F), Vector2.right, new Vector2(0.717F, -0.717F),
                           Vector2.down, new Vector2(-0.717F,-0.717F), Vector2.left, new Vector2(-0.717F, 0.717F)};
    GameSystem GS;
    EnemyController EC;
    Rigidbody2D body;
    Animator animator;
    int vecCnt = 0;
    void SetState(State s)
    {
        state = s;
        switch(s)
        {
            case State.Idle:
                StartCoroutine(IdleCoroutine());
                break;
            case State.Shoot:
                StartCoroutine(ShootCoroutine());
                break;
            case State.Dash:
                StartCoroutine(DashCoroutine());
                break;
        }
    }

    void SetVec()
    {
        vecCnt++;
        if (vecCnt % 2 == 0)
            vec = EnemyNavigator.NavVec(transform.position, -0.7F, -0.5F);
        else
            vec = EnemyNavigator.NavVec(transform.position, 0.5F, 0.7F);
    }

    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(0.5F);
        vec = (player.transform.position - transform.position).normalized;
        UpdateDirection();
        vec *= 4F;
        body.drag = 1.6F;
        EC.collideDamage = 15F;
        EC.collideKnockBack = 72F;
        animator.SetBool("Dash", true);

        yield return new WaitForSeconds(1.8F);

        body.drag = 6.4F;
        vec = Vector2.zero;
        EC.collideDamage = 5F;
        EC.collideKnockBack = 32F;
        animator.SetBool("Dash", false);
        SetState(State.Idle);
    }

    void Shoot()
    {
        for(int i = 0; i < 8; i++)
        {
            GameObject obj = GameObject.Instantiate(bullet, transform.position + (Vector3)bulletVec[i] * 0.6F, Quaternion.identity);
            obj.transform.Find("SpriteCenter").Find("Sprite").GetComponent<SpriteRenderer>().sprite = bulletSprite[i];
            obj.GetComponent<Rigidbody2D>().velocity = bulletVec[i] * 10.6F;
        }
    }

    IEnumerator ShootCoroutine()
    {
        int shootCnt = 3;
        animator.SetTrigger("Shoot");
        for(int i = 1;i<=shootCnt;i++)
        {
            yield return new WaitForSeconds(0.6F);
            Shoot();
        }
        SetState(State.Idle);
    }

    int idleCnt = 0;
    IEnumerator IdleCoroutine()
    {
        yield return new WaitForSeconds(2F);
        int idleMoveCnt = Mathf.FloorToInt(Random.Range(2F, 4.999F));
        for (int i = 1; i <= idleMoveCnt; i++)
        {
            SetVec();
            UpdateDirection();
            animator.SetBool("Walk", true);
            body.drag = 1F;
            yield return new WaitForSeconds(0.9F / speedRate);

            vec = Vector2.zero;
            animator.SetBool("Walk", false);
            body.drag = 4F;
            yield return new WaitForSeconds(0.6F / speedRate);
        }
        idleCnt++;
        if (idleCnt % 2 == 0)
            SetState(State.Dash);
        else
            SetState(State.Shoot);

    }


    private void FixedUpdate()
    {
        body.AddForce(vec * basicSpeed * speedRate);
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/HedgehogBullet");
        bulletSprite = new Sprite[8];
        for(int i =0;i<8;i++)
        {
            bulletSprite[i] = Resources.Load<Sprite>("Sprite/HedgehogBullets/HB" + (i+1).ToString());
        }
        body = GetComponent<Rigidbody2D>();
        EC = GetComponent<EnemyController>();
        animator = GetComponentInChildren<Animator>();
        SetState(State.Idle);
        GS = FindObjectOfType<GameSystem>();
        speedRate = GS.EnemySpeedRate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }

}
