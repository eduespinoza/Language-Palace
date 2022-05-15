using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GetPlayerName : MonoBehaviour
{
    public GameObject m_text;

    public void getPlayerName(){
        m_text.GetComponent<TMPro.TextMeshProUGUI>().text = "Hola " + Player.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        getPlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
