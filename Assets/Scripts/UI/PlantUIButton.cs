using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantUIButton : MonoBehaviour
{
    public enum ButtonType { None, Seed, Remove, Water , Collect};
    public ButtonType type;
    Button btn;
    public Plant.Type seedType;
    GameObject player;
    Player pl;
    PlayerPlantOperate ppo;
    Text text;
    Plant HLP() => PlantUIController.highlightedPlant;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pl = player.GetComponent<Player>();
        ppo = player.GetComponent<PlayerPlantOperate>();
        btn = GetComponent<Button>();
        if (type == ButtonType.Seed)
            text = GetComponentInChildren<Text>();
    }

    bool CheckInteractable()
    {
        if (!HLP())
            return false;
        switch (type)
        {
            case ButtonType.Seed:
                return ppo.seedCnt[seedType] > 0 && HLP().type == Plant.Type.None;
            case ButtonType.Water:
                return pl.health > ppo.waterCost && HLP().type != Plant.Type.None && !HLP().watered;
            case ButtonType.Remove:
                return HLP().type != Plant.Type.None;
            case ButtonType.Collect:
                return HLP().fruit > 0;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        btn.interactable = CheckInteractable();
        /*
        if (type == ButtonType.Seed)
            text.text = ppo.seedCnt[seedType].ToString();
            */
    }

    public void OnClick()
    {
        if (!HLP())
            return;
        switch(type)
        {
            case ButtonType.Seed:
                ppo.TryPlant(seedType);
                break;
            case ButtonType.Water:
                ppo.TryWater();
                break;
            case ButtonType.Remove:
                ppo.TryRemove();
                break;
            case ButtonType.Collect:
                ppo.TryCollect();
                break;
        }
    }
}
