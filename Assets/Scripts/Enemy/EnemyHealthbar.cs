using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    GameObject Enemy;
    public bool isBoss;
    public float length = 1F;
    float dispTime = 0F;
    Color c;
    public void Init(GameObject _e, bool _b,float _h)
    {
        isBoss = _b;
        Enemy = _e;

        if(isBoss)
        {
            transform.SetParent(GameObject.Find("Camera Position").transform);
            transform.localPosition = Vector3.down * -4F;
        }
        else
        {
            transform.SetParent(Enemy.transform);
            transform.localPosition = Vector3.up * (_h+0.2F);
        }
    }

    public void Set(float h)
    {
        if (h < 0F)
            Destroy(gameObject);
        length = h;
        c = new Color(1 - h, h, 0);
        if(isBoss)
            transform.localScale = new Vector3(h * 10F, 2F);
        else
            transform.localScale = new Vector3(h, 1F);
        dispTime = 1F;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dispTime > 0F)
            c.a = 1F;
        else
            c.a = 0F;
        GetComponent<SpriteRenderer>().color = c;
    }
}
