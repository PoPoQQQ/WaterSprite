using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFlash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.5F);
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
            if (ec)
            {
                ec.Damage(0.1F);
                ec.Stun(3F);
            }
        }
    }
}
