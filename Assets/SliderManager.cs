using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SliderManager : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform rectTransform;
    public int index;
    public int maxIndex;
    PlayerMovement control;
    int horizontalMovement;
    public bool isPressConfirm;
    void Awake()
    {
        control = new PlayerMovement();
		control.Gameplay.SlideLeft.performed += context => horizontalMovement = -1; 
		control.Gameplay.SlideRight.performed += context => horizontalMovement = 1; 
		control.Gameplay.SlideLeft.canceled += context => horizontalMovement = 0; 
		control.Gameplay.SlideRight.canceled += context => horizontalMovement = 0;
		control.Gameplay.StartTest.canceled += context => isPressConfirm = false;
		control.Gameplay.StartTest.performed += context => isPressConfirm = true;
    }
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log($"actual pos {rectTransform.offsetMax} ");
        Debug.Log($"actual pos {rectTransform.offsetMin} ");
        Debug.Log($"actual pos {rectTransform.rect} ");
        Debug.Log($"actual pos {rectTransform.anchorMax} ");
        Debug.Log($"actual pos {rectTransform.anchorMin} ");
    }
    void OnEnable(){
		control.Gameplay.Enable();
	}

    // Update is called once per frame
    void Update()
    {
        if (horizontalMovement != 0){
            if(horizontalMovement > 0){
                if (index < maxIndex)
                {
                    index++;
                    if (index > 1 && index < maxIndex)
                    {
                        rectTransform.offsetMin -= new Vector2(-80, rectTransform.offsetMin.y);
                    }
                }
                else
                {
                    index = 0;
                    rectTransform.offsetMin = Vector2.zero;
                }
            } else if(horizontalMovement < 0) {    
                if (index > 0)
                {
                    index--;
                    if(index < maxIndex - 1 && index > 0)
                    {
                        rectTransform.offsetMin -= new Vector2(80, rectTransform.offsetMin.y);
                    }
                } 
                else
                {
                    index = maxIndex;
                    rectTransform.offsetMin = new Vector2((maxIndex - 2) * 80, rectTransform.offsetMin.y);
                }
            } 
        }
    }
}
