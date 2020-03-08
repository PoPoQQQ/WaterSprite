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
    public bool stunned = false,startingStun = false;
    public float spawnEmitDist = 1.2F;
    GameObject stunnedObj;
    GameObject spriteObj, shadowObj,maskObj;
    public float stunTime = 0F;
    public static int enemyCnt = 0;

    // Loot
    public Plant.Type lootType = Plant.Type.Aquabud;
    public float lootRate = 0.04F;

    Player player;
    GameSystem GS;
    Rigidbody2D body;

    IEnumerator SpawnEmitCoroutine()
    {
        shadowObj.SetActive(false);
        Vector3 basicPos = spriteObj.transform.localPosition;
        spriteObj.transform.localPosition = basicPos + spawnEmitDist * Vector3.down;
        float phase = 1F;
        while (phase >=0F)
        {
            yield return 0;
            phase -= Time.deltaTime;
            spriteObj.transform.localPosition = basicPos + phase * spawnEmitDist * Vector3.down;
        }
        shadowObj.SetActive(true);
        Destroy(maskObj);
    }

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody2D>();
        stunnedObj = transform.Find("Stunned").gameObject;
        stunnedObj.SetActive(false);
        spriteObj = transform.Find("Sprite").gameObject;
        shadowObj = transform.Find("Shadow").gameObject;
        maskObj = transform.Find("SpawnMask").gameObject;
        if (!GetComponent<Mole>()&& !GetComponent<SplitedGhost>() && !GetComponent<SplitedWisp>())
        {
            startingStun = true;
            Stun(1F);
            StartCoroutine(SpawnEmitCoroutine());
            //StartCoroutine(FlashCoroutine(1F));
        }
        enemyCnt++;
    }

    // Update is called once per frame
    void Update()
    {
        if(stunned == true)
        {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0F)
                Unstun();
        }
    }

    IEnumerator FlashCoroutine(float duration = 0.2F)
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
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

    public void Stun(float duration = 1F)
    {
        stunned = true;
        if(!startingStun)
            stunnedObj.SetActive(true);
        stunTime = Mathf.Max(stunTime, duration);
    }

    public void Unstun()
    {
        stunned = false;
        startingStun = false;
        stunnedObj.SetActive(false);
    }

    void FixedUpdate()
    {
        //body.velocity = Vector2.zero;
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
        Debug.Log("!");
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
