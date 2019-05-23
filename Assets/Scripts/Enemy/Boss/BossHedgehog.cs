using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHedgehog : MonoBehaviour
{
    public enum State { Idle,Shoot,Dash};
    public State state = State.Idle;

    void SetState(State s)
    {
        state = s;
        switch(s)
        {
            case State.Idle:

                break;
            case State.Shoot:

                break;
            case State.Dash:

                break;
        }
    }


    IEnumerator IdleCoroutine()
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
