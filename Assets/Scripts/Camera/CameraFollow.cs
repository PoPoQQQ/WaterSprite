using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float followingSpeed = 5f;

    GameObject player;

    float cameraSize;

    float MapXUpperbound;
    float MapXLowerbound;
    float MapYUpperbound;
    float MapYLowerbound;

    void Start()
    {
        player = GameObject.Find("Player");

        cameraSize = GetComponentInChildren<Camera>().orthographicSize;

        GameSystem GS = GameObject.Find("GameManager").GetComponent<GameSystem>();
        MapXUpperbound = GS.MapXUpperbound;
        MapXLowerbound = GS.MapXLowerbound;
        MapYUpperbound = GS.MapYUpperbound;
        MapYLowerbound = GS.MapYLowerbound;
    }

    // Update is called once per frame
    void Update()
    {
    	Vector2 position = player.transform.position;
    	position.x = Mathf.Max(position.x, MapXLowerbound + cameraSize / 9 * 16);
    	position.x = Mathf.Min(position.x, MapXUpperbound - cameraSize / 9 * 16);
    	position.y = Mathf.Max(position.y, MapYLowerbound + cameraSize);
    	position.y = Mathf.Min(position.y, MapYUpperbound - cameraSize);
    	Vector2 delta = position - (Vector2)transform.position;
        transform.Translate(new Vector3(delta.x, delta.y, 0) * followingSpeed * Time.deltaTime);
    }
}
