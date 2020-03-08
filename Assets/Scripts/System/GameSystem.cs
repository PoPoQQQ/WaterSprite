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
    Player PL;
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
    public GameObject treeMan, rootMan, hornet, ghost, skull, hornetPlus, boar, mole;
    public GameObject nxtEnemy;
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

    float r = 0F;
    GameObject RandEnemy()
    {
        if (dayCnt == 1)
            return treeMan;
        if (dayCnt <= 3)
            return Random.Range(0F, 2F) <= 1F ? treeMan : rootMan;
        if(dayCnt <= 10)
        {
            r += Random.Range(0.5F, 1.5F);
            if (r > 2F)
                r -= 2F;

            if (r <= 0.6F)
                return treeMan;
            else if (r <= 1.2F)
                return rootMan;
            else return hornet;
        }
        if (dayCnt <= 20)
        {
            r += Random.Range(0.5F, 1.5F);
            if (r > 2F)
                r -= 2F;

            if (r <= 0.3F)
                return treeMan;
            else if (r <= 1.1F)
                return meleeE;
            else return rangeE;
        }
        else
        {
            r += Random.Range(0.5F, 1.5F);
            if (r > 2F)
                r -= 2F;

            if (r <= 1F)
                return meleeE;
            else return rangeE;
        }
    }

    GameObject meleeE, rangeE;//melee enemy,  ranged enemy

    GameObject FirstEnemy()
    {
        if (dayCnt == 1)
            meleeE = treeMan;
        else if (dayCnt <= 10)
            meleeE = Random.Range(0F, 2F) < 1F ? treeMan : rootMan;
        else
        {
            float r = Random.Range(0F, 3F);
            if (r <= 1F) meleeE = treeMan;
            else if (r <= 2F) meleeE = rootMan;
            else meleeE = boar;
        }

        if (dayCnt <= 10)
            rangeE = hornet;
        else if (dayCnt <= 20)
            rangeE = Random.Range(0F, 2F) < 1F ? hornet : skull;
        else
        {
            float r = Random.Range(0F, 3F);
            if (r <= 1F) rangeE = hornet;
            else if (r <= 2F) rangeE = skull;
            else rangeE = mole;
        }
        return meleeE;
    }
    GameObject LastEnemy(int wave)
    {
        if (dayCnt == 1)
            return treeMan;
        if (dayCnt == 2)
            return wave == 1 ? treeMan : rootMan;
        if (dayCnt <= 5)
            return Random.Range(0F, 1F) < 0.3F ? rootMan : hornet;
        if(dayCnt <= 8)
        {
            if (wave <= 2)
                return hornet;
            else
                return ghost;
        }
        if (dayCnt <= 15)
        {
            if (Random.Range(0F,2F) < 1F)
                return hornet;
            else
                return ghost;
        }
        else
        {
            if (Random.Range(0F, 2F) < 1F)
                return hornetPlus;
            else
                return ghost;
        }

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
                    nxtEnemy = LastEnemy(i);
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
        PL = FindObjectOfType<Player>();
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
        PL.Refresh();
        SwitchBGM(night, morning);
        dayNightManager.setDayIcon();
        if(dayCnt% 10 == 0)
        {
            LS.lootChanceMultiplier[Plant.Type.Goji] += 0.18F;
            LS.lootChanceMultiplier[Plant.Type.Mulberry] += 0.1F;
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
        PL.SetWisps();
        StartCoroutine(NightCoroutine());
    }
    // Update is called once per frame
    void Update()
    {
    }
}
