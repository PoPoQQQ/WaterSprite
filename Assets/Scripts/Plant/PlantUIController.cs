using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlantUIController : MonoBehaviour
{
    public static Plant highlightedPlant = null;
    bool isHighlighted = false;
    GameObject UITop, UIBottom;
    
    static bool UIEnabled = false;
    void UIEnable()
    {
        if (UIEnabled)
            return;
        float canvasY = UITop.transform.parent.position.y;
        UITop.transform.DOMoveY(canvasY + 0F, 0.6F);
        UIBottom.transform.DOMoveY(canvasY + 0F, 0.6F);
        UIEnabled = true;
    }

    void UIDisable()
    {
        if (!UIEnabled) 
            return;
        float canvasY = UITop.transform.parent.position.y;
        UITop.transform.DOMoveY(canvasY + 500F, 0.6F);
        UIBottom.transform.DOMoveY(canvasY - 500F, 0.6F);
        UIEnabled = false;
    }

    public void EnableHighlight()
    {
        isHighlighted = true;
        highlightedPlant = GetComponent<Plant>();
        //UIEnable();
    }
    public void DisableHighlight()
    {
        isHighlighted = false;
        highlightedPlant = null;
        //UIDisable();
    }
    // Start is called before the first frame update
    void Start()
    {
        UITop = GameObject.Find("PlantUI - Top");
        UIBottom = GameObject.Find("PlantUI - Bottom");
    }

    // Update is called once per frame
    void Update()
    {

    }

}
