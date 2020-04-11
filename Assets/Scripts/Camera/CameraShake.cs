using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
	public float duration = 0.5f;
	public float strength = 0.08f;
	public int time = 20;
	public float randomness = 90f;

    public void Shake(float power = 1F)
    {
    	transform.DOShakePosition(duration * power, strength * power, time, randomness);
    }
}
