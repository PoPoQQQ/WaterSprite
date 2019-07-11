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
    GameObject StonePrefab;
    float basicSpeed = 4F;
    float speedRate;

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
    IEnumerator BurrowCoroutine()
    {
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
        yield return new WaitForSeconds(1.5F);
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
        for(int i = 1;i<=5;i++)
        {
            yield return new WaitForSeconds(0.3F);
            Throw();
            yield return new WaitForSeconds(0.3F);
        }
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
                GetComponent<SpriteRenderer>().color = new Color(1F, 1F, 1F, 0.4F);
                StartCoroutine(BurrowCoroutine());
                break;
            case State.Peep:
                var cc = gameObject.AddComponent<CircleCollider2D>();
                cc.radius = 0.42F;
                GetComponent<SpriteRenderer>().color = Color.white;
                StartCoroutine(PeepCoroutine());
                break;
            case State.Attack:
                GetComponent<SpriteRenderer>().color = new Color(1F,0.6F,0.6F);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
