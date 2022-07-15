using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Text Prompt Generator is intended to take a pre-planned list of phrases and output them onto the screen in a TMPro field
/// </summary>
/// Yes I know Generator is a little bit of a lie, sorry =[
public class TextPromptGenerator : MonoBehaviour
{

    public TextMeshProUGUI textPrompt;
    public List<string> promptList;

    //start
    private void Start()
    {
        if (textPrompt == null) { print("No Text Mesh Pro Found on obj: " + transform.name); return; }
        //set text
        textPrompt.text = "";
    }//end of start

    // Update is called once per frame
    private void Update()
    {
        //local input to test promp 
        if (Input.GetKeyDown("p")) { PublishText(); }
    }//end update

    // a function that will output our prompts
    public void PublishText()
    {
        //error check
        if(promptList.Count == 0 || promptList == null) { print("No Promps Found on obj: " + transform.name); return; } 
        if(textPrompt == null) { print("No Text Mesh Pro Found on obj: " + transform.name); return; }

        //random prompt
        int randomInt = Random.Range(0, promptList.Count);
        textPrompt.text = "Prompt: " + promptList[randomInt];
    }//end of publish text

}//end of text prompt generator
