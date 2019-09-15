using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    static public Vector2 ToVec(float a) => new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    static public float ToAng(Vector2 v) => Mathf.Atan2(v.y, v.x);

    Rigidbody2D body;
    float vel = 4F,phase = 0F;
    Vector2 startVec,adjVec;
    public void CalcVel(Vector2 basicVel)
    {
        float r = Random.Range(-0.5F, 0.5F),ang = ToAng(basicVel);
        startVec = ToVec(ang + r);
        adjVec = ToVec(ang + Mathf.PI * 0.5F) * (-1.2F * r);
    }
    private void FixedUpdate()
    {
        phase += Time.fixedDeltaTime / 3F;
        if (phase > 1F)
            Destroy(gameObject);
        float adjRate = phase < 0.5F ? 1F : (2F * (1F - phase));
        body.AddForce(adjVec * adjRate);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var i in objs)
        {
            Vector2 vec = (Vector2)(i.transform.position - transform.position);
            float dist = vec.magnitude;
            if (dist > 4F)
                continue;
            vec.Normalize();
            dist = Mathf.Clamp(dist, 1F, 4F);
            vec = vec * 4F / dist;
            body.AddForce(vec);
            body.AddForce(-body.velocity * 0.5F / dist);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = startVec * vel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float burnRate = -1F;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            var ec = collision.gameObject.GetComponent<EnemyController>();
            if (!ec)
                return;
            ec.Damage(2F * Time.deltaTime);
        }
        if (collision.gameObject.tag == "Trunk")
        {
            if (burnRate < 0F)
                burnRate = Random.Range(0F, 1F);
            var t = collision.gameObject.GetComponent<Trunk>();
            if (t && burnRate>0.5F)
                t.Burn();

        }
    }

}
