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
    public enum State { Start, Idle, Burning, End };
    public State state = State.Start;

    // Sprout state = 0, 1, 2. Only used in Idle state.
    public int sproutState = 0;

    public void SetState(State s)
    {
        state = s;
        // Animator State Machine operations here......

        if (s == State.Burning)
            Destroy(gameObject, 5F);
        if (s == State.End)
            Destroy(gameObject, 1F);
    }

    public void Sprout(float p)
    {
        if (sproutState >= 2 || state != State.Idle)
            return;
        if (Random.Range(0F, 1F) <= p)
        {
            sproutState++;
            // Animator State Machine operations here......
        }
    }
    public void Sprout() => Sprout(1F);

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
