using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderElement : MonoBehaviour
{

    public SliderManager sliderManager;
    public Animator animator;
    public int thisIndex;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sliderManager.index == thisIndex){
        animator.SetBool("selected", true);
        if(sliderManager.isPressConfirm)
        {
            animator.SetBool("pressed", true);
        } 
        else if(animator.GetBool("pressed"))
        {
            animator.SetBool("pressed", false);
        }
    } else {
        animator.SetBool("selected", false);
    }
    }
}
