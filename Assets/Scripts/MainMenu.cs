using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;



public class MainMenu : MonoBehaviour
{


    public GameObject contentView;
    public GameObject itemTemplate;

    private bool lang1Selected = false;
    

    FirebaseFirestore db;
    int id = 0;
    Player player = new Player(); 
    public void Start(){
        db = FirebaseFirestore.DefaultInstance;
        GetLanguages();
    }

    public void Update(){}


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


    public async void GetLanguages(){
        string popularLangList = "";
        string langList = "";
        string[] popularLanguages = { "Catalan", "English", "French", "Galician", "Greek",
                                    "German", "Italian", "Latin", "Portuguese", "Russian", "Spanish"};
        int index = -3;
        // CollectionReference citiesRef = db.Collection("popular");
        Query query = db.Collection("popular").Limit(1);
        await query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
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
                }
            }
        });
    }

    private void addLanguageToContent(string nameObj){
        var copy = Instantiate(itemTemplate);
        copy.gameObject.name = nameObj;
        copy.GetComponent<TMPro.TextMeshProUGUI>().text = nameObj;
        copy.GetComponent<Button>().onClick.AddListener ( delegate {OptionSelected();} );
        copy.transform.SetParent(contentView.transform, false);
    }

    public void OptionSelected(){
        string aux = (string) EventSystem.current.currentSelectedGameObject.name;
        Debug.Log($"Option selected : {aux}");
        if (lang1Selected)
            Player.language2 = aux;
        else
            Player.language1 = aux;
    }

    public void LanguageSelected(bool value){
        lang1Selected = value;
    }


}
