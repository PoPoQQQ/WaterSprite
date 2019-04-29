using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlantOperate : MonoBehaviour
{
    public Dictionary<Plant.Type, int> seedCnt;

    // Start is called before the first frame update
    void Start()
    {
        seedCnt = new Dictionary<Plant.Type, int>();
        seedCnt.Add(Plant.Type.Water, 0);
        seedCnt.Add(Plant.Type.Buff, 0);
        seedCnt.Add(Plant.Type.Ultimate, 0);
        seedCnt[Plant.Type.Ultimate]++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
