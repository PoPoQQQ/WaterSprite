using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFruitEater : MonoBehaviour
{
    static Player pl;
    static GameSystem GS;

    public static bool Usable(Plant.Type t)
    {
        if (pl == null)
            pl = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();

        switch(t)
        {
            case Plant.Type.Aquabud:
                return pl.health < 300F;
            case Plant.Type.Goji:
                return pl.gojiBuffCnt < 5;
            case Plant.Type.Mulberry:
                return pl.mulberryBuffCnt < 5;
            case Plant.Type.Lime:
                return pl.limeBuffed == false;
            case Plant.Type.Cloudberry:
                return pl.cloudberryBuffed == false;
        }
        return false;
    }

    public static void Eat(Plant.Type t)
    {

        if (pl == null)
            pl = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();

        switch (t)
        {
            case Plant.Type.Aquabud:
                pl.AddHealth(30);
                break;
            case Plant.Type.Goji:
                pl.gojiBuffCnt++;
                break;
            case Plant.Type.Mulberry:
                pl.mulberryBuffCnt++;
                break;
            case Plant.Type.Lime:
                pl.limeBuffed = true;
                break;
            case Plant.Type.Cloudberry:
                pl.cloudberryBuffed = true;
                break;
        }
    }
}
