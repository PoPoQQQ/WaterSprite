using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMan : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Rigidbody2D body;
    GameObject player;
    float speed = 300F;

    Animator animator;

    IEnumerator ChangeVecCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2F));
        while(true)
        {
            UpdateDirection();
            SetVec();
            yield return 0;
        }
    }

    void SetVec()
    {
        if (!player)
        {
            vec = Vector2.zero;
            return;
        }
        Vector2 dPos = player.transform.position - transform.position;
        dPos.Normalize();
        vec = dPos * speed;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(ChangeVecCoroutine());
    }

    private void FixedUpdate()
    {
        body.AddForce(vec);
    }

    void UpdateDirection()
    {
        Vector2 dir = player.transform.position - transform.position;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }

    void Update()
    {
        
    }
}
