using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{ 
    public enum Type { None, Water, Attack, Consume, Withered};
    public Type type = Type.None;
    public int age = 0; // age == 0 means it's a seed.
    public int fruit = 0;
    public bool watered = false;

    public GameObject sprout, waterfruit, waterpluck, attackfruit, attackpluck, consumefruit, consumepluck, wither;

    public void SetAnimationVariables()
    {
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
            return;
        }
        if(type == Type.Withered)
        {
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

    }

    public void Remove()
    {
        age = 0;
        fruit = 0;
        type = Type.None;
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
        if (age > 3)
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
