using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamp : MonoBehaviour
{

    public float strength = 1000F, time = 0.3F;
    float alpha = 1F;
    Rigidbody2D body;
    private void FixedUpdate()
    {
        alpha -= Time.fixedDeltaTime / time;
        body.AddForce(-body.velocity * alpha * strength);
    }



    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, time);
        body = GetComponent<Rigidbody2D>();
        if (!body)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
