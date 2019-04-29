﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{ 
    public enum Type { None, Water, Attack, Consume, Withered};
    public Type type = Type.None;
    public int age = 0; // age == 0 means it's a seed.
    public int fruit = 0;
    public bool watered = false;

    public void SetAnimationVariables()
    {
        
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
        age = 1;
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
