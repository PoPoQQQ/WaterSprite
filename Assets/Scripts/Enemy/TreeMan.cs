using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMan : MonoBehaviour
{
    Vector2 vec = Vector2.zero;
    Rigidbody2D body;
    GameObject player;
    GameSystem GS;
    float basicSpeed = 600F;
    float speedRate;
    int vecCnt = 0;
    Animator animator;

    IEnumerator ChangeVecCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0F, 2F));
        while(true)
        {
            UpdateDirection();
            SetVec();
            body.drag = 1F;
            yield return new WaitForSeconds(0.6F / speedRate);

            vec = Vector2.zero;
            body.drag = 10F;
            yield return new WaitForSeconds(0.4F / speedRate);
        }
    }

    void SetVec()
    {
        vecCnt++;
        if (vecCnt % 2 == 0)
            vec = EnemyNavigator.NavVec(transform.position, -0.7F, -0.5F);
        else
            vec = EnemyNavigator.NavVec(transform.position, 0.5F,0.7F);
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(ChangeVecCoroutine());
        GS = FindObjectOfType<GameSystem>();
        speedRate = GS.EnemySpeedRate;
    }

    private void FixedUpdate()
    {
        body.AddForce(vec * basicSpeed * speedRate);
    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health > 0)
            return;
        FindObjectOfType<LootSystem>().LootSeed(transform.position, Plant.Type.Water, 0.16F);
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
