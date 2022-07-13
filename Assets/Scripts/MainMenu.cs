using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;



public class MainMenu : MonoBehaviour
{


    public GameObject contentView;
    public GameObject itemTemplate;

    private bool lang1Selected = false;
    
    private FirebaseApp app;
    FirebaseFirestore db;
    int id = 0;
    Player player = new Player(); 

    PlayerMovement gamepad;
    
    void Awake()
    {
        // checking that firebase connection has been established

        Firebase.FirebaseApp.CheckDependenciesAsync().ContinueWith(checkTask => {
        Firebase.DependencyStatus status = checkTask.Result;
        if (status != Firebase.DependencyStatus.Available) {
            return Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(t => {
            return Firebase.FirebaseApp.CheckDependenciesAsync();
            }).Unwrap();
        } else {
            return checkTask;
        }
        }).Unwrap().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            db = Firebase.Firestore.FirebaseFirestore.DefaultInstance;
            GetLanguages();
        } else {
            Debug.LogError(
            "Could not resolve all Firebase dependencies: " + dependencyStatus);
        }
        });

        setPlayerActionsInMenu();      
    }

    void setPlayerActionsInMenu(){
        gamepad = new PlayerMovement();

        /*** TO DO HANDLE MOVEMENTE THROUGH THE MENU WITH GAMEPAD ***/

        gamepad.UI.Click.performed += ctx => print("awake");
        
        /*** TO DO HANDLE MOVEMENTE THROUGH THE MENU WITH GAMEPAD ***/

    }

    void OnEnable(){
		gamepad.Enable();
	}

    void print(string msg){
        Debug.Log($"Hola fire {msg}");
    }

    public void Start(){
        Debug.Log($" fire {Gamepad.all.Count}");
    }

    public void Update(){
    }


    public void UpdatePlayerName(string name){
        player.updateName(name);
    }

    public void StartPlay(string scene){
        if(Player.language2 != ""){
            SceneManager.LoadScene(scene);
            Debug.Log($"Options selected : {Player.language1} : {Player.language2} ");
        }
    }

    public async void UploadWord(){
        DocumentReference docRef = db.Collection("words").Document("first");
        Dictionary<string, object> user = new Dictionary<string, object>
        {
                { "First", "Alan" },
                { "Middle", "Mathison" },
                { "Last", "Turing" },
                { "Born", 1912 }
        };
        // string usuario = JsonUtility.ToJson(user);
        await docRef.SetAsync(user).ContinueWithOnMainThread(task => {
                Debug.Log("Added data to the alovelace document in the users collection.");
        });
    }


    public void GetLanguages(){
        string popularLangList = "";
        string langList = "";
        string[] popularLanguages = { "Catalan", "English", "French", "Galician", "Greek",
                                    "German", "Italian", "Latin", "Portuguese", "Russian", "Spanish"};
        int index = -3;
        // CollectionReference citiesRef = db.Collection("popular");
        Query query = db.Collection("popular").Limit(1);
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
            {
                Dictionary<string, object> word = documentSnapshot.ToDictionary();
                SortedDictionary<string, object> ordWord = new SortedDictionary<string, object>();
                // Debug.Log(String.Format("Document {0} returned by query State=CA", documentSnapshot.Id));
                foreach (KeyValuePair<string, object> pair in word) {
                    // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                    index = Array.IndexOf(popularLanguages, pair.Key);
                    if(index == -1)
                        ordWord.Add(pair.Key, pair.Value);
                    else
                        addLanguageToContent(pair.Key);
                        // popularLangList += $"{pair.Key}{Environment.NewLine}";
                }
                foreach (KeyValuePair<string, object> pair in ordWord) {
                    // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                    // langList += $"{pair.Key}{Environment.NewLine}";
                    addLanguageToContent(pair.Key);
                    Debug.Log($" langs : {pair.Key}");
                }
            }
        });
    }

    private void addLanguageToContent(string nameObj){
        var copy = Instantiate(itemTemplate);
        copy.gameObject.name = nameObj;
        copy.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = nameObj;
        copy.GetComponent<Button>().onClick.AddListener ( delegate {OptionSelected();} );
        copy.transform.SetParent(contentView.transform, false);
    }

    public void OptionSelected(){
        string aux = (string) EventSystem.current.currentSelectedGameObject.name;
        // string aux = "jfjf";
        Debug.Log($"Option selected : {aux}");
        if (lang1Selected)
            Player.language2 = aux;
        else
            Player.language1 = aux;
    }

    public void LanguageSelected(bool value){
        lang1Selected = value;
    }

    public void TopicSelection(string value){
        Player.topic = value;
    }


}
