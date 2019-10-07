using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    float r = 4F;

    GameObject BulletPrefab;
    GameObject FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float d = r;
        GameObject t = null;
        foreach(GameObject i in enemies)
        {
            float dist = ((Vector2)(i.transform.position - transform.position)).magnitude;
            if(dist<d)
            {
                t = i;
                d = dist;
            }
        }
        return t;
    }

    void Shoot()
    {
        GameObject o = FindTarget();
        if (o == null)
            return;
        Vector2 vec = (o.transform.position - transform.position).normalized;
        var b = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<TurretBullet>().vec = vec * 3F;
        b.GetComponent<TurretBullet>().target = o;

    }
    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(1F);
        
        var cc = gameObject.AddComponent<CapsuleCollider2D>();
        cc.direction = CapsuleDirection2D.Horizontal;
        cc.size = new Vector2(1F, 0.6F);
        
        for (int i =1;i<=12;i++)
        {
            Shoot();
            yield return new WaitForSeconds(1F);
        }
        transform.Find("Sprite").GetComponent<Animator>().SetTrigger("dis");
        Destroy(gameObject, 1F);
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
        BulletPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/TurretBullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
