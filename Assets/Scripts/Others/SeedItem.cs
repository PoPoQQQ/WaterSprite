﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SeedItem : MonoBehaviour
{
    public Plant.Type seedType;
    public GameObject player;
    Vector3 startPos;
    float phase = 0F;
    IEnumerator SeedItemCoroutine()
    {
        yield return new WaitForSeconds(1F);
        DOTween.To(() => phase, x => phase = x, 1F, 0.5F);
        yield return new WaitForSeconds(0.5F);
        yield return new WaitForSeconds(1F);
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
        GameObject prefab = Resources.Load<GameObject>("Prefab/SeedItem");
        GameObject obj = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        Sprite s = null;
        switch(type)
        {
            case Plant.Type.Water:
                s = Resources.Load<Sprite>("Seeds/seed1");
                break;
            case Plant.Type.Attack:
                s = Resources.Load<Sprite>("Seeds/seed2");
                break;
            case Plant.Type.Consume:
                s = Resources.Load<Sprite>("Seeds/seed3");
                break;
        }
        obj.GetComponent<SpriteRenderer>().sprite = s;
        obj.GetComponent<SeedItem>().seedType = type;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, player.transform.position, phase);
    }
}