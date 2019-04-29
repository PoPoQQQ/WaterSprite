using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RootMan : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Vector2 dPos;
    Rigidbody2D body;
    GameObject player;
    float speed = 500F;
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
          
    }

    private void FixedUpdate()
    {
        dPos = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dPos.y, dPos.x);
        angle += theta;
        vec = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
        UpdateDirection();
        UpdateTheta();
        body.AddForce(vec);
    }

    void UpdateTheta()
    {
        maxDelta = Mathf.Min(Mathf.PI * 0.5F, dPos.magnitude * 1.0F);
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
        if (Random.Range(0F, 1F) <= 0.2F)
            SeedItem.Generate(transform.position, Plant.Type.Water);
    }

    void UpdateDirection()
    {
        animator.SetFloat("X", dPos.x);
        animator.SetFloat("Y", dPos.y);
    }

    void Update()
    {
        
    }
}
