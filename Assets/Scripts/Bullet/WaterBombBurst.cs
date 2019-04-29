using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBombBurst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.6F);
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
                ec.Damage(7F, (ec.transform.position - transform.position).normalized * 800F);
            }

        }
    }
}
