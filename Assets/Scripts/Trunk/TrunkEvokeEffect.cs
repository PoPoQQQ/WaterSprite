using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkEvokeEffect : MonoBehaviour
{
    ParticleSystem.ExternalForcesModule psef;
    ParticleSystem.EmissionModule em;
    float evokeTime = 2F;

    IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(evokeTime);
        em.rateOverTimeMultiplier = 0F;
        yield return new WaitForSeconds(0.6F);
        Destroy(gameObject);
    }

    public void Set(float t, GameObject obj)
    {
        psef = GetComponent<ParticleSystem>().externalForces;
        em = GetComponent<ParticleSystem>().emission;
        psef.RemoveAllInfluences();
        psef.AddInfluence(obj.GetComponent<ParticleSystemForceField>());
        evokeTime = t;
        em.rateOverTimeMultiplier = 15F;
        StartCoroutine(EffectCoroutine());
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
