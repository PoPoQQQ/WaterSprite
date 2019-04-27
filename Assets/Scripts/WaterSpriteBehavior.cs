using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpriteBehavior : MonoBehaviour
{
	public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Vector2 dir = Vector2.zero;
    	if(Input.GetKey(KeyCode.W)) dir += Vector2.up;
    	if(Input.GetKey(KeyCode.S)) dir += Vector2.down;
    	if(Input.GetKey(KeyCode.A)) dir += Vector2.left;
    	if(Input.GetKey(KeyCode.D)) dir += Vector2.right;
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}
