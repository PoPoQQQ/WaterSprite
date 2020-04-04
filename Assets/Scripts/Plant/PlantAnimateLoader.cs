using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAnimateLoader : MonoBehaviour
{
    Animator anim;
    AnimatorOverrideController overrideController;

    void Awake()
    {
        anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = anim.runtimeAnimatorController;

    }

    public void loadAnimation(Plant.Type seedType)
    {
        CollecableSeed temp = ScriptableObject.CreateInstance("CollecableSeed") as CollecableSeed;
        temp = Resources.Load<CollecableSeed>(CollecableSeed.seedDictionary[seedType]);
        string path = "PlantAnimate/" + temp.name;
        AnimationClip ani = Resources.Load<AnimationClip>(path);
        overrideController["Aquabud"] = ani;
        anim.runtimeAnimatorController = overrideController;
    }
}
