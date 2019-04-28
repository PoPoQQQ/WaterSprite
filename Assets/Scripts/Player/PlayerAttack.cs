using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public float attackSpeed = 0.5f;
    public float offsetY = 0f;

    public GameObject bulletPrefab;

    Coroutine c;

    void Start()
    {
        
    }

    IEnumerator ShootingCoroutine()
    {
    	while(true)
    	{
    		Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        	dir.y -= offsetY;
        	dir.Normalize();

        	GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        	bullet.GetComponent<Rigidbody2D>().AddForce(dir * 5f, ForceMode2D.Impulse);
        	bullet.GetComponent<DirectionAdjustion>().Adjust(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        	Destroy(bullet, 10f);

        	yield return new WaitForSeconds(attackSpeed);
    	}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        	c = StartCoroutine(ShootingCoroutine());
        if(Input.GetMouseButtonUp(0))
        	StopCoroutine(c);
    }
}
