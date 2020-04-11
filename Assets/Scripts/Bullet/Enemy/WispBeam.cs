using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispBeam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(5F);
        }
    }
}
