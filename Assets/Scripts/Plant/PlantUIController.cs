using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUIController : MonoBehaviour
{
    static int HighlightedPlantCnt = 0;
    bool isHighlighted = false;

    static void UIEnable()
    {

    }

    static void UIDisable()
    {

    }



    public void EnableHighlight()
    {
        isHighlighted = true;
        HighlightedPlantCnt ++;
        if (HighlightedPlantCnt == 1)
            UIEnable();
    }
    public void DisableHighlight()
    {
        isHighlighted = false;
        HighlightedPlantCnt --;
        if (HighlightedPlantCnt == 0)
            UIDisable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
