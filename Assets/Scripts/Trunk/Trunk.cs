using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkGenerator
{
    static GameObject TrunkPrefab;
    public static void Generate(Trunk.Type type, Vector3 pos)
    {
        if (TrunkPrefab == null)
            TrunkPrefab = Resources.Load<GameObject>("Prefabs/Trunk");
        var obj = GameObject.Instantiate(TrunkPrefab, pos, Quaternion.identity);
        obj.GetComponent<Trunk>().type = type;
    }
}

public class Trunk : MonoBehaviour
{
    public enum Type { Tree, Root};
    public Type type;
    public enum State { Idle, Sprout, End };
    public bool burning = false;
    public State state = State.Idle;
    GameObject EnemyPrefab;
    Animator anim;

    IEnumerator FlashCoroutine()
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        const float duration = 1f;
        const float frequency = 0.05f;
        float _t = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _t += Time.deltaTime;
            if (_t >= 2 * frequency)
                _t -= 2 * frequency;
            sr.enabled = (_t >= frequency);
            yield return 0;
        }
        sr.enabled = true;
    }
    IEnumerator ReviveCoroutine()
    {
        yield return new WaitForSeconds(0.875F);
        Destroy(GetComponent<CapsuleCollider2D>());
        GameObject.Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    IEnumerator BurnCoroutine()
    {
        burning = true;
        var bsr = transform.Find("Fire").GetComponent<SpriteRenderer>();
        bsr.color = Color.white;
        yield return new WaitForSeconds(1.75F);
        if(Random.Range(0F,1F)<=0.4F)
        {
            Destroy(gameObject, 1F);
            StartCoroutine(FlashCoroutine());
        }

        yield return new WaitForSeconds(1.75F);
        bsr.color = new Color(1F,1F,1F,0F);
        burning = false;
    }
    public void SetState(State s)
    {
        state = s;
        if (s == State.Idle)
            anim.SetTrigger("Idle");
        if (s == State.Sprout)
            anim.SetTrigger("Sprout");

        if (s == State.End)
        {
            StartCoroutine(ReviveCoroutine());
            anim.SetTrigger("End");
        }
    }

    public void Sprout(float p)
    {
        if (state != State.Idle)
            return;
        if (Random.Range(0F, 1F) <= p)
        {
            SetState(State.Sprout);
        }
    }
    public void Sprout() => Sprout(1F);

    public void Summon()
    {
        if (state == State.Idle)
        {
            SetState(State.End);
        }
        if (state == State.Sprout)
        {
            if (Random.Range(0F, 1F) <= 0.5F)
                SetState(State.Idle);
        }
    }

    public void Burn(float p)
    {
        if (state == State.End || burning)
            return;
        if (Random.Range(0F, 1F) <= p)
        {
            StartCoroutine(BurnCoroutine());
        }
    }
    public void Burn() => Burn(1F);

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.Find("Sprite").GetComponent<Animator>();
        if (type == Type.Tree)
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Treeman");
        else
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Rootman");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
