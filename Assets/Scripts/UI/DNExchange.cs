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
        curtain.Exchange(.1f, false);
    }

    public void setNightIcon()
    {
        curtain.Exchange(.1f, true);
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
