using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{ 
    public enum Type { None,
        Aquabud, Goji, Mulberry, Wisplum,
        Lychee, Cyanberry, Mango,         
        Lime, Cloudberry,
        Dragonfruit, Jujube, Persimmon,
        Turret, Bubble,
        Miracle,
        Withered };

    public Type type = Type.None;
    public int age = 0; // age == 0 means it's a seed.
    public int curWater = 0;
    public int maxWater = 30;
    public int fruit = 0;
    public bool watered = false;
    
    public GameObject soil, wateredsoil, sprout, waterfruit, waterpluck, attackfruit, attackpluck, consumefruit, consumepluck, wither;

    public void SetAnimationVariables()  
    {
        wateredsoil.SetActive(false);
        soil.SetActive(true);
        sprout.SetActive(false);
        wither.SetActive(false);
        transform.Find("plant").gameObject.SetActive(false);
        if(type == Type.None)
                return;
        if(age == 0)
        {
            sprout.SetActive(true);
            if(watered)
            {
                soil.SetActive(false);
                wateredsoil.SetActive(true);
            }
            return;
        }
        if(type == Type.Withered)
        {
            wither.SetActive(true);
            return;
        }
        /*CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[type]);
        transform.Find("plant").gameObject.GetComponent<SpriteRenderer>().sprite = temp.icon;*/
        transform.Find("plant").gameObject.SetActive(true);
        transform.Find("plant").gameObject.GetComponent<PlantAnimateLoader>().loadAnimation(type);
        if(fruit == 0)
            transform.Find("plant").gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        else
            transform.Find("plant").gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        
    }

    int MaxAge()
    {
        switch(type)
        {
            case Type.Goji:
            case Type.Mulberry:
            case Type.Wisplum:
            case Type.Lime:
                return 1;
            case Type.Cyanberry:
            case Type.Dragonfruit:
            case Type.Cloudberry:
                return 2;
            case Type.Aquabud:
            case Type.Jujube:
            case Type.Persimmon:
                return 3;
            case Type.Miracle:
                return 98765;
        }
        return 3;
    }

    public bool mature{get{return fruit>0;}}

    public void Remove()
    {
        age = 0;
        fruit = 0;
        type = Type.None;
        age = 0;
        fruit = 0;
        curWater = maxWater = 0;
        watered = false;
        SetAnimationVariables();
    }
    public void Wither()
    {
        type = Type.Withered;
        fruit = 0;
        SetAnimationVariables();
    }

    public void Refresh()//call this when day begins
    {
        if (type == Type.Withered || type == Type.None)
            return;
        if (!watered)
            return;
        age++;
        if (age > MaxAge())
        {
            Wither();
            return;
        }
        fruit = 1;
        SetAnimationVariables();
    }

    public void Collect()
    {
        fruit = 0;
        if (age == MaxAge())
        {
            Wither();
            return;
        }
        SetAnimationVariables();
    }

    public void PlantSeed(Type seedType)
    {
        if (type != Type.None)
            return;
        type = seedType;
        age = 0;
        fruit = 0;
        SetAnimationVariables();
    }

    public void Water()
    {
        if (watered)
            return;
        if (type == Type.None)
            return;
        curWater += 10;
        if (curWater >= maxWater)
            watered = true;
        SetAnimationVariables();
    }

    void Start()
    {
        SetAnimationVariables();
    }

}
