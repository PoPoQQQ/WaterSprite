using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBomb : MonoBehaviour
{
    GameObject sc;
    public GameObject burstPrefab;
    public float phase = 0, height = 0;
    public Vector2 startPos, endPos;
    // Start is called before the first frame update
    void Start()
    {
        sc = transform.Find("SpriteCenter").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        phase += Time.deltaTime * 1.0F;
        height = phase * (1F - phase) * 12F + 0.2F;
        transform.position = (Vector3)Vector2.Lerp(startPos, endPos, phase) + Vector3.forward * transform.position.z;
        sc.transform.localPosition = new Vector3(0F, height);
        if (phase >= 1F)
        {
            GameObject.Instantiate(burstPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
