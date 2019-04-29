using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 40f;
    public float health = 50F;
    int state = 1;
    Rigidbody2D body;
    PlayerMoving PM;
    PlayerEffects PE;
    public int atkBuffCnt = 0;
    public int csmBuffCnt = 0;
    public HPBar bar;
    

    void GameOver()
    {
        Debug.Log("GameOver!");
    }

    void ChangeState(int _state)
    {
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
        Check();
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerMoving>();
        PE = GetComponent<PlayerEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
