using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameSystem : MonoBehaviour
{
    public enum DayOrNight { Day, Night};
    public DayOrNight dayOrNight = DayOrNight.Day;
    public int dayCnt = 1;
    public int score = 0;
    Plant[] plants;

    public float MapXUpperbound = 32;
    public float MapXLowerbound = -32;
    public float MapYUpperbound = 18;
    public float MapYLowerbound = -18;

    public AudioSource morning;
    public AudioSource night;

    public DNExchange dayNightManager;

    void SwitchBGM(AudioSource o, AudioSource i)
    {
        o.DOFade(0, 3f);
        i.DOFade(1, 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        plants = FindObjectsOfType<Plant>();
    }

    public void DayStart()
    {
        dayNightManager.setDayIcon();
        if (dayOrNight == DayOrNight.Day)
            return;
        dayCnt++;
        dayNightManager.setDayCnt(dayCnt);
        dayOrNight = DayOrNight.Day;
        foreach (var i in plants)
            i.Refresh();
        SwitchBGM(night, morning);
    }

    public void NightStart()
    {
        dayNightManager.setNightIcon();
        if (dayOrNight == DayOrNight.Night)
            return;
        dayOrNight = DayOrNight.Night;
        SwitchBGM(morning, night);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            NightStart();
        if (Input.GetKeyDown(KeyCode.O))
            DayStart();
    }
}
