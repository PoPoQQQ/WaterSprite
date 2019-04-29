using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bar1;
    public GameObject bar2;

    float fullHealth;
    float currentHealth;

    public int updateStep;

    void Awake()
    {
        fullHealth=100;
        currentHealth=50;

        updateNew();
    }

    void updateNew()
    {
        bar1.GetComponent<RectTransform>().localScale = new Vector3( currentHealth/fullHealth, 1, 1);
        bar2.GetComponent<RectTransform>().localScale = new Vector3( currentHealth/fullHealth, 1, 1);
    }

    

    public void updateHealth(float health, float current)
    {
        //Debug.Log(currentHealth);
        if(health == fullHealth && current == currentHealth)
            return;
        if(health != fullHealth)
        {
            float currentScale = gameObject.GetComponent<RectTransform>().localScale.x;
            currentScale = currentScale*(health/fullHealth);
            fullHealth = health;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(currentScale, 1.0f, 1.0f);
            updateNew();
            
        }
        else{
            StartCoroutine(updateHealthBar(current - currentHealth));
        }
        currentHealth = current;
        fullHealth = health;
    }

    public void updateHealth(float current) => updateHealth(fullHealth, current);

    IEnumerator updateHealthBar(float healthchange)
    {
        float scalechange = healthchange/fullHealth;
        if(healthchange >0)
        {
            transFormScaleChange(bar1, scalechange);
            transFormScaleChange(bar2, scalechange);
            yield return null;
        }
        else{
            transFormScaleChange(bar2, scalechange);
            StartCoroutine(HealthBarDelay(bar1, scalechange));
        }
    }

    IEnumerator HealthBarDelay(GameObject bar, float scaleChange)
    {
        for(int i=0;i<updateStep;i++)
        {
            transFormScaleChange(bar, scaleChange/10.0f);
            yield return null;
        }
    }

    void transFormScaleChange(GameObject bar, float change)
    {
        bar.GetComponent<RectTransform>().localScale = new Vector3( bar.GetComponent<RectTransform>().localScale.x + change, 1, 1);
    }
}
