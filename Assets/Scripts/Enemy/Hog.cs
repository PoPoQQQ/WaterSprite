﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hog : MonoBehaviour
{

    public enum State { Idle, Dash};
    public State state = State.Idle;
    int dashCnt = 0;
    float dashTime = 0F, maxDashTime = 1.3F;
    float basicSpeed = 1400F;
    Vector2 dashVec;
    Rigidbody2D body;
    GameObject player;
    EnemyController ec;

    void SetState(State s)
    {
        state = s;
        switch(s)
        {
            case State.Dash:
                dashCnt = 0;
                ResetDash();
                break;
            case State.Idle:
                StartCoroutine(IdleCoroutine());
                break;
        }
    }


    IEnumerator IdleCoroutine()
    {
        yield return new WaitForSeconds(3F);
        while(ec.stunned)
        {
            yield return new WaitForSeconds(0.5F);
        }
        SetState(State.Dash);
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(State.Idle);
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        ec = GetComponent<EnemyController>();
    }

    void ResetDash()
    {
        if (dashCnt < 3F)
        {
            maxDashTime = Random.Range(1F, 1.5F);
            dashTime = 0F;
            dashVec = player.transform.position - transform.position;
            dashVec.Normalize();
            dashVec *= basicSpeed;
            dashCnt++;
        }
        else
            SetState(State.Idle);
    }

    private void FixedUpdate()
    {

        if (state == State.Dash)
        {
            if (dashTime <= maxDashTime * 0.8F)
                body.AddForce(dashVec);
            else
                body.AddForce(body.velocity * -600F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Dash)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= maxDashTime)
                ResetDash();
            if (ec.stunned)
                SetState(State.Idle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            SetState(State.Idle);
        }
    }
}
