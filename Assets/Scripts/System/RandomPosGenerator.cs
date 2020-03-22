using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomPosGenerator
{
    const float MINX = -10.5F, MAXX = 11.5F, MINY = -26F, MAXY = 6.5F;
    const float COLX = 3F, COLY = 2F;
    static LayerMask mask = LayerMask.GetMask("Player", "Enemy", "Obstacle", "Airwall", "FriendlyBorder");    

    public static Vector2 GetPos()
    {
        Vector2 pos = new Vector2(Random.Range(MINX, MAXX), Random.Range(MINY, MAXY));
        while(Physics2D.OverlapCapsule(pos,new Vector2(COLX,COLY),CapsuleDirection2D.Horizontal,0F,mask))
        {
            pos = new Vector2(Random.Range(MINX, MAXX), Random.Range(MINY, MAXY));
        }
        Debug.DrawLine(pos + Vector2.down * 0.5F * COLY, pos + Vector2.up * 0.5F * COLY);
        Debug.DrawLine(pos + Vector2.left * 0.5F * COLX, pos + Vector2.right * 0.5F * COLX);
        //Debug.Break();
        return pos;
    }

}
