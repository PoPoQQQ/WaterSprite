using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBombBurst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.625F);
        Destroy(GetComponent<CapsuleCollider2D>(), 0.15F);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            float dist = (transform.position - collision.transform.position).magnitude;
            float dmgRate = (1.0F - 0.2F * dist);
            if (ec)
            {
                ec.Damage(9F * dmgRate, (ec.transform.position - transform.position).normalized * 2200F * dmgRate);
                ec.gameObject.AddComponent<WaterDamp>();
            }

        }
    }
}
