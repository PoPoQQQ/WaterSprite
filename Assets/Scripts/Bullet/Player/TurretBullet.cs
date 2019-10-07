using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public GameObject target;
    public Vector2 vec;
    Rigidbody2D body;
    float damage = 1F;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5F);
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dVec = (target.transform.position - transform.position).normalized;
        vec += (dVec - vec.normalized) * 3F * Time.deltaTime;
        body.velocity = vec;
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

    private void OnDestroy()
    {
        GameObject b = Resources.Load<GameObject>("Prefabs/Ammo/Player/TurretBulletBurst");
        GameObject burst = GameObject.Instantiate(b, transform.position, Quaternion.identity);
        Destroy(burst, 0.5f);
    }
}
