using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10F;
    public float collideDamage = 5F;
    public float collideKnockBack = 24F;
    public bool isBoss = false;
    public bool invincible = false;
    public static int enemyCnt = 0;

    // Loot
    public Plant.Type lootType = Plant.Type.Aquabud;
    public float lootRate = 0.04F;

    Player player;
    GameSystem GS;
    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        player = FindObjectOfType<Player>();
        enemyCnt++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FlashCoroutine()
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        const float duration = 0.2f;
        const float frequency = 0.05f;
        float _t = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            _t += Time.deltaTime;
            if (_t >= 2 * frequency)
                _t -= 2 * frequency;
            sr.enabled = (_t >= frequency);
            yield return 0;
        }
        sr.enabled = true;
        if (health <= 0F)
            Destroy(gameObject);
    }
    public void Damage(float damage, Vector2 knockback, bool isSystemDamage)
    {
        if (!isSystemDamage)
        {
            if (invincible)
                return;
            damage *= player.PlayerDamageRate();
            damage /= GS.EnemyDefRate;
        }
        health -= damage;
        StartCoroutine(FlashCoroutine());
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
    }

    public void Damage(float damage, bool isSystemDamage) => Damage(damage, Vector2.zero,isSystemDamage);
    public void Damage(float damage, Vector2 knockback) => Damage(damage, knockback, false);

    public void Damage(float damage) => Damage(damage, Vector2.zero);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<Player>();
            p.Damage(collideDamage, (Vector2)(p.transform.position - transform.position).normalized * collideKnockBack);
        }
    }
    private void OnDestroy()
    {
        enemyCnt--;
    }
}
