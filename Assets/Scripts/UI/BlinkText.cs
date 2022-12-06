using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkText : MonoBehaviour
{
    float time = 0f;
    [SerializeField] float secondsPerBlink = 1f;
    TextMeshProUGUI text;
    void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.playerTwoIsHuman)
        {
            text.alpha = 0f;
            return;
        }

        time += Time.deltaTime;
        if(time > secondsPerBlink)
        {
            time -= secondsPerBlink;
            text.alpha = 1f - text.alpha;
        }
    }
}
