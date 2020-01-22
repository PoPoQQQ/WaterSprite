using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : MonoBehaviour
{
    float phase = 0;
    GameObject SplitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SplitPrefab = Resources.Load<GameObject>("Prefabs/Ammo/Player/Fire/FireBombSplit");
        StartCoroutine(SplitCoroutine());
    }
    void Split(float ang, float dist)
    {
        Debug.Log(ang.ToString() + " " + dist.ToString());
        Vector3 vec = dist * new Vector3(Mathf.Cos(ang) * 4F, Mathf.Sin(ang) * 3F);
        var obj = GameObject.Instantiate(SplitPrefab,transform);
        obj.transform.localPosition = vec;
    }

    IEnumerator SplitCoroutine()
    {
        for(int i =1;i<=8;i++)
        {
            float ang = Random.Range(-Mathf.PI, Mathf.PI);
            for(int j=1;j<=i;j++)
            {
                ang += Mathf.PI * 2F / (float)i;
                Split(ang, (i / 10F));
            }
            yield return new WaitForSeconds(0.05F);
        }
        yield return new WaitForSeconds(1F);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
