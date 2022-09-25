using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;

    public void StartConfig(){
        GameManager.get().startConfig(index);
    }
}
