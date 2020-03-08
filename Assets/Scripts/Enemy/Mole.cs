using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    public enum State { Burrow, Peep, Attack};
    public State state = State.Burrow;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    GameObject StonePrefab, TrailPrefab;
    float basicSpeed = 3F;
    float speedRate;
    Animator anim;
    EnemyController ec;

    bool Inbound(Vector2 pos)
    {
        return pos.x <= 10F && pos.x >= -5.5F && pos.y <= 6F && pos.y >= -22F;
    }
    float burrowStartTime;
    bool CanStop()
    {
        return (!Inbound(transform.position)) || (Time.time > burrowStartTime + 2F);
    }

    Vector2 CircleRandPos(Vector2 c,float d)
    {
        float dist = Random.Range(0F, d);
        float angle = Random.Range(0F, 2F) * Mathf.PI;
        return c + dist * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    IEnumerator BurrowTrailCoroutine()
    {
        while(state == State.Burrow)
        {
            yield return new WaitForSeconds(0.05F);
            var o = GameObject.Instantiate(TrailPrefab, transform.position, Quaternion.identity);
            Destroy(o, 1.25F);
        }
    }

    IEnumerator BurrowCoroutine()
    {
        StartCoroutine(BurrowTrailCoroutine());
        Vector2 tgtPos = CircleRandPos(player.transform.position,3F);
        float dist = (tgtPos - (Vector2)transform.position).magnitude;
        Vector2 vec = (tgtPos - (Vector2)transform.position).normalized;
        body.velocity = vec * basicSpeed*speedRate;
        burrowStartTime = Time.time;

        yield return new WaitUntil(CanStop);

        body.velocity = Vector2.zero;
        SetState(State.Peep);
    }

    IEnumerator PeepCoroutine()
    {
        anim.SetInteger("State", 0);
        yield return new WaitForSeconds(2.25F);
        SetState(State.Attack);
    }

    void Throw()
    {
        var obj = GameObject.Instantiate(StonePrefab, transform.position, Quaternion.identity);
        var MS = obj.GetComponent<MoleStone>();
        MS.startPos = transform.position;
        MS.endPos = CircleRandPos(player.transform.position, 3F);
    }
    IEnumerator AttackCoroutine()
    {
        anim.SetInteger("State", 1);
        anim.SetBool("Leftward", transform.position.x > player.transform.position.x);
        for(int i = 1;i<=5;i++)
        {
            yield return new WaitForSeconds(0.3F);
            anim.SetBool("Leftward", transform.position.x > player.transform.position.x);
            Throw();
            yield return new WaitForSeconds(0.3F);
        }
        anim.SetInteger("State", 2);
        yield return new WaitForSeconds(2.25F);
        SetState(State.Burrow);
    }
    public void SetState(State s)
    {
        state = s;
        // Animator State Machine operations here......
        switch (s)
        {
            case State.Burrow:
                Destroy(GetComponent<CircleCollider2D>());
                StartCoroutine(BurrowCoroutine());
                break;
            case State.Peep:
                var cc = gameObject.AddComponent<CircleCollider2D>();
                cc.radius = 0.42F;
                StartCoroutine(PeepCoroutine());
                break;
            case State.Attack:
                StartCoroutine(AttackCoroutine());
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        GS = FindObjectOfType<GameSystem>();
        speedRate = GS.EnemySpeedRate;
        SetState(State.Burrow);
        StonePrefab = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/MoleStone");
        TrailPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Enemy/MoleTrail");
        anim = GetComponent<Animator>();
        ec = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ec.stunned && state != State.Burrow)
            SetState(State.Burrow);
           
    }
}
