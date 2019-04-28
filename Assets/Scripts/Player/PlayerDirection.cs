using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public float alpha = 15f;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector2 dir;
        if(gameObject.layer == LayerMask.NameToLayer("Flash"))
        	dir = - GetComponent<Rigidbody2D>().velocity;
        else if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        	dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        else
        	dir = GetComponent<Rigidbody2D>().velocity;

        if(dir == Vector2.zero)
        	return;
        /*
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
        	angle += 360;
        if(360 - alpha <= angle && angle <= 360 || 0 <= angle && angle < alpha)
        	GetComponentInChildren<Animator>().SetInteger("Direction", 6);
        else if(alpha <= angle && angle < 180 - alpha)
        	GetComponentInChildren<Animator>().SetInteger("Direction", 8);
        else if(180 - alpha <= angle && angle < 180 + alpha)
        	GetComponentInChildren<Animator>().SetInteger("Direction", 4);
        else
        	GetComponentInChildren<Animator>().SetInteger("Direction", 2);
        */
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }
}
