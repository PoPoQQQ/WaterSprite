using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 5F;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10F);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var pl = collision.gameObject.GetComponent<Player>();
            if (pl)
                pl.Damage(damage);
        }

        Debug.Log("Collide!");
        Destroy(gameObject);
    }
}
