using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SeedItem : MonoBehaviour
{
    public Plant.Type seedType;
    public GameObject player;
    static GameObject prefab;
    Vector3 startPos;
    float phase = 0F;
    IEnumerator SeedItemCoroutine()
    {
        yield return new WaitForSeconds(1F);
        DOTween.To(() => phase, x => phase = x, 1F, 0.5F);
        yield return new WaitForSeconds(0.5F);
        player.GetComponent<PlayerPlantOperate>().AddSeed(seedType);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SeedItemCoroutine());
        startPos = transform.position;
    }
    public static void Generate(Vector3 pos, Plant.Type type)
    {
        if (FindObjectOfType<PlayerPlantOperate>().seedCnt[type] >= 99)
            return;
        if(prefab == null)
            prefab = Resources.Load<GameObject>("Prefabs/SeedItem");
        GameObject obj = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite
            = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[type]).icon;
         obj.GetComponent<SeedItem>().seedType = type;
        /* switch(type)
        {
            case Plant.Type.Aquabud:
                s = Resources.Load<Sprite>("Seeds/seed1");
                break;
            case Plant.Type.Goji:
                s = Resources.Load<Sprite>("Seeds/seed2");
                break;
            case Plant.Type.Mulberry:
                s = Resources.Load<Sprite>("Seeds/seed3");
                break;
        }*/
       
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, player.transform.position, phase);
    }
}
