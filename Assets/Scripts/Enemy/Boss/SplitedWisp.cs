using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedWisp : MonoBehaviour
{
    float b = 0, c = 0, a;
    public float basicAlpha, basicTheta;
    float alpha, theta;
    float bGrowth = 2F, cGrowth = 0.6F, alphaVel = 1F,thetaVel = 0.2F;
    GameObject mother;
    BossWisp wisp;

    private void FixedUpdate()
    {
        if(!wisp.declining)
        {
            if (b < 4F)
                b += bGrowth * Time.fixedDeltaTime;
            else if (c < b * 1.1F)
                c += cGrowth * Time.fixedDeltaTime;
        }
        else
        {
            if(c > 0F)
            {
                b -= 0.5F * Time.deltaTime;
                c -= 2F * Time.deltaTime;
            }
            else if(b > 0F)
            {
                b -= 1.6F * Time.deltaTime;
            }
        }

        a = Mathf.Sqrt(b * b + c * c);
        alpha = basicAlpha + alphaVel * Time.time;
        theta = basicTheta + thetaVel * Time.time;
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
}
