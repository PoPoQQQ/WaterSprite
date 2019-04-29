using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 dPos;
    Rigidbody2D body;
    GameObject player;
    public GameObject SplitedGost;
    float speed = 500F;
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
            phase = 0;
            body.drag = 1F;
            yield return new WaitForSeconds(2F);

            moving = false;
            body.drag = 10F;
            yield return new WaitForSeconds(0.5F);

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
    void ResetVec()
    {
        dPos = player.transform.position - transform.position;
        angle = Mathf.Atan2(dPos.y, dPos.x);
        float randAngle = Random.Range(0.005F, 0.007F);
        if (moveCnt % 2 == 1)
            randAngle = -randAngle;
        moveCnt++;
        angle += randAngle;
    }
    void UpdateVec()
    {
        if (moving)
        {
            gamma = Mathf.Max(0F, Mathf.PI * (phase-0.3F));
            if (moveCnt % 2 == 0)
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
