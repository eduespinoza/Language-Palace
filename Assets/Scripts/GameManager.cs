//level manager
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;
using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager{
    private static GameManager gm;
    private Database db;
    public List<Dictionary<string, object>> words = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> wordsToLearn = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> levelsConfig = new List<Dictionary<string, object>>();

    public Dictionary<string, object> config;

    private GameManager(){}
    public static GameManager get(){
        if(GameManager.gm == null)
            GameManager.gm = new GameManager();
        return GameManager.gm;
    }
    public void startConfig(int index){
        config = levelsConfig[index];
        Debug.Log(index);
        SceneManager.LoadScene("OfficeWorld");
    }

    public void getRandomWords(object value){
        words =(List<Dictionary<string,object>>) value;
    }

}