using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 navVec;
    Rigidbody2D body;
    GameObject player;
    public GameObject sg;
    float basicSpeed = 750F,speed;
    float phase,angle;
    float gamma, theta;
    bool moving = false;
    int moveCnt = 0;
    Animator animator;

    Vector2 ToVec(float a) => new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    float ToAng(Vector2 v) => Mathf.Atan2(v.y, v.x);
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
            body.drag = 15F;
            yield return new WaitForSeconds(0.2F);

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
        float ang = ToAng(player.transform.position - transform.position);
        var o = GameObject.Instantiate(sg, transform.position, Quaternion.identity);
        o.GetComponent<SplitedGhost>().movAng = ang + 0.5F * Mathf.PI;

        o = GameObject.Instantiate(sg, transform.position, Quaternion.identity);
        o.GetComponent<SplitedGhost>().movAng = ang - 0.5F * Mathf.PI;
    }

    void ResetVec()
    {
        if ((player.transform.position - transform.position).magnitude <= 4F)
            Split();

        speed = basicSpeed * Random.Range(0.5F, 1.5F);

        angle = EnemyNavigator.NavAng(transform.position);
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
        animator.SetFloat("X", navVec.x);
        animator.SetFloat("Y", navVec.y);
    }

    void Update()
    {

    }

}
