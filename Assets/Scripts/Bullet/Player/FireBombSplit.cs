using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBombSplit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.65F);
    }



    float burnRate = -1F;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec)
            {
                ec.Damage(2F);
                ec.gameObject.AddComponent<WaterDamp>();
            }

        }
        if (collision.gameObject.tag == "Trunk")
        {
            if (burnRate < 0F)
                burnRate = Random.Range(0F, 1F);
            var t = collision.gameObject.GetComponent<Trunk>();
            if (t && burnRate > 0.2F)
                t.Burn();
        }
    }
}
