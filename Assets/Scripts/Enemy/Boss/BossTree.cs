using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTree : MonoBehaviour
{
    public enum State { Idle, Stab, Summon};
    public State state = State.Idle;

    Vector2 vec = Vector2.zero;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    float basicSpeed = 6000F;
    float speedRate;
    int vecCnt = 0;
    Animator animator;
    GameObject TreeStabPrefab;

    void SetState(State s)
    {
        state = s;
        switch(state)
        {
            case State.Idle:
                StartCoroutine(MoveCoroutine());
                break;
            case State.Stab:
                StartCoroutine(StabCoroutine());
                break;
            case State.Summon:
                StartCoroutine(SummonCoroutine());
                break;
        }
    }

    void Stab(Vector2 pos)
    {
        GameObject.Instantiate(TreeStabPrefab, pos, Quaternion.identity);
    }

    IEnumerator LineStabCoroutine()
    {
        int cnt;
        float dist =2F, basicAngle, adjAngle;
        basicAngle = Random.Range(0F, 2F) * Mathf.PI;
        Vector2 pos;
        for(cnt = 1;cnt<=15;cnt++)
        {
            dist += (1F-0.03F*cnt) * 0.6F;
            adjAngle = Mathf.Sin(dist) * 0.5F;
            float angle = basicAngle + adjAngle;
            for(int i =1;i<=3;i++)
            {
                angle += Mathf.PI * 0.6667F;
                pos = (Vector2)transform.position + dist * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Stab(pos);
            }
            yield return new WaitForSeconds((1F - 0.03F * cnt) * 0.8F);
        }
    }

    IEnumerator PointStabCoroutine(int maxCnt, float interval)
    {
        for(int i = 1;i<=maxCnt;i++)
        {
            Stab(player.transform.position);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator StabCoroutine()
    {
    	yield return 0;
    }

    IEnumerator SummonCoroutine()
    {
    	yield return 0;
    }
    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2F));
        while (state == State.Idle)
        {
            UpdateDirection();
            SetVec();
            body.drag = 1F;
            yield return new WaitForSeconds(1.0F / speedRate);

            vec = Vector2.zero;
            body.drag = 10F;
            yield return new WaitForSeconds(0.8F / speedRate);
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

    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        TreeStabPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/TreeStab");
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(MoveCoroutine());
        GS = FindObjectOfType<GameSystem>();
        speedRate = GS.EnemySpeedRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
