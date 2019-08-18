using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //-------------Elements-----------
    public enum Element { Water, Fire, Ice, Electric};
    public Dictionary<Element, float> elems = new Dictionary<Element, float>();
    public Element curElement = Element.Water;
    public Element NextElement(Element e)
    {
        switch(e)
        {
            case Element.Water:
                return Element.Fire;
            case Element.Fire:
                return Element.Ice;
            case Element.Ice:
                return Element.Electric;
            case Element.Electric:
                return Element.Water;
        }
        return Element.Water;
    }
    public Element PrevElement(Element e)
    {
        switch(e)
        {
            case Element.Water:
                return Element.Electric;
            case Element.Fire:
                return Element.Water;
            case Element.Ice:
                return Element.Fire;
            case Element.Electric:
                return Element.Ice;
        }
        return Element.Water;
    }

    public float TotalElement()
    {
        return elems[Element.Water] + elems[Element.Fire] + elems[Element.Ice] + elems[Element.Electric];
    }

    //-------------Values-------------
    public float moveSpeed = 40f;
    public int state = 1;
    public int maxState = 1;

    //------------- Buffs -------------
    public int gojiBuffCnt = 0;
    public int mulberryBuffCnt = 0;
    public int wisplumBuffCnt = 0;
    public bool limeBuffed = false;
    public bool cloudberryBuffed = false;

    //-------------UI & Graphics-------------
    public HPBar bar;
    public GameObject shadow;
    public Image splash;
    public Sprite[] splashSprites;
    public Image ending;
    public GameObject curtain;
    public Text endingtext;

    // -------------- External --------------
    GameSystem GS;
    Rigidbody2D body;
    PlayerMoving PM;
    PlayerEffects PE;

    public float PlayerDamageRate()
    {
        return 1F + 0.5F * gojiBuffCnt;
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
        endingtext.text = "You've survived for "+ GameObject.Find("GameManager").GetComponent<GameSystem>().dayCnt+" day(s)";
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
        bar.updateHealth(_maxState*100, TotalElement());
    }
    void ChangeState(int _state)
    {
        if (_state > maxState)
            ChangeMaxState(_state);
        state = _state;
        GetComponentInChildren<Animator>().SetInteger("Form", _state);
    }

    void Check() // ----- Need To Rewrite!!!! -----
    {
        /*
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
        */
    }

    public void Damage(float damage, Vector2 KnockBack)
    {
        damage *= GS.EnemyAtkRate;
        elems[Element.Water] -= damage;
        body.AddForce(KnockBack, ForceMode2D.Impulse);
        PE.Hurt();
        Check();
    }
    public void Damage(float damage) => Damage(damage, Vector2.zero);

    public void CostElem(float cost,Element e)
    {
        elems[e] -= cost;
        Check();
    }

    public void AddElem(float h, Element e)
    {
        elems[e] += h;
        Check();
    }
    public void Refresh()
    {
        limeBuffed = cloudberryBuffed = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        elems[Element.Water] = 100;
        elems[Element.Fire] = 0;
        elems[Element.Electric] = 0;
        elems[Element.Ice] = 0;
        GS = FindObjectOfType<GameSystem>();
        body = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerMoving>();
        PE = GetComponent<PlayerEffects>();
        bar = FindObjectOfType<HPBar>();
        bar.updateHealth(TotalElement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
