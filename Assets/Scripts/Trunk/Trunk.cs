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
    public static List<GameObject> TrunkList = new List<GameObject>();

    public enum Type { Tree, Root};
    public Type type;
    public enum State { Idle, Sprout, End };
    public bool burning = false;
    public float health = 1F;
    public State state = State.Idle;
    GameObject EnemyPrefab, EvokePrefab;
    Animator anim;
    int deathDate = 0;

    float evokeEffectTime = -1F;
    GameObject evokeObj;
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
        var obj = GameObject.Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<EnemyController>().fromTrunk = true;
        if (obj.GetComponent<TrunkEvoker>())
            Destroy(obj.GetComponent<TrunkEvoker>());
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
        health += p;
        if (state != State.Idle)
            return;
        if (health >= 2F)
        {
            SetState(State.Sprout);
        }
    }
    public void Sprout() => Sprout(1F);

    public void Evoke(GameObject obj)
    {
        if (FindObjectOfType<GameSystem>().dayCnt == deathDate)
            return;
        if (evokeEffectTime > -1F)
            return;

        float dmg =  Random.Range(0.3F, 0.5F);
        health -= dmg;

        evokeEffectTime = dmg * 2F;

        var evokeObj = GameObject.Instantiate(EvokePrefab, transform.position,Quaternion.identity,transform);
        evokeObj.GetComponent<TrunkEvokeEffect>().Set(evokeEffectTime, obj);

        if (state == State.Idle && health<=0F)
        {
            SetState(State.End);
        }
        if (state == State.Sprout && health<=1F)
        {
            SetState(State.Idle);
        }
    }

    public void BossSummon()
    {
        float dmg = Random.Range(0.4F, 0.8F);
        health -= dmg;

        if (state == State.Idle && health <= 0F)
        {
            SetState(State.End);
        }
        if (state == State.Sprout && health <= 1F)
        {
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

    public void DayCheck()
    {
        GameObject[] trunks = GameObject.FindGameObjectsWithTag("Trunk");
        foreach(var i in trunks)
        {
            if (i.transform.position.y <= transform.position.y)
                continue;
            float dist = ((Vector2)(i.transform.position - transform.position)).magnitude;
            if(Random.Range(0F,1F)>dist-1.5F)
                Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        deathDate = FindObjectOfType<GameSystem>().dayCnt;
        anim = transform.Find("Sprite").GetComponent<Animator>();
        if (type == Type.Tree)
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Treeman");
        else
            EnemyPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Rootman");
        EvokePrefab = Resources.Load<GameObject>("Prefabs/TrunkEvokeEffect");
        TrunkList.Add(gameObject);
    }

    private void OnDestroy()
    {
        TrunkList.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        evokeEffectTime -= Time.deltaTime;
    }

}
