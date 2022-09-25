using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ConfigManager : MonoBehaviour
{
    public GameObject container;
    public GameObject prefab;
    private int MIN_INDEX = -3;
    private int MAX_INDEX = 0;

    private GameObject back;
    private GameObject forw;

    private List<GameObject> instances = new List<GameObject>();
    private GameManager gm;

    void Awake(){
        gm = GameManager.get();
        // setMinMaxValues(true);
        back = gameObject.transform.Find("back").gameObject;
        forw = gameObject.transform.Find("forw").gameObject;
    }


    public void setMinMaxValues(bool way){
        destroyInstances();
        if(way){
            MIN_INDEX += 3;
            MAX_INDEX += 3;
            if(MAX_INDEX > gm.levelsConfig.Count){
                MAX_INDEX = gm.levelsConfig.Count;
            }
        }
        else{
            MIN_INDEX -= 3;
            var offset = MAX_INDEX % 3;
            if(offset != 0)
                MAX_INDEX -= offset; 
            else
                MAX_INDEX -= 3;
        }
        back.SetActive(true);
        if(MIN_INDEX == 0){ 
            back.SetActive(false);
        }
        forw.SetActive(true);
        if(MAX_INDEX == gm.levelsConfig.Count){
            forw.SetActive(false);
        }
        prepareConfig();
    }

    void destroyInstances(){
        foreach(GameObject instance in instances){
            Destroy(instance);
        }
        instances = new List<GameObject>();
    }

    void prepareConfig(){
        var index = MIN_INDEX;
        var list = gm.levelsConfig;
        while(index < MAX_INDEX){
            var item = (string)list[index]["date"];
            Debug.Log($"int {item}");
            var insConfig = (GameObject) Instantiate(prefab, container.transform);
            insConfig.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = item;
            insConfig.GetComponent<ConfigButton>().index = index;
            instances.Add(insConfig);
            index++;
        }
    }

    bool a = true;
    void Update(){
        if(a && gm.levelsConfig.Count != 0){
            setMinMaxValues(true);
            a = false;
        }
    }
}