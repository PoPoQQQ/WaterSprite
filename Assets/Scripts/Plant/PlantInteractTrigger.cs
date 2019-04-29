using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractTrigger : MonoBehaviour
{
    GameObject[] plants;
    GameObject highLightedPlant= null, hp = null;
    public float maxRad = 2F;
    float minRad;
    GameSystem GS;
    // Start is called before the first frame update
    void Start()
    {
        GS = FindObjectOfType<GameSystem>();
        plants = GameObject.FindGameObjectsWithTag("Plant");
    }

    // Update is called once per frame
    void Update()
    {
        hp = null;
        minRad = maxRad;
        if (GS.dayOrNight == GameSystem.DayOrNight.Day)
        {
            foreach (var i in plants)
            {
                float d = ((Vector2)(i.transform.position - transform.position)).magnitude;
                if (d < minRad)
                {
                    hp = i;
                    minRad = d;
                }
            }
        }
        if(hp != highLightedPlant)
        {
            if(highLightedPlant)
                highLightedPlant.GetComponent<PlantUIController>().DisableHighlight();
            if (hp)
                hp.GetComponent<PlantUIController>().EnableHighlight();
            highLightedPlant = hp;
        }
    }

}
