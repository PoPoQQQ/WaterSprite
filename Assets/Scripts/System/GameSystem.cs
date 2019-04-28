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

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
