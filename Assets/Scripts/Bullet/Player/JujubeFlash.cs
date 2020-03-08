using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JujubeFlash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5F);
        Destroy(GetComponent<CapsuleCollider2D>(), 0.5F);
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
            if(ec)
            {
                ec.Damage(2F, (ec.transform.position - transform.position).normalized * 4200F);
                var wd = ec.gameObject.AddComponent<WaterDamp>();
                wd.strength = 1000F;
                wd.time = 0.6F;
            }

        }
    }
}
