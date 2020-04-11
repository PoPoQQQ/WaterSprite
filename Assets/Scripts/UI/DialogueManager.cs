using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    public Queue<string> dialogues;
    [TextArea(3, 10)]
    public string[] sentences;

    public Text dialogue;
    public Text nextButton;

    IEnumerator showsentence;

    public bool dialogueOver;
    public FadingCurtain cover;
    public FadingCurtain startMenu;

    bool end;
    void Awake()
    {
        dialogues = new Queue<string>();
        dialogueOver = true;
        end = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        /* foreach(string sentence in sentences)
            dialogues.Enqueue(sentence);
        showsentence = showText(dialogues.Dequeue());
        StartCoroutine(showsentence);*/
        showCover();
        
    }

    // Update is called once per frame
    void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!dialogueOver)
            {
                string sentence = dialogues.Dequeue();
                showsentence = showText(sentence);
                StartCoroutine(showsentence);
                if(dialogues.Count == 0)
                dialogueOver = true;
            }
            else if(dialogueOver && !end)
            {
                showCover();
                end = true;
            }
        }*/
        
        
    }

    void showCover()
    {
        //yield return new WaitForSeconds(2.0f);
        cover.gameObject.SetActive(true);
        StartCoroutine(cover.FadingReverseCoroutine(80));
        startMenu.gameObject.SetActive(true);
        StartCoroutine(startMenu.FadingReverseCoroutine(80));

    }


    IEnumerator showText(string sentence)
    {
        dialogue.text = "";
        foreach (char letter in sentence.ToCharArray() )
        {
            dialogue.text += letter;
            yield return null;
        }
    }

    public void load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


}
