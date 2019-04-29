using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void GameOver()
    {
        Debug.Log("GameOver!");
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
        body = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerMoving>();
        PE = GetComponent<PlayerEffects>();
        bar = FindObjectOfType<HPBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
