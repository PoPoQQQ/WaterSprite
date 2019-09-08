using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            var b = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 v = b.transform.position - transform.position;
            v.Normalize();
            b.AddForce(v * 250F, ForceMode2D.Impulse);
        }
    }
}
