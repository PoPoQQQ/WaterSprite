using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public enum DayOrNight { Day, Night };
    public DayOrNight dayOrNight = DayOrNight.Day;
    public int dayCnt = 1;
    public int score = 0;
    Plant[] plants;
    LootSystem LS;

    public float MapXUpperbound = 32;
    public float MapXLowerbound = -32;
    public float MapYUpperbound = 18;
    public float MapYLowerbound = -18;
    public float EnemyDefRate = 1F, EnemyAtkRate = 1F, EnemySpeedRate = 1F;

    public AudioSource morning;
    public AudioSource night;

    public DNExchange dayNightManager;
    public GameObject treeMan, rootMan, hornet, ghost,nxtEnemy;
    int CalcWaveCnt()
    {
        if (dayCnt <= 4)
            return 2;

        if (dayCnt <= 11)
            return 3;
        return 4;
    }
    int CalcEnmCnt(int wave)
    {
        return (int)(0.4F + Mathf.Sqrt((float)dayCnt) + 0.6F * wave);
    }

    GameObject RandEnemy()
    {
        float tR = 1F, rR = Mathf.Min(0.16F * (dayCnt - 1), 0.8F),
            hR = Mathf.Min(0.2F * (dayCnt - 3), 1.5F),
            gR = Mathf.Min(0.08F * (dayCnt - 6), 0.5F);
        float tot = tR + rR + hR + gR;
        float r = Random.Range(0F, tot);
        if (r <= tR)
            return treeMan;
        else if (r <= tR + rR)
            return rootMan;
        else if (r <= tR + rR + hR)
            return hornet;
        else 
            return ghost;
    }
    GameObject FirstEnemy()
    {
        if (dayCnt <= 2)
            return treeMan;
        if (dayCnt <= 4)
            return Random.Range(0F, 1F) < 0.7F ? rootMan : hornet;
        
        return Random.Range(0F,1F) < 0.2F * (dayCnt - 4) ? ghost : rootMan;
    }
    GameObject FinalEnemy(int wave)
    {
        if (dayCnt == 1)
            return treeMan;
        if (dayCnt == 2)
            return wave == 1 ? treeMan : rootMan;
        if (dayCnt <= 5)
            return Random.Range(0F, 1F) < 0.3F ? rootMan : hornet;
        if(dayCnt <= 8)
            return hornet;
        return hornet;

    }
    IEnumerator NightCoroutine()
    {
        int waveCnt = CalcWaveCnt();
        yield return new WaitForSeconds(4F);
        for (int i = 1; i<=waveCnt;i++)
        {
            int enmCnt = CalcEnmCnt(i);
            for(int j =1;j<=enmCnt;j++)
            {
                if (j == 1)
                    nxtEnemy = FirstEnemy();
                if (j < enmCnt)
                    nxtEnemy = RandEnemy();
                else
                    nxtEnemy = FinalEnemy(i);
                GameObject.Instantiate(nxtEnemy, new Vector3(Random.Range(-8F, 10F), Random.Range(-27F, -25F)), Quaternion.identity);
                yield return new WaitForSeconds(1F);
            }
            while(EnemyController.enemyCnt >0)
                yield return new WaitForSeconds(1F);
            yield return new WaitForSeconds(1.5F);
        }
        DayStart();
    }
    void SwitchBGM(AudioSource o, AudioSource i)
    {
        o.DOFade(0, 3f);
        i.DOFade(1, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        dayNightManager.setDayCnt(1);
        DOTween.Init();
        plants = FindObjectsOfType<Plant>();
        LS = GetComponent<LootSystem>();
    }

    void UpdateDifficulty()
    {
        EnemyAtkRate = 0.9F + 0.1F * dayCnt;
        if (dayCnt >= 8)
            EnemyAtkRate += 0.1F * (dayCnt - 8);
        EnemyAtkRate = Mathf.Clamp(EnemyAtkRate, 1F, 10F);

        EnemyDefRate = Mathf.Clamp(EnemyAtkRate - 0.5F, 1F, 5F);

        EnemySpeedRate = 0.99F + 0.01F * dayCnt;
        EnemySpeedRate = Mathf.Clamp(EnemySpeedRate, 1F, 1.5F);
    }

    public void DayStart()
    {
        if (dayOrNight == DayOrNight.Day)
            return;
        dayCnt++;
        dayNightManager.setDayCnt(dayCnt);
        dayOrNight = DayOrNight.Day;
        foreach (var i in plants)
            i.Refresh();
        SwitchBGM(night, morning);
        dayNightManager.setDayIcon();
        if(dayCnt% 10 == 0)
        {
            LS.lootChance[Plant.Type.Attack] += 0.18F;
            LS.lootChance[Plant.Type.Consume] += 0.1F;
        }
    }

    public void NightStart()
    {
        //Debug.Log("exchange");
        if (dayOrNight == DayOrNight.Night)
            return;
        dayOrNight = DayOrNight.Night;
        SwitchBGM(morning, night);
        dayNightManager.setNightIcon();
        StartCoroutine(NightCoroutine());
    }
    // Update is called once per frame
    void Update()
    {
    }
}
