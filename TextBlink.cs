using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//Niv Porat and Artiom Sheremetiev
/// <summary>
/// class is used to display flashing text at splashscreen
/// a UI related class
/// </summary>
public class TextBlink : MonoBehaviour
{

    Text flashingText;

    void Start()
    {
        //get the Text component
        flashingText = GetComponent<Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text
    public IEnumerator BlinkText()
    {
        //blinking forever
        while (true)
        {
            //set the Text's text to blank
            flashingText.text = "";
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.5f);
            flashingText.text = "PRESS ANY KEY TO CONTINUE";
            yield return new WaitForSeconds(.5f);
        }
    }
    private void Update()
    {//loads next scene on any input
        if (Input.anyKey)
            SceneManager.LoadScene(1);

    }
    

}