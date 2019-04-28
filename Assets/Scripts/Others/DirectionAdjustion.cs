using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionAdjustion : MonoBehaviour
{
    public GameObject spriteCenter;

    public void Adjust(float degree)
    {
    	if(-90 <= degree && degree < 90)
    		spriteCenter.transform.eulerAngles = new Vector3(0, 0, degree);
    	else
    	{
    		spriteCenter.transform.localScale = new Vector3(-1, 1, 1);
    		spriteCenter.transform.eulerAngles = new Vector3(0, 0, degree + 180f);
    	}
    }
}
