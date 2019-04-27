using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAxisAdjustion : MonoBehaviour
{
    const float minZAxis = 0f;
    const float maxZAxis = 10f;
    const float minYAxis = -100f;
    const float maxYAxis = 100f;

    // Update is called once per frame
    void Update()
    {
        float y = transform.position.y;
        float z = (y - minYAxis) / (maxYAxis - minYAxis) * (maxZAxis - minZAxis) + minZAxis;
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
}
