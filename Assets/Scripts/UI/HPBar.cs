using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bar1;
    public GameObject bar2;
    public GameObject allBar;
    public GameObject frontImage;

    public Text number;

    float fullHealth;
    float currentHealth;

    float frontWidth;
    float frontHeight;

    public int updateStep;

    void Awake()
    {
        fullHealth=100;
        currentHealth=100;
        frontWidth = frontImage.GetComponent<RectTransform>().rect.width;
        frontHeight = frontImage.GetComponent<RectTransform>().rect.height;
        updateNew();
    }

    void updateNew()
    {
        bar1.GetComponent<RectTransform>().localScale = new Vector3( currentHealth/fullHealth, 1, 1);
        bar2.GetComponent<RectTransform>().localScale = new Vector3( currentHealth/fullHealth, 1, 1);
        number.text = (string)((int)currentHealth + " / " + (int)fullHealth);
    }

    

    public void updateHealth(float health, float current)
    {
        if(health == fullHealth && current == currentHealth)
            return;
        if(health != fullHealth)
        {
            float currentScale = allBar.GetComponent<RectTransform>().localScale.x;
            float delta = allBar.GetComponent<RectTransform>().rect.width * currentScale*(health/fullHealth - 1.0f);
            //Debug.Log(delta);
            currentScale = currentScale*(health/fullHealth);
            fullHealth = health;
            allBar.GetComponent<RectTransform>().localScale = new Vector3(currentScale, 1.0f, 1.0f);
            frontWidth += delta;
            frontImage.GetComponent<RectTransform>().sizeDelta = new Vector2(frontWidth,frontHeight);
            
            
        }
        else{
            StartCoroutine(updateHealthBar(current - currentHealth));
        }
        currentHealth = current;
        fullHealth = health;
        updateNew();
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

    void Update()
    {
        
    }

}
