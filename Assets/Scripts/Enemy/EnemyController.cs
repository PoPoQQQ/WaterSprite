using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10F,maxHealth;
    public float collideDamage = 5F;
    public float collideKnockBack = 24F;
    public bool isBoss = false;
    public bool invincible = false;
    public bool stunned = false,startingStun = false;
    public bool fromTrunk = false;
    public float spawnEmitDist = 1.2F;
    GameObject stunnedObj;
    GameObject spriteObj, shadowObj,maskObj;
    GameObject EnemyHealthbarPrefab, Healthbar;
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
        if(shadowObj)
            shadowObj.SetActive(false);
        Vector3 basicPos = spriteObj.transform.localPosition;
        if(spriteObj)
            spriteObj.transform.localPosition = basicPos + spawnEmitDist * Vector3.down;
        float phase = 1F;
        while (phase >=0F)
        {
            yield return 0;
            phase -= Time.deltaTime;
            spriteObj.transform.localPosition = basicPos + phase * spawnEmitDist * Vector3.down;
        }
        if (shadowObj)
            shadowObj.SetActive(true);
        if(maskObj)
            Destroy(maskObj);
    }

    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody2D>();
        if (transform.Find("Stunned"))
        {
            stunnedObj = transform.Find("Stunned").gameObject;
            stunnedObj.SetActive(false);
        }
        if (transform.Find("Sprite"))
            spriteObj = transform.Find("Sprite").gameObject;
        if (transform.Find("Shadow"))
            shadowObj = transform.Find("Shadow").gameObject;
        if (transform.Find("SpawnMask"))
            maskObj = transform.Find("SpawnMask").gameObject;
        if (!GetComponent<Mole>()&& !GetComponent<SplitedGhost>() && !GetComponent<SplitedWisp>()
            && !fromTrunk && !isBoss)
        {
            startingStun = true;
            Stun(1F);
            StartCoroutine(SpawnEmitCoroutine());
        }

        EnemyHealthbarPrefab = Resources.Load<GameObject>("Prefabs/Enemies/EnemyHealthbar");
        Healthbar = GameObject.Instantiate(EnemyHealthbarPrefab, Vector3.zero, Quaternion.identity);
        Healthbar.GetComponent<EnemyHealthbar>().Init(gameObject, isBoss, spawnEmitDist);
        maxHealth = health;
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
        if(!startingStun && stunnedObj)
            stunnedObj.SetActive(true);
        stunTime = Mathf.Max(stunTime, duration);
    }

    public void Unstun()
    {
        stunned = false;
        startingStun = false;
        if(stunnedObj)
            stunnedObj.SetActive(false);
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
        Debug.Log("damage:"+damage.ToString());
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        Healthbar.GetComponent<EnemyHealthbar>().Set(health / maxHealth);
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
        if (GetComponent<EnemyController>().health > 0)
            return;
        FindObjectOfType<LootSystem>().LootSeed(transform.position, lootType, lootRate);
    }
}
