using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed = 40f;
    public float health = 50F;

    Rigidbody2D body;
    PlayerMoving PM;
    PlayerHurt PH;
    
    public void Damage(float damage, Vector2 KnockBack)
    {
        body.AddForce(KnockBack, ForceMode2D.Impulse);

    }

    public void Damage(float damage)
    {
        Damage(damage, Vector2.zero);
    }
    public void CostHealth(float cost)
    {

    }

    public void AddHealth(float h)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        PM = GetComponent<PlayerMoving>();
        PH = GetComponent<PlayerHurt>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
