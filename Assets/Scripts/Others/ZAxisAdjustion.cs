using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisAdjustion : MonoBehaviour
{
    protected const float minZAxis = 0f;
    protected const float maxZAxis = 10f;
    protected const float minYAxis = -100f;
    protected const float maxYAxis = 100f;

    protected void Adjust()
    {
        float y = transform.position.y;
        float z = (y - minYAxis) / (maxYAxis - minYAxis) * (maxZAxis - minZAxis) + minZAxis;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    protected void Update()
    {
        Adjust();
    }
}
