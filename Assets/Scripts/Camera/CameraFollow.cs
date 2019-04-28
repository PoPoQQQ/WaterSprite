using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float followingSpeed = 5f;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    	Vector2 delta = player.transform.position - transform.position;
        transform.Translate(new Vector3(delta.x, delta.y, 0) * followingSpeed * Time.deltaTime);
    }
}
