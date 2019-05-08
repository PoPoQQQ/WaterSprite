using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombBurst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.6F);
        Destroy(GetComponent<CapsuleCollider2D>(), 0.15F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            float dist = (transform.position - collision.transform.position).magnitude;
            float dmg = 8F * (1.0F - 0.2F * dist);
            if(ec)
            {
                ec.Damage(7F, (ec.transform.position - transform.position).normalized * 800F);
            }

        }
    }
}
