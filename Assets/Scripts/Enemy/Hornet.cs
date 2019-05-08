using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 navVec;
    Rigidbody2D body;
    GameObject player;
    public GameObject hornetBullet;
    float speed = 750F;
    float vRate = 0F, maxDelta;
    bool upward = false;
    int moveCnt = 0;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        vRate = Random.Range(0F, 1F);
        upward = Random.Range(0F, 1F) < 0.5F;
        moveCnt = Random.Range(0F, 1F) < 0.5F ? 0 : 1;
        ResetVec();
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
        float angle = Mathf.Atan2(navVec.y, navVec.x);
        float randAngle = 0F;
        if ((player.transform.position - transform.position).magnitude >= 4F)
            randAngle = Random.Range(0.5F, 0.7F);
        else
            randAngle = Random.Range(0.45F, 0.55F) * Mathf.PI;
        if (moveCnt % 2 == 1)
            randAngle = -randAngle;
        moveCnt++;
        angle += randAngle;
        vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
    }
    void Shoot()
    {
        Vector2 shootVec = player.transform.position - transform.position; ;
        shootVec.Normalize();
        var obj = GameObject.Instantiate(hornetBullet, transform.position, Quaternion.identity);
        obj.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(shootVec.y, shootVec.x) * Mathf.Rad2Deg);
        obj.GetComponent<Rigidbody2D>().velocity = shootVec * 7F;
    }
    void UpdateVRate()
    {
        if (upward)
        {
            vRate += 2F * Time.fixedDeltaTime;
            if (vRate > 1)
            {
                upward = false;
                if (((Vector2)(player.transform.position - transform.position)).magnitude <= 4.5F)
                {
                    Shoot();
                }
            }
        }
        else
        {
            vRate -= 2F * Time.fixedDeltaTime;
            if (vRate < 0)
            {
                upward = true;
                UpdateDirection();
                ResetVec();
            }
        }

    }

    static int dropCnt = 0;
    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        float r = Random.Range(0F, 1F);
        float p = 0.2F;
        if (dropCnt == 0)
            p = 1F;
        if (dropCnt <= 2)
            p = 0.3F;
        if(r <= p)
        {
            SeedItem.Generate(transform.position, Plant.Type.Attack);
            dropCnt++;
        }
    }
    void Update()
    {

    }
}
