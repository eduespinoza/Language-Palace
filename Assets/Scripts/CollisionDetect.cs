using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class CollisionDetect : MonoBehaviour
{

    private bool inside = false;
    public GameObject startTestUI;

    private Level level;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartTest(){
        if(inside){
            level.startTest();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {

        // show press start button to begin test
        if (collision.gameObject.CompareTag("Player"))
        {
            inside = true;
            startTestUI.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider collision)
    {

        // hide message press start button to begin test
        if (collision.gameObject.CompareTag("Player"))
        {
            inside = false;
            startTestUI.SetActive(false);
        }
    }

}
