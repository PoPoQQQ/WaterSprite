using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHedgehog : MonoBehaviour
{
    public enum State { Idle,Shoot,Dash};
    public State state = State.Idle;
    float basicSpeed = 4000F;
    float speedRate = 1F;
    float dashTime = 0F;
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

    bool Dashing() => dashTime > 0F;

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
        dashTime = 1.8F;

        yield return new WaitWhile(Dashing);
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
        animator.SetBool("Shoot",true);
        UpdateDirection();
        for (int i = 1;i<=shootCnt;i++)
        {
            yield return new WaitForSeconds(0.6F);
            Shoot();
        }
        animator.SetBool("Shoot", false);
        SetState(State.Idle);
    }

    int idleCnt = 0;
    IEnumerator IdleCoroutine()
    {
        animator.SetBool("Idle", true);
        yield return new WaitForSeconds(2F);
        int idleMoveCnt = Mathf.FloorToInt(Random.Range(2F, 4.999F));
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", true);
        for (int i = 1; i <= idleMoveCnt; i++)
        {
            SetVec();
            UpdateDirection();
            body.drag = 1F;
            yield return new WaitForSeconds(0.9F / speedRate);

            vec = Vector2.zero;
            body.drag = 4F;
            yield return new WaitForSeconds(0.6F / speedRate);
        }
        idleCnt++;
        animator.SetBool("Walk", false);
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
        dashTime -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            dashTime = 0F;
            FindObjectOfType<CameraShake>().Shake(2F);
            if (collision.gameObject.tag == "Trunk")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x > 0?1:0);
    }

}
