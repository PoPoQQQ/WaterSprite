using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public float moveSpeed = 40f;
    public float health = 50F;
    public int state = 1;
    public int maxState = 1;
    Rigidbody2D body;
    PlayerMoving PM;
    PlayerEffects PE;
    public int atkBuffCnt = 0;
    public int csmBuffCnt = 0;
    public HPBar bar;
    public GameObject shadow;
    public Image splash;
    public Sprite[] splashSprites;
    public Image ending;
    public GameObject curtain;
    GameSystem GS;
    public Text endingtext;

    public float PlayerDamageRate()
    {
        return 1F + 0.5F * atkBuffCnt;
    }

    public float BulletConsumeRate()
    {
        return Mathf.Pow(0.6F, csmBuffCnt);
    }
    public float BombConsumeRate()
    {
        return Mathf.Pow(0.7F, csmBuffCnt);
    }

    IEnumerator PlaySplash()
    {
        int index = 0;
        while(true)
        {
            splash.sprite = splashSprites[index++];
            if(index == splashSprites.Length)
                break;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        ending.DOColor(new Color(1, 1, 1, 1), 3f);
        yield return new WaitForSeconds(4f);
        endingtext.text = "You've been survived for "+ GameObject.Find("GameManager").GetComponent<GameSystem>().dayCnt+" day(s)";
        endingtext.gameObject.SetActive(true);
        curtain.GetComponent<FadingCurtain>().StartCoroutine(curtain.GetComponent<FadingCurtain>().fading(70));
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("intro");
    }


    void GameOver()
    {
        Debug.Log("GameOver!");
        GetComponentInChildren<Animator>().SetBool("Dead", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        shadow.SetActive(false);
        StartCoroutine(PlaySplash());
        PM.enabled = false;
    }

    void ChangeMaxState(int _maxState)
    {
        maxState = _maxState;
        bar.updateHealth(_maxState*100, health);
    }
    void ChangeState(int _state)
    {
        if (_state > maxState)
            ChangeMaxState(_state);
        state = _state;
        GetComponentInChildren<Animator>().SetInteger("Form", _state);
    }

    void Check()
    {
    	bar.updateHealth(health);
        if (health <= 0F)
            GameOver();
        else
        {
            int _state = 1;
            if (health < 100F)
                _state = 1;
            else if (health < 200F)
                _state = 2;
            else
                _state = 3;

            if (_state != state)
                ChangeState(_state);
        }
    }

    public void Damage(float damage, Vector2 KnockBack)
    {
        damage *= 0.9F + 0.1F * GS.dayCnt;
        health -= damage;
        body.AddForce(KnockBack, ForceMode2D.Impulse);
        PE.Hurt();
        Check();
    }
    public void Damage(float damage) => Damage(damage, Vector2.zero);

    public void CostHealth(float cost)
    {
        health -= cost;
        Check();
    }

    public void AddHealth(float h)
    {
        health += h;
        if (health > 300)
            health = 300;
        Check();
    }
    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        body = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerMoving>();
        PE = GetComponent<PlayerEffects>();
        bar = FindObjectOfType<HPBar>();
        bar.updateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
