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
        if(GS.dayOrNight == GameSystem.DayOrNight.Day)
        {
            switch (t)
            {
                case Plant.Type.Aquabud:
                    return pl.health < 300F;
                case Plant.Type.Goji:
                    return pl.gojiBuffCnt < 5;
                case Plant.Type.Mulberry:
                    return pl.mulberryBuffCnt < 5;
                default:
                    return false;
            }
        }
        else
        {
            switch(t)
            {
                case Plant.Type.Lime:
                    return true;
                case Plant.Type.Cloudberry:
                    return true;
                case Plant.Type.Lychee:
                    return pl.element == Player.Element.Water;
                case Plant.Type.Cyanberry:
                    return pl.element == Player.Element.Water;
                case Plant.Type.Mango:
                    return pl.element == Player.Element.Water;
                case Plant.Type.Turret:
                    return true;
                case Plant.Type.Bubble:
                    return true;

                default:
                    return false;
            }
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
                pl.limeBuffTime = Player.MaxLimeBuffTime;
                break;
            case Plant.Type.Cloudberry:
                pl.cloudberryBuffTime = Player.MaxCloudberryBuffTime;
                break;
            case Plant.Type.Lychee:
                pl.SetElement(Player.Element.Fire);
                break;
            case Plant.Type.Cyanberry:
                pl.SetElement(Player.Element.Ice);
                break;
            case Plant.Type.Mango:
                pl.SetElement(Player.Element.Electric);
                break;
            case Plant.Type.Turret:
                GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Ammo/Player/Turret"), pl.transform.position, Quaternion.identity);
                break;
            case Plant.Type.Bubble:
                GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Ammo/Player/Barrier"), pl.transform.position, Quaternion.identity);
                break;
        }
    }
}
