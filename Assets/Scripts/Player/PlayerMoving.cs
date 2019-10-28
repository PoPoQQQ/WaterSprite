using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Player pl;
    private void Start()
    {
        pl = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) dir += Vector2.up;
        if (Input.GetKey(KeyCode.S)) dir += Vector2.down;
        if (Input.GetKey(KeyCode.A)) dir += Vector2.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector2.right;
        if (Input.GetKeyDown(KeyCode.P))
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Ammo/Player/Turret"), transform.position, Quaternion.identity);
        if (dir.magnitude > 1F)
            dir.Normalize();
        float speed = pl.moveSpeed;
        if (pl.cloudberryBuffTime>0F)
            speed *= 1.5F;
        GetComponent<Rigidbody2D>().AddForce(dir * speed);

    }

    void Update()
    {
    }
}
