using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
	public GameObject burstPrefab;

    float damage = 1F;

    IEnumerator AddColliderCoroutine()
    {
        yield return new WaitForSeconds(0.1F);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddColliderCoroutine());
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
