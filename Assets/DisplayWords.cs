using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWords : MonoBehaviour
{
    public GameObject wordLanguage1;
    public GameObject wordLanguage2;

    public string word1 = "Word1";
    public string word2 = "Word2";

    // Start is called before the first frame update
    void Start()
    {
        wordLanguage1.GetComponent<TMPro.TextMeshProUGUI>().text = word1;
        wordLanguage2.GetComponent<TMPro.TextMeshProUGUI>().text = word2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
