using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
	public GameObject burstPrefab;

    float damage = 1F;


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
        if(collision.gameObject.tag == "Enemy")
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (ec)
                ec.Damage(damage);
        }

        GameObject burst = GameObject.Instantiate(burstPrefab, transform.position, Quaternion.identity);
        Destroy(burst, 0.5f);
        Destroy(gameObject);
    }
}
