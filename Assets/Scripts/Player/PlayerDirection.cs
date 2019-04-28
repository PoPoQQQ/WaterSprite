using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
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

        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);
    }
}
