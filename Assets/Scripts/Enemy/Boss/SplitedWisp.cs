using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedWisp : MonoBehaviour
{
    float pb = 0, pc = 0;
    float a, b, c;
    public float alpha, theta;
    public bool outerRing = false;
    float bGrowth = 2F, cGrowth = 0.6F, alphaVel = 1F,thetaVel = 0.2F;
    float lifeTime = 0F,circleRate = 0F;
    GameObject mother;
    BossWisp wisp;

    private void FixedUpdate()
    {
        if(!wisp.declining)
        {
            if (pb < 4F)
                pb += bGrowth * Time.fixedDeltaTime;
            else if (pc < pb * 1.1F)
                pc += cGrowth * Time.fixedDeltaTime;
        }
        else
        {
            if(pc > 0F)
            {
                pb -= 0.5F * Time.deltaTime;
                pc -= 2F * Time.deltaTime;
            }
            else if(pb > 0F)
                pb -= 1.6F * Time.deltaTime;
        }
        if (wisp.state == BossWisp.State.mid)
        {
            b = pb;
            c = pc;
        }
        else
        {
            lifeTime += Time.deltaTime * 0.3F;
            if (outerRing)
                circleRate = (Mathf.Sin(lifeTime) + 1F) * 0.5F;
            else
                circleRate = (Mathf.Cos(lifeTime) + 1F) * 0.5F;
            if (lifeTime <= 2 * Mathf.PI)
                circleRate = 0;
            else if (lifeTime <= 3 * Mathf.PI)
                circleRate *= (lifeTime / Mathf.PI) - 2F;

            if(outerRing)
            {
                b = pb + circleRate * pc;
                c = pc - circleRate * pc;
            }
            else
            {
                b = pb;
                c = pc - circleRate * pc;
            }
        }
        a = Mathf.Sqrt(b * b + c * c);
        alpha += alphaVel * Time.fixedDeltaTime;
        if(wisp.state == BossWisp.State.mid)
            theta += thetaVel * Time.fixedDeltaTime;

        Vector2 axisX = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)),
            axisY = new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta));
        float x = a * Mathf.Cos(alpha) - c, y = b * Mathf.Sin(alpha);
        Vector3 pos = x * axisX + y * axisY;

        transform.position = mother.transform.position + pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        mother = transform.parent.gameObject;
        wisp = mother.GetComponent<BossWisp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (GetComponent<EnemyController>().health <= 0F)
            wisp.Split(theta, alpha, false);
    }
}
