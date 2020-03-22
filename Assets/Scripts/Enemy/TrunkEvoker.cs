using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkEvoker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach(var i in Trunk.TrunkList)
       {
            float dist = ((Vector2)(i.transform.position - transform.position)).magnitude;
            float p = 1F * Time.deltaTime * (7F - dist) / 8F;
            if (Random.Range(0F,1F)<p)
            {
                var tr = i.GetComponent<Trunk>();
                tr.Evoke(gameObject);
            }
       }
    }
}
