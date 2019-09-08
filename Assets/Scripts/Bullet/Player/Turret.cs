using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float r = 4F;

    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float d = r;
        GameObject t = null;
        foreach(GameObject i in enemies)
        {
            float dist = ((Vector2)(i.transform.position - transform.position)).magnitude;
            if(dist<d)
            {
                t = i;
                d = dist;
            }
        }
        return t;
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
