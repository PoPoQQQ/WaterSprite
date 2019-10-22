using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBomb : MonoBehaviour
{
    GameObject burstPrefab;
    // Start is called before the first frame update
    IEnumerator BurstCoroutine()
    {
        yield return new WaitForSeconds(2F);
        GameObject.Instantiate(burstPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Start()
    {
        burstPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Ice/IceBombBurst");
        StartCoroutine(BurstCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
