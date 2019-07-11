using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleStoneBurst : MonoBehaviour
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

            if (collision.gameObject.tag == "Player")
            {
                var p = collision.gameObject.GetComponent<Player>();
                p.Damage(6F);
            }

    }

}
