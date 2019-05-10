using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RootMan : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 navVec;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    public float basicSpeed = 500F;
    float speed = 470F;
    float theta = 0F, maxDelta;
    bool upward = false;
    Animator animator;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        theta = Random.Range(-0.5F * Mathf.PI, 0.5F * Mathf.PI);
        upward = Random.Range(0F, 1F) < 0.5F;
        GS = FindObjectOfType<GameSystem>();
        speed = basicSpeed * (0.5F * GS.EnemySpeedRate + 0.5F);
    }

    private void FixedUpdate()
    {
        navVec = EnemyNavigator.NavVec(transform.position);
        float angle = Mathf.Atan2(navVec.y, navVec.x);
        angle += theta;
        vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
        UpdateDirection();
        UpdateTheta();
        body.AddForce(vec);
    }

    void UpdateTheta()
    {
        maxDelta = Mathf.Min(Mathf.PI * 0.5F, navVec.magnitude * 1.0F);
        if(upward)
        {
            theta += Time.fixedDeltaTime * 3F;
            if (theta > maxDelta)
                upward = false;
        }
        else
        {
            theta -= Time.fixedDeltaTime * 3F;
            if (theta < -maxDelta)
                upward = true;
        }

    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        FindObjectOfType<LootSystem>().LootSeed(transform.position, Plant.Type.Water, 0.16F);
    }

    void UpdateDirection()
    {
        animator.SetFloat("X", navVec.x);
        animator.SetFloat("Y", navVec.y);
    }

    void Update()
    {
        
    }
}
