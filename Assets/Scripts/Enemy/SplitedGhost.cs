
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplitedGhost : MonoBehaviour
{
    Rigidbody2D body;
    GameObject player;
    float speed = 700F,angVel = 1.5F;
    public float movAng = 0F;
    
    Animator animator;
    Vector2 dir;
    Vector2 ToVec(float a) => new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    float ToAng(Vector2 v) => Mathf.Atan2(v.y, v.x);


    private void FixedUpdate()
    {
        dir = player.transform.position - transform.position;
        UpdateDirection();
        float targetAng = ToAng(dir);
        if (movAng < targetAng - Mathf.PI)
            movAng -= angVel * Time.fixedDeltaTime;
        else if (movAng > targetAng + Mathf.PI)
            movAng += angVel * Time.fixedDeltaTime;
        else if(movAng > targetAng)
            movAng -= angVel * Time.fixedDeltaTime;
        else
            movAng += angVel * Time.fixedDeltaTime;

        if (movAng > Mathf.PI) movAng -= 2F * Mathf.PI;
        if (movAng< -Mathf.PI) movAng += 2F * Mathf.PI;

        body.AddForce(speed * ToVec(movAng));
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        FindObjectOfType<LootSystem>().LootSeed(transform.position, Plant.Type.Consume, 0.02F);
    }
    void UpdateDirection()
    {
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }
}
