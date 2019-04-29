using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    //public SpriteRenderer sprite;

    GameObject cam;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    IEnumerator FlashCoroutine()
    {
        var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        const float duration = 0.3f;
        const float frequency = 0.05f;
        float _t = 0;

        gameObject.layer = LayerMask.NameToLayer("Flash");
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            _t += Time.deltaTime;
            if(_t >= 2 * frequency)
                _t -= 2 * frequency;
            sr.enabled = (_t >= frequency);
            yield return 0;
        }
        sr.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public void Hurt()
    {
        cam.GetComponent<CameraShake>().Shake();
        StartCoroutine(FlashCoroutine());
    }
}
