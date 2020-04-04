using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkEvoker : MonoBehaviour
{
    float startTime, lastEvokeTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > 16F && Time.time - lastEvokeTime > 1F)
        {

            foreach (var i in Trunk.TrunkList)
            {
                float dist = ((Vector2)(i.transform.position - transform.position)).magnitude;
                float p = 1F * Time.deltaTime * (7F - dist) / 8F;
                if (Random.Range(0F, 1F) < p)
                {
                    var tr = i.GetComponent<Trunk>();
                    tr.Evoke(gameObject);
                    lastEvokeTime = Time.time;
                }
            }
        }
    }
}
