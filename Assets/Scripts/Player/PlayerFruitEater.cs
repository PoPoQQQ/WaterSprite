using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFruitEater : MonoBehaviour
{
    static Player PL;
    static PlayerAttack PA;
    static GameSystem GS;

    public static bool Usable(Plant.Type t)
    {
        if (PL == null)
            PL = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();
        if (PA == null)
            PA = FindObjectOfType<PlayerAttack>();
        if(GS.dayOrNight == GameSystem.DayOrNight.Day)
        {
            switch (t)
            {
                case Plant.Type.Aquabud:
                    return PL.health < 300F;
                case Plant.Type.Goji:
                    return PL.gojiBuffCnt < 5;
                case Plant.Type.Mulberry:
                    return PL.mulberryBuffCnt < 5;
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
                    return PL.element == Player.Element.Water;
                case Plant.Type.Cyanberry:
                    return PL.element == Player.Element.Water;
                case Plant.Type.Mango:
                    return PL.element == Player.Element.Water;
                case Plant.Type.Turret:
                    return PA.CanUseItem();
                case Plant.Type.Bubble:
                    return PA.CanUseItem();
                case Plant.Type.Persimmon:
                    return PA.CanUseItem();
                case Plant.Type.Jujube:
                    return PA.CanUseItem();
                case Plant.Type.Dragonfruit:
                    return PA.CanUseItem();
                default:
                    return false;
            }
        }
        return false;
    }

    public static void Eat(Plant.Type t)
    {

        if (PL == null)
            PL = FindObjectOfType<Player>();
        if (GS == null)
            GS = FindObjectOfType<GameSystem>();
        if (PA == null)
            PA = FindObjectOfType<PlayerAttack>();

        switch (t)
        {
            case Plant.Type.Aquabud:
                PL.AddHealth(30);
                break;
            case Plant.Type.Goji:
                PL.gojiBuffCnt++;
                break;
            case Plant.Type.Mulberry:
                PL.mulberryBuffCnt++;
                break;
            case Plant.Type.Lime:
                PL.limeBuffTime = Player.MaxLimeBuffTime;
                break;
            case Plant.Type.Cloudberry:
                PL.cloudberryBuffTime = Player.MaxCloudberryBuffTime;
                break;
            case Plant.Type.Lychee:
                PL.SetElement(Player.Element.Fire);
                break;
            case Plant.Type.Cyanberry:
                PL.SetElement(Player.Element.Ice);
                break;
            case Plant.Type.Mango:
                PL.SetElement(Player.Element.Electric);
                break;
            case Plant.Type.Turret:
                PA.PlaceTurret();
                break;
            case Plant.Type.Bubble:
                PA.PlaceBarrier(); 
                break;
            case Plant.Type.Dragonfruit:
                PA.DragonfruitShoot();
                break;
            case Plant.Type.Jujube:
                PA.JujubeFlash();
                break;
            case Plant.Type.Persimmon:
                PA.PersimmonFlash();
                break;
        }
    }
}
