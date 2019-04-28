using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10F;
    public float collideDamage = 5F;
    public float collideKnockBack = 24F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashCoroutine()
    {
        const float duration = 0.5f;
        const float frequency = 0.05f;
        float _t = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _t += Time.deltaTime;
            if (_t >= 2 * frequency)
                _t -= 2 * frequency;
            GetComponentInChildren<SpriteRenderer>().enabled = (_t >= frequency);
            yield return 0;
        }
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    void Damage(float damage)
    {
        health -= damage;
        StartCoroutine(FlashCoroutine());
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<Player>();
            p.Damage(collideDamage, (Vector2)(p.transform.position - transform.position).normalized * collideKnockBack);
        }
    }
}
