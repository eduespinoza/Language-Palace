using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actionable : MonoBehaviour
{
    public GameObject actionAnimation;

    private GameObject insActionController = null;

    protected float t1 = 0, t2 = 0;

    protected float duration = 2.2f;

    protected bool looking = false;

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

    public void PointerEnter(){
        insActionController = (GameObject) Instantiate(actionAnimation,transform.position,transform.rotation);
        insActionController.transform.LookAt(CameraPointer.Instance.transform);
        insActionController.transform.position = new Vector3(insActionController.transform.position.x, insActionController.transform.position.y + 1.3f, insActionController.transform.position.z);
        looking = true;
    }

    public void PointerExit(){
        if (insActionController != null){
            Destroy(insActionController);
            insActionController = null;
        }
        t1 = 0;
        looking = false;
    }
    
    public abstract void PointerDown();


}
