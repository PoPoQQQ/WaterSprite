using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFruitEater : MonoBehaviour
{
    static Player pl;
    static GameSystem GS;

    static bool Usable(Plant.Type t)
    {
        if (pl == null)
            pl = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();

        switch(t)
        {
            case Plant.Type.Aquabud:
                return pl.TotalElement() < 300F;
            case Plant.Type.Cyanberry:
                return pl.elems[Player.Element.Water] > 30F;
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

    static void Eat(Plant.Type t)
    {

        if (pl == null)
            pl = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();

        switch (t)
        {
            case Plant.Type.Aquabud:
                pl.AddElem(30, Player.Element.Water);
                break;
            case Plant.Type.Cyanberry:
                pl.CostElem(30, Player.Element.Water);
                pl.AddElem(30, Player.Element.Ice);
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
