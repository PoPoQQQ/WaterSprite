using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavigator
{
    static GameObject pl;

    public static Vector2 NavVec(Vector2 pos)
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
        float angle = Mathf.Atan2(vec.y, vec.x),dAngle;
        Vector2 dVec = vec;
        RaycastHit2D dRay;
        for(int i = 1;i<=6;i++)
        {
            dAngle = angle + Mathf.PI * 0.1F * i;
            dVec = new Vector2(Mathf.Cos(dAngle), Mathf.Sin(dAngle));
            dRay = Physics2D.Raycast(pos, dVec, 1000F, mask);
            if(!dRay)
                return dVec;
            if (dRay.collider.gameObject.tag == "Player" || dRay.distance >= dist + 1.2F)
                return dVec;

            dAngle = angle - Mathf.PI * 0.1F * i;
            dVec = new Vector2(Mathf.Cos(dAngle), Mathf.Sin(dAngle));
            dRay = Physics2D.Raycast(pos, dVec, 1000F, mask);
            if (!dRay)
                return dVec;
            if (dRay.collider.gameObject.tag == "Player" || dRay.distance >= dist + 1.2F)
                return dVec;
        }
        return dVec;
    }

}
