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
        if (collision.gameObject.tag == "Trunk")
        {
            var t = collision.gameObject.GetComponent<Trunk>();
            if (t)
                t.Sprout(0.3F);

        }

        GameObject burst = GameObject.Instantiate(burstPrefab, transform.position, Quaternion.identity);
        Destroy(burst, 0.5f);
        Destroy(gameObject);
    }
}
