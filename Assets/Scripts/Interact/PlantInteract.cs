using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteract : MonoBehaviour
{
    QuickSeedManager QSM;
    public PointerToPlant PTP;
    Plant currentPlant;


    // Start is called before the first frame update
    void Start()
    {
        QSM=FindObjectOfType<QuickSeedManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlant = PTP.C_PLANT;
        if(currentPlant == null)
        {
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            interact();
        }

        if(Input.GetMouseButtonDown(1))
        {
            remove();
        }
    }

    void interact()
    {
        QSM.plantSeed(currentPlant);
    }

    void remove()
    {
        currentPlant.Remove();
    }


}
