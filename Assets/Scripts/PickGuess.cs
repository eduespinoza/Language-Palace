using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGuess : Actionable
{
    TestManager testManager;
    void Start(){
        testManager = gameObject.GetComponentInParent(typeof(TestManager)) as TestManager;
    }
    public override void PointerDown() {
        // gameObject.GetComponentInChildren<Button>().Select();
        testManager.CheckGuess(gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);   
    }

}