using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryController : MonoBehaviour
{
    Vector3 originPos;
    Vector3 targetPos;
    Vector3 targetPosition;
    float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        originPos = targetPos = transform.position;
        targetPos.y = originPos.y + 1080;
        targetPosition = originPos;
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
       {
           targetPosition = targetPosition == originPos? targetPos : originPos;
           transform.DOMove(targetPosition, smoothTime);
           //transform.position = targetPosition;
       }
    }
}
