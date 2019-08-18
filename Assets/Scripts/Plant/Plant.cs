using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{ 
    public enum Type { None,
        Aquabud, Cyanberry,
        Goji, Mulberry, Wisplum,
        Lime, Cloudberry,
        Dragonfruit, Jujube, Persimmon,
        Baobab, Jackfruit, Miracle,
        Withered };

    public Type type = Type.None;
    public int age = 0; // age == 0 means it's a seed.
    public int curWater = 0;
    public int maxWater = 0;
    public int fruit = 0;
    public bool watered = false;
    
    public GameObject soil, wateredsoil, sprout, waterfruit, waterpluck, attackfruit, attackpluck, consumefruit, consumepluck, wither;

    // ----- Need To Rewrite!!!! -----
    public void SetAnimationVariables()  
    {
        /*
            soil.SetActive(true);
            wateredsoil.SetActive(false);
            sprout.SetActive(false);
            waterfruit.SetActive(false);
            waterpluck.SetActive(false);
            attackfruit.SetActive(false);
            attackpluck.SetActive(false);
            consumefruit.SetActive(false);
            consumepluck.SetActive(false);
            wither.SetActive(false);

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
            if(type == Type.Water)
            {
                if(fruit > 0)
                    waterfruit.SetActive(true);
                else
                    waterpluck.SetActive(true);
            }
            else if(type == Type.Attack)
            {
                if(fruit > 0)
                    attackfruit.SetActive(true);
                else
                    attackpluck.SetActive(true);
            }
            else if(type == Type.Consume)
            {
                if(fruit > 0)
                    consumefruit.SetActive(true);
                else
                    consumepluck.SetActive(true);
            }
            else
            {
                wither.SetActive(true);
            }
        */
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
            case Type.Baobab:
            case Type.Jackfruit:
            case Type.Miracle:
                return 98765;
        }
        return 3;
    }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
