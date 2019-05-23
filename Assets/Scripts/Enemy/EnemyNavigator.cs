using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator
{
    static GameObject pl;

    static Vector2 ToVec(float a) => new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    static float ToAng(Vector2 v) => Mathf.Atan2(v.y, v.x);
    public static Vector2 NavVec(Vector2 pos, float randMin, float randMax)
    {
        if (pl == null)
            pl = GameObject.Find("Player");

        if (pl == null)
            return Vector2.zero;

        Vector2 plPos = pl.transform.position;
        int mask = LayerMask.GetMask("Player", "Obstacle", "AirWall");
        Vector2 vec = (plPos - pos).normalized;
        var ray = Physics2D.Raycast(pos, vec, 1000F, mask);
        if (ray.collider.gameObject.tag == "Player")
            return vec;
        float dist = ray.distance;
        float angle ,dAngle;
        angle = ToAng(vec);

        Vector2 dVec = vec;
        RaycastHit2D dRay;
        float retAng = angle;
        for(int i = 1;i<=6;i++)
        {
            dAngle = angle + Mathf.PI * 0.1F * i;
            dVec = ToVec(dAngle);
            dRay = Physics2D.Raycast(pos, dVec, 1000F, mask);
            if(!dRay)
            {
                retAng = dAngle + 0.4F;
                break;
            }
            if (dRay.collider.gameObject.tag == "Player" || dRay.distance >= dist + 1.2F)
            {
                retAng = dAngle + 0.4F;
                break;
            }

            dAngle = angle - Mathf.PI * 0.1F * i;
            dVec = ToVec(dAngle);
            dRay = Physics2D.Raycast(pos, dVec, 1000F, mask);
            if (!dRay)
            {
                retAng = dAngle - 0.4F;
                break;
            }
            if (dRay.collider.gameObject.tag == "Player" || dRay.distance >= dist + 1.2F)
            {
                retAng = dAngle - 0.4F;
                break;
            }
        }

        retAng += Random.Range(randMin, randMax);
        return ToVec(retAng);
    }
    public static Vector2 NavVec(Vector2 pos) => NavVec(pos, 0,0);
}
