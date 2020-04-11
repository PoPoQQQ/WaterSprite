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
    public GameObject treeMan, rootMan, hornet, ghost, skull, bigHornet, hog, mole;
    public GameObject bossHedgehog, bossWisp, bossTree;
    public GameObject nxtEnemy;
    int CalcWaveCnt()
    {
        int ret;
        if (dayCnt <= 6)
            ret = 2;
        else if (dayCnt <= 30)
            ret = 3;
        else
            ret = 4;

        if (dayCnt % 10 == 0)
            ret--;
        return ret;
    }
    int CalcEnmCnt(int wave)
    {
        return (int)(0.4F + Mathf.Sqrt((float)dayCnt) + 0.6F * wave);
    }

    float r = 0F;

    GameObject meleeE, rangeE;//melee enemy,  ranged enemy
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
            else meleeE = hog;
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
                return bigHornet;
            else
                return ghost;
        }

    }
    Vector2 BossPos()
    {
        Vector2 pos = PL.transform.position;
        pos -= pos.normalized * 5F;
        return pos;
    }
    GameObject BossObj()
    {
        switch (dayCnt % 30)
        {
            case 10:
                return bossHedgehog;
            case 20:
                return bossWisp;
            case 0:
                return bossTree;
            default:
                return bossHedgehog;
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
                GameObject.Instantiate(nxtEnemy, RandomPosGenerator.GetPos(), Quaternion.identity);
                yield return new WaitForSeconds(1F);
            }
            while(EnemyController.enemyCnt >0)
                yield return new WaitForSeconds(1F);
            yield return new WaitForSeconds(1.5F);
        }

        if(dayCnt%10 == 0)
        {
            GameObject.Instantiate(BossObj(), BossPos(), Quaternion.identity);
            yield return new WaitForSeconds(5F);
            while (EnemyController.enemyCnt > 0)
                yield return new WaitForSeconds(3F);
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
        EnemyAtkRate = 0.9F + 0.03F * dayCnt;
        EnemyAtkRate = Mathf.Clamp(EnemyAtkRate, 1F, 10F);

        EnemyDefRate = 0.7F + 0.05F * dayCnt;
        EnemyDefRate = Mathf.Clamp(EnemyDefRate, 1F, 4F);

        EnemySpeedRate = 0.99F + 0.005F * dayCnt;
        EnemySpeedRate = Mathf.Clamp(EnemySpeedRate, 1F, 1.5F);
    }

    public void DayStart()
    {
        if (dayOrNight == DayOrNight.Day)
            return;
        dayCnt++;
        dayNightManager.setDayCnt(dayCnt);
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
    public void DayCheck()
    {
        dayOrNight = DayOrNight.Day;
        GameObject[] trunks = GameObject.FindGameObjectsWithTag("Trunk");
        foreach (var i in trunks)
        {
            i.GetComponent<Trunk>().DayCheck();
        }
        EnemyController[] ecList = FindObjectsOfType<EnemyController>();
        foreach(var i in ecList)
        {
            Destroy(i.gameObject);
        }
    }
    public void NightStart()
    {
        //Debug.Log("exchange");
        if (dayOrNight == DayOrNight.Night)
            return;
        UpdateDifficulty();
        SwitchBGM(morning, night);
        dayNightManager.setNightIcon();
        StartCoroutine(NightCoroutine());
    }
    public void NightCheck()
    {
        dayOrNight = DayOrNight.Night;
        EnemyController.enemyCnt = 0;
        PL.SetWisps();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
