using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hornet : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 dPos;
    Rigidbody2D body;
    GameObject player;
    public GameObject hornetBullet;
    float speed = 700F;
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
        animator.SetFloat("X", dPos.x);
        animator.SetFloat("Y", dPos.y);
    }

    private void FixedUpdate()
    {
        UpdateVRate();
        body.AddForce(vec * vRate);
    }

    void ResetVec()
    {
        dPos = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dPos.y, dPos.x);
        float randAngle = 0F;
        if (dPos.magnitude >= 4F)
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
        dPos = player.transform.position - transform.position;
        dPos.Normalize();
        var obj = GameObject.Instantiate(hornetBullet, transform.position, Quaternion.identity);
        obj.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(dPos.y, dPos.x) * Mathf.Rad2Deg);
        obj.GetComponent<Rigidbody2D>().velocity = dPos * 7F;
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

    private void OnDestroy()
    {
        float r = Random.Range(0F, 1F);
        if(r<= 0.3F)
            SeedItem.Generate(transform.position, Plant.Type.Attack);
    }
    void Update()
    {

    }
}
