using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public void Shake()
    {
    	transform.DOShakePosition(1f, 0.8f, 10, 10f);
    }
}
