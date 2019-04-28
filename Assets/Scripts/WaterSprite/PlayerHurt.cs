﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    GameObject camera;

    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    IEnumerator FlashCoroutine()
    {
        const float duration = 0.5f;
        const float frequency = 0.05f;
        float _t = 0;

        gameObject.layer = LayerMask.NameToLayer("Flash");
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            _t += Time.deltaTime;
            if(_t >= 2 * frequency)
                _t -= 2 * frequency;
            GetComponentInChildren<SpriteRenderer>().enabled = (_t >= frequency);
            yield return 0;
        }
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
    	if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    	{
            camera.GetComponent<CameraShake>().Shake();
    		Vector2 dir = (Vector2)(transform.position - other.gameObject.transform.position);
    		dir.Normalize();
    		GetComponent<Rigidbody2D>().AddForce(dir * 1200);
            StartCoroutine(FlashCoroutine());
    	}
    }
}
