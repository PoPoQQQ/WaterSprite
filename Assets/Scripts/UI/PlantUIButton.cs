using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUIButton : MonoBehaviour
{
    public PlantUIController.UISelectStatus type;
    public Plant.Type seedType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        PlantUIController.uiSelectStatus = type;
        PlantUIController.selectedSeedType = seedType;
    }
}
