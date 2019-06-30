using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChain
{
    public Vector2 playerPos, aimPos;
    Vector2 curPos,lastPos;
    List<GameObject> targetList;
    GameObject[] enemies;
    GameObject BallPrefab;

    GameObject FindTarget(Vector2 pos, float rad)
    {
        GameObject ret = null;
        float minDist = rad;
        foreach(var i in enemies)
        {
            if (targetList.Contains(i))
                continue;
            float dist = (pos - (Vector2)i.transform.position).magnitude;
            if(dist<minDist)
            {
                ret = i;
                minDist = dist;
            }
        }
        return ret;
    }

    public void MakeChain()
    {
        BallPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Lightning/LightningChainBall");
        targetList = new List<GameObject>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        curPos = aimPos;
        lastPos = playerPos;
        for(int i =1;i<=5;i++)
        {
            var t = FindTarget(curPos, 3F);
            if (!t)
                return;
            var bi = GameObject.Instantiate(BallPrefab, t.transform);
            bi.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(0.15F*(i-1), (1.2F - 0.2F * i), 1F); 
            Debug.Log(i);
            targetList.Add(t);
            Debug.DrawLine(lastPos, curPos, Color.black, 2F);
            lastPos = curPos;
            curPos = t.transform.position;
        }
    }
    public LightningChain() { }
}
