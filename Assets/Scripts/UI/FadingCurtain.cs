using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingCurtain : MonoBehaviour
{

    public Image image;
    public Color col;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        col = image.color;
        col.a = .0f;
        image.color = col;
    }

    public void exchange(float sec)
    {
        StartCoroutine(exchangeIe(sec));
    }



    IEnumerator exchangeIe(float sec)
    {
        yield return StartCoroutine(fadingReverse(70));
        yield return new WaitForSeconds(sec);
        yield return StartCoroutine(fading(70));
        
    } 


    public IEnumerator fading(int step)
    {
        for(int i=1;i<=step;i++)
        {
            col.a = ((float)(step-i))/((float)step);
            image.color = col;
            yield return null;
        }
    }

    public IEnumerator fadingReverse(int step)
    {
        for(int i=1;i<=step;i++)
        {
            col.a = ((float)i)/((float)step);
            image.color = col;
            //Debug.Log(col.a);
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown("o"))
            setFading(false, 70);
        if(Input.GetKeyDown("p"))
            setFading(true, 70);*/
        
    }
}
