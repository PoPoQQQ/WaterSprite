using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        dialogues = new Queue<string>();
        dialogueOver = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach(string sentence in sentences)
            dialogues.Enqueue(sentence);
        showsentence = showText(dialogues.Dequeue());
        StartCoroutine(showsentence);
        /* foreach (string sentence in sentences)
        {
            IEnumerator showsentence = showText(sentence);
            yield return StartCoroutine(showsentence);
            if(GetKeyDown(KeyCode.Space))
            {
                StopCoroutine(showsentence);
            }
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !dialogueOver)
        {
            string sentence = dialogues.Dequeue();
            showsentence = showText(sentence);
            StartCoroutine(showsentence);
            if(dialogues.Count == 0)
            dialogueOver = true;
        }
        
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


}
