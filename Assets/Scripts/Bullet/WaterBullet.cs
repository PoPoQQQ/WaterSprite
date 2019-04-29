using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    float damage = 1F;

    IEnumerator AddColliderCoroutine()
    {
        yield return new WaitForSeconds(0.1F);
        var c = gameObject.AddComponent<CapsuleCollider2D>();
        c.size = new Vector2(0.8F, 0.35F);
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

        Destroy(gameObject);
    }
}
