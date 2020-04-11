using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    Image im;
    RectTransform rt;
    Rect rect;
    float basicW, basicH;
    public float CDTime = 1F, curTime = 1F, phase;
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        rect = rt.rect;
        basicW = rect.width;
        basicH = rect.height;
    }

    public void Set(float CD)
    {
        curTime = phase = 0F;
        CDTime = CD;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTime<=CDTime)
        {
            curTime += Time.deltaTime;
            phase = Mathf.Clamp01(curTime / CDTime);
            //rect.height = basicH * phase;
            rt.sizeDelta = new Vector2(basicW, basicH * phase);
            Debug.Log(rect.height);
        }

        if (FindObjectOfType<GameSystem>().dayOrNight == GameSystem.DayOrNight.Day)
        {
            im.color = new Color(1F, 1F, 1F, 0F);
        }
        else
        {
            if (phase < 1F)
                im.color = new Color(1F, 1F, 1F, 0.5F);
            else
                im.color = new Color(1F, 1F, 1F, 1F);
        }
    }
}
