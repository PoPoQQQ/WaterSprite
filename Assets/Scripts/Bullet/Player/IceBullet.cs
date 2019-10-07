using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    float damage = 1.4F;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5F);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec)
                ec.Damage(damage);
        }
        Destroy(gameObject);
    }
}
