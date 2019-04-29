using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour
{
    public GameObject[] redBar;
    public GameObject[] greenBar;

    public float originY;
    public float targetY;

    public float step;
    public float waitTime;

    public void SetATK(int num)
    {
        for(int i=0;i<num-1;i++)
            redBar[i].SetActive(true);
        StartCoroutine(fillColor(redBar[num-1]));
    }

    public void SetCOST(int num)
    {
        for(int i=0;i<num-1;i++)
            greenBar[i].SetActive(true);
        StartCoroutine(fillColor(greenBar[num-1]));
    }

    IEnumerator fillColor(GameObject set)
    {
        yield return new WaitForSeconds(0.5f);
        set.SetActive(true);
        Color cor = set.GetComponent<Image>().color;
        for(float i=.0f;i<20.0f;i++)
        {
            cor.a = (i/19.0f);
            set.GetComponent<Image>().color = cor;
            yield return null;
        }
    }

    public void ShowScroll()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        while(gameObject.GetComponent<RectTransform>().localPosition.y>targetY)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, gameObject.GetComponent<RectTransform>().localPosition.y - step, gameObject.GetComponent<RectTransform>().localPosition.z);
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, targetY, gameObject.GetComponent<RectTransform>().localPosition.z);
        yield return new WaitForSeconds(waitTime);
        while(gameObject.GetComponent<RectTransform>().localPosition.y<originY)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, gameObject.GetComponent<RectTransform>().localPosition.y + step, gameObject.GetComponent<RectTransform>().localPosition.z);
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, originY, gameObject.GetComponent<RectTransform>().localPosition.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, originY, gameObject.GetComponent<RectTransform>().localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
