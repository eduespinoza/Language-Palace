using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShowWord : Actionable
{
    public GameObject canvas;

    private GameObject insCanvas = null;

    public Dictionary<string, object> word;

    private float x,y,z;

    private Level level;

    void Awake(){
        level = gameObject.GetComponentInParent<Level>();
        Debug.Log($"level {level == null}");
    }

    public override void PointerDown() {
        // show the word linked to the object
        if(insCanvas == null){
            x = transform.position.x;
            y = transform.position.y + 1.7f;
            z = transform.position.z;
            insCanvas = Instantiate(canvas, transform,true);
            insCanvas.GetComponent<DisplayWords>().word1 = (string) word[Player.language1];
            insCanvas.GetComponent<DisplayWords>().word2 = (string) word[Player.language2];
            insCanvas.transform.position = new Vector3(x,y,z);
            insCanvas.transform.rotation = transform.rotation;
            SendWordDiscovered();
        }
        looking = false;
    }

    private void SendWordDiscovered(){
        // send info about the object name, object position, timestamp, word

        Dictionary<string, object> wordDiscovered = new Dictionary<string, object>
        {
                { "time", DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                { "name", gameObject.name },
                { "position",  $"x:[{x}]-y:[{y}]-z:[{z}]"},
                { "word", word}
        };        
        level.wordsDiscovered.Add(wordDiscovered);
        level.updateObjectDiscovered();
    }

}
