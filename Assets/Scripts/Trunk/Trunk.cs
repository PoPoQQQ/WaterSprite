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
    public enum State { Idle, Sprout, Burning, End };
    public State state = State.Idle;
    GameObject EnemyPrefab;
    Animator anim;

    IEnumerator ReviveCoroutine()
    {
        yield return new WaitForSeconds(0.875F);

        GameObject.Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject, 1F);
    }
    public void SetState(State s)
    {
        state = s;
        if (s == State.Idle)
            anim.SetTrigger("Idle");
        if (s == State.Sprout)
            anim.SetTrigger("Sprout");

        if (s == State.Burning)
            Destroy(gameObject, 5F);
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
        if (state != State.Idle)
            return;
        if (Random.Range(0F, 1F) <= p)
            SetState(State.Burning);
    }
    public void Burn() => Burn(1F);

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
