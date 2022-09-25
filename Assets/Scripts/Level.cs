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
public abstract class Level : MonoBehaviour
{
    // MenuVr reference singleton -> we take the config from here
    private GameManager gm;
    private Database db;

    public abstract int numWords{get;}
    public abstract string levelName{get;}

    public abstract Dictionary<string, Dictionary<string,object>> levelObjects{get;}
    
    public abstract Dictionary<string,object>[] positionss {get;} 
    public abstract Vector3 [] positions {get;} 
    public Dictionary<string, Dictionary<string,object>> gameObjects = new Dictionary<string, Dictionary<string, object>>();
    public List<Dictionary<string,object>> wordsDiscovered = new List<Dictionary<string,object>>();

    public GameObject[] items;

    public GameObject found;

    void Awake(){
        gm = GameManager.get();
        db = Database.getInstance();
        db.getRandomWords(gm.getRandomWords);
    }

    void Start(){
        //SetWordsToItemsLocal();
        //setItemsWords();
        //setItemsPosition();
        db.setUserGame();
        getStatedData();
        setItemsProperties();

    }

    void setItemsProperties(){
        var names = getGameObjectNames();
        GameObject item;
        foreach(string name in names){
            item = Array.Find(items, item => item.name == name);
            var word = (Dictionary<string, object>)gameObjects[item.name];
            item.GetComponent<ShowWord>().word = word; //setting word
            // var x = Convert.ToSingle(word["x"]);
            // var y = Convert.ToSingle(word["y"]);
            // var z = Convert.ToSingle(word["z"]);
            //item.transform.position = new Vector3(x,y,z); // seting position 
            // item.transform.rotation = Quaternion.Euler(0, word["rot"], 0);
        }
    }


    public string[] getGameObjectNames(){
        string[] names = new string[gameObjects.Keys.Count];
        gameObjects.Keys.CopyTo(names, 0);
        return names;
    }
    public void getRandomObjects(){
        gameObjects = new Dictionary<string, Dictionary<string, object>>();
        string[] all = new string[levelObjects.Keys.Count];
        levelObjects.Keys.CopyTo(all, 0);
        List<string> names = new List<string>(all);
        var index = 0;
        while(index < numWords){
            var randomIndex = new System.Random().Next(0,names.Count);
            var key = names[randomIndex];
            var value = levelObjects[key];
            gameObjects.Add(key, value);
            names.RemoveAt(randomIndex);
            index++;
        }
    }

    void getStatedData(){
        if(gm.config != null)
            createFromConfig();
        else{
            // for(var i=0;i<6;i++){
            //     createRandom();
            // }
        createRandom();
        }
    }

    public void createRandom(){

        db.setLevelConfiguration();

        getRandomObjects();

        List<Vector3> positionsList = new List<Vector3>(positions);

        var names = getGameObjectNames();
        foreach (string name in names){
            var random = new System.Random();
            var randomIndex = random.Next(0,positionsList.Count);
            var position = positionsList[randomIndex];
            positionsList.RemoveAt(randomIndex);
            gameObjects[name]["x"] = position.x;
            gameObjects[name]["y"] = position.y;
            gameObjects[name]["z"] = position.z;
            gameObjects[name]["rot"] = 90;
        }

        db.updateLevelConfig(levelName,gameObjects);
    }

    public void createFromConfig(){
        var config =(Dictionary<string, object>) gm.config[levelName];
        string[] all = new string[config.Keys.Count];
        config.Keys.CopyTo(all, 0);
        foreach(string name in all){
            var value = (Dictionary<string, object>)config[name];
            gameObjects.Add(name,value);
        }
    }


    public void startTest(){
        List<Dictionary<string, object>> wordsToLearn = new List<Dictionary<string, object>>();
        var names = getGameObjectNames();
        Dictionary<string, object> obj;
        foreach(string name in names){
            var lang = gameObjects[name];
            obj = new Dictionary<string, object>();
            obj.Add("English",name);
            obj.Add(Player.language2,lang[Player.language2]);
            obj.Add(Player.language1,lang[Player.language1]);
            wordsToLearn.Add(obj);
        }
        gm.wordsToLearn = wordsToLearn;

        db.updateUserGame("finishTime",DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
        SceneManager.LoadScene("Test");
    }

    public void updateObjectDiscovered(){
        found.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{wordsDiscovered.Count}/{numWords}";
        db.updateUserGame("objectsDiscovered",wordsDiscovered);
        if (wordsDiscovered.Count == numWords){
            reticle.SetActive(false);
            startTestUI.SetActive(true);
            timerIsRunning = true;
        }
    }

    public GameObject startTestUI;
    public GameObject reticle;
    private float timeRemaining = 6;
    private bool timerIsRunning = false;

    void Update(){
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                startTest();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        startTestUI.gameObject.GetComponentInChildren<TimerStartingTest>().gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{seconds}";
    }


}