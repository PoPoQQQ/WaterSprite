using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 dPos;
    Rigidbody2D body;
    GameObject player;
    public GameObject sg;
    float speed = 600F;
    float phase,angle;
    float gamma, theta;
    bool moving = false;
    int moveCnt = 0;
    Animator animator;

    IEnumerator SetVecCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2.5F));
        while(true)
        {
            moving = true;
            ResetVec();
            UpdateDirection();
            phase = 0;
            body.drag = 1F;
            yield return new WaitForSeconds(2F);

            moving = false;
            body.drag = 10F;
            yield return new WaitForSeconds(0.1F);

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        moving = Random.Range(0F, 1F) < 0.5F;
        moveCnt = Random.Range(0F, 1F) < 0.5F ? 0 : 1;
        StartCoroutine(SetVecCoroutine());
    }

    private void FixedUpdate()
    {
        phase += Time.fixedDeltaTime;
        UpdateVec();
        body.AddForce(vec * speed);
    }

    void Split()
    {
        for(int i = 0;i<4;i++)
        {
            float a = 0.5F * i * Mathf.PI;
            var o = GameObject.Instantiate(sg, transform.position + new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * 0.5F, Quaternion.identity);
            o.GetComponent<SplitedGhost>().angle = a;
        }
        Destroy(gameObject);
    }

    void ResetVec()
    {
        dPos = player.transform.position - transform.position;
        if (dPos.magnitude <= 3F)
            Split();
        angle = Mathf.Atan2(dPos.y, dPos.x);
        float randAngle = Random.Range(0.5F, 0.7F);
        if (moveCnt % 2 == 1)
            randAngle = -randAngle;
        moveCnt++;
        angle += randAngle;
    }
    void UpdateVec()
    {
        if (moving)
        {
            gamma = Mathf.Max(0F, Mathf.PI * (phase-0.5F));
            if (moveCnt % 2 == 1)
                gamma = -gamma;
            theta = angle + gamma;
            vec = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        }
        else
        {
            vec = Vector2.zero;
        }

    }

    void UpdateDirection()
    {
        animator.SetFloat("X", dPos.x);
        animator.SetFloat("Y", dPos.y);
    }

    void Update()
    {

    }
}
