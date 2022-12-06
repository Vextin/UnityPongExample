using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//RequireComponent adds a TextMeshPro component if there already isn't one.
//It also prevents you from removing the TextMeshPro component as long as 
//there is a BlinkText component.
[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkText : MonoBehaviour
{
    //How long it has been since we blinked.
    float time = 0f;
    //How many seconds it should take to turn off after turning on, and vice-versa.
    [SerializeField] float secondsPerBlink = 1f;
    //The TextMeshPro component to blink.
    TextMeshProUGUI text;
    
    //Start is called on the first frame.
    void Start ()
    {
        //Get the TextMeshPro component attached to this object.
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //We only want the text to show if there is no Player 2
        if(GameManager.playerTwoIsHuman)
        {
            //"alpha" describes how transparent something is.
            //Alpha of 0 means fully transparent.
            text.alpha = 0f;

            //Exit Update() early.
            return;
        }

        //Increase our timer by how much time has passed since last frame.
        time += Time.deltaTime;

        //If we haven't blinked in [secondsPerBlink] seconds, 
        if(time > secondsPerBlink)
        {
            //set the timer back
            //(you could also do time=0; but this is slightly more accurate.)
            time -= secondsPerBlink;

            //"one minus" is a common operation in games, because it's super useful.
            //alpha of 0 is full transparent.
            //alpha of 1 is full opaque. 
            //in either case, setting alpha to (1 - alpha) switches to the other case.
            //So transparent becomes opaque, and opaque becomes transparent. 
            text.alpha = 1f - text.alpha;
        }
    }
}
