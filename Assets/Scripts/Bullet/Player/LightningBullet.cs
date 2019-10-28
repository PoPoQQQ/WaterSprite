using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : MonoBehaviour
{
    float damage = 1F;
    float phase = 1F, basicVel = 10F;
    public Vector2 vec;

    Rigidbody2D body;
    private void FixedUpdate()
    {
        if (phase > -1.5F)
            phase -= Time.fixedDeltaTime;
        body.velocity = vec * basicVel * phase;
    }


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
                ec.Damage(damage);
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (phase > 0)
                return;
            Debug.Log("PL");
            var pl = collision.gameObject.GetComponent<Player>();
            if(pl)
            {
                if (pl.limeBuffTime == 0F)
                    pl.AddHealth(1);
                Destroy(gameObject,0.1F);
            }
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
