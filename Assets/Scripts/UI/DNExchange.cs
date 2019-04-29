using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNExchange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dayIcon;
    public GameObject nightIcon;
    public FadingCurtain curtain;

    public Text dayCount;

    public void setDayIcon()
    {
        curtain.exchange(.1f, true);
        dayIcon.SetActive(true);
        nightIcon.SetActive(false);
    }

    public void setNightIcon()
    {
        curtain.exchange(.1f, false);
        dayIcon.SetActive(false);
        nightIcon.SetActive(true);
    }

    public void setDayCnt(int num)
    {
        dayCount.text = "" + num;
    } 

    void start()
    {
        setDayIcon();
    }
}
