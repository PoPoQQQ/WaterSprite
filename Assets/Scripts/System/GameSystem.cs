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

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        plants = FindObjectsOfType<Plant>();
    }

    void DayStart()
    {
        if (dayOrNight == DayOrNight.Day)
            return;
        dayCnt++;
        dayOrNight = DayOrNight.Day;
        foreach (var i in plants)
            i.Refresh();
    }

    void NightStart()
    {
        if (dayOrNight == DayOrNight.Night)
            return;
        dayOrNight = DayOrNight.Night;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            NightStart();
        if (Input.GetKeyDown(KeyCode.O))
            DayStart();

    }
}
