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

    int stabCnt = 0, coroutineCnt = 0;
    IEnumerator LineStabCoroutine(float basicAngle)
    {
        coroutineCnt++;
        int cnt;
        float dist = 2F, adjAngle;

        Vector2 pos;
        for (cnt = 1; cnt <= 20; cnt++)
        {
            dist += 0.65F;
            adjAngle = Mathf.Sin(dist) * 0.4F;
            float angle = basicAngle + adjAngle * 5F / (dist + 2F);
            for (int i = 1; i <= 4; i++)
            {
                angle += Mathf.PI * 0.5F;
                pos = (Vector2)transform.position + dist * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Stab(pos);
            }
            yield return new WaitForSeconds(0.6F - 0.012F * cnt);
        }
        coroutineCnt--;
    }

    IEnumerator PointStabCoroutine(int maxCnt, float interval)
    {
        coroutineCnt++;
        for(int i = 1;i<=maxCnt;i++)
        {
            Stab(player.transform.position);
            yield return new WaitForSeconds(interval);
        }
        coroutineCnt--;
    }

    bool CoroutinesEnded()
    {
        return coroutineCnt == 0;
    }

    IEnumerator StabCoroutine()
    {
        yield return new WaitForSeconds(1F);
        stabCnt++;
        if (stabCnt == 1)
        {
            StartCoroutine(PointStabCoroutine(24, 0.55F));
        }
        else if (stabCnt == 2)
        {
            StartCoroutine(LineStabCoroutine(Random.Range(0F,Mathf.PI)));
        }
        else if (stabCnt == 3)
        {
            StartCoroutine(LineStabCoroutine(Random.Range(0F, Mathf.PI)));
            StartCoroutine(PointStabCoroutine(24, 0.6F));
        }
        else
        {
            float ba = Random.Range(0F, Mathf.PI);
            StartCoroutine(LineStabCoroutine(ba));
            StartCoroutine(PointStabCoroutine(34, 0.6F));

            yield return new WaitForSeconds(4F);
            StartCoroutine(LineStabCoroutine(ba + Mathf.PI*0.25F));
        }
        yield return new WaitForSeconds(1F);
        yield return new WaitUntil(CoroutinesEnded);
        SetState(State.Idle);

    }

    IEnumerator SummonCoroutine()
    {
        yield return new WaitForSeconds(1.5F);

        GameObject[] trunks = GameObject.FindGameObjectsWithTag("Trunk");
        foreach(var i in trunks)
        {
            if ((i.transform.position - transform.position).magnitude <= 8F)
                i.GetComponent<Trunk>().Summon();
        }

        yield return new WaitForSeconds(1.5F);
        SetState(State.Idle);
    }

    IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2F));

        float phase = 0F;
        while (state == State.Idle)
        {
            UpdateDirection();
            SetVec();
            body.drag = 1F;
            yield return new WaitForSeconds(1.0F / speedRate);

            vec = Vector2.zero;
            body.drag = 10F;
            yield return new WaitForSeconds(0.8F / speedRate);

            float dist = (transform.position - player.transform.position).magnitude;
            phase += 1F / dist;
            if(phase >=1F)
            {
                SetState(State.Summon);
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        body.AddForce(vec * basicSpeed * speedRate);
    }
    void SetVec()
    {
        vecCnt++;
        if (vecCnt % 2 == 0)
            vec = EnemyNavigator.NavVec(transform.position, -0.7F, -0.5F);
        else
            vec = EnemyNavigator.NavVec(transform.position, 0.5F, 0.7F);

        float dist = (transform.position - player.transform.position).magnitude;
        if (dist > 7F)
            vec *= 1F + (dist - 7F) * 0.15F;
    }

    void UpdateDirection()
    {
        return;
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
        SetState(State.Idle);
        GS = FindObjectOfType<GameSystem>();
        speedRate = GS.EnemySpeedRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
