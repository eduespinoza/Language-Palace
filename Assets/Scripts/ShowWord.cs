using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWord : Actionable
{
    public GameObject canvas;

    private GameObject insCanvas = null;

    public Dictionary<string, object> word;


    public override void PointerDown() {
        // show the word linked to the object
        if(insCanvas == null){
            float x = transform.position.x;
            float y = transform.position.y + 3f;
            float z = transform.position.z;
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
        GameManager.wordsDiscovered.Add(word);
        GameManager.StartPlay();
    }

}
