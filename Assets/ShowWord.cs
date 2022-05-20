using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWord : MonoBehaviour
{
    public GameObject actionControll;
    public GameObject canvas;

    private GameObject insActionController = null;
    private GameObject insCanvas = null;
    float t1 = 0, t2 = 0;

    float duration = 2.2f;

    bool looking = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(looking){
            if (t1 >= duration) {
                PointerDown();
                t1 = 0;
            }
            t1 += Time.deltaTime;
        }
    }


    public void PointerEnter() {
        // instantiate the actionControll and play it
        print("Entro la mira dentro del objeto");
        insActionController = (GameObject) Instantiate(actionControll,transform.position,transform.rotation);
        insActionController.transform.LookAt(CameraPointer.Instance.transform);
        insActionController.transform.position = new Vector3(insActionController.transform.position.x, insActionController.transform.position.y + 1.3f, insActionController.transform.position.z);
        looking = true;
    }
    public void PointerExit() {
        // destroy the actionControll
        print("SALIO la mira dentro del objeto");
        if (insActionController != null){
            Destroy(insActionController);
            insActionController = null;
        }
        t1 = 0;
        looking = false;
    }
    public void PointerDown() {
        // show the word linked to the object
        if(insCanvas == null){
            float x = transform.position.x;
            float y = transform.position.y + 3f;
            float z = transform.position.z;
            insCanvas = Instantiate(canvas, transform);
            insCanvas.GetComponent<DisplayWords>().word1 = "hola";
            insCanvas.GetComponent<DisplayWords>().word2 = "hello";
            insCanvas.transform.position = new Vector3(x,y,z);
        }
        print("I've been pointed dooooooown");
        looking = false;

    }



}
