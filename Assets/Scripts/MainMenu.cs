using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;



public class MainMenu : MonoBehaviour
{

    public GameObject textList;

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
        SceneManager.LoadScene(scene);
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
        string langList = "";

        // CollectionReference citiesRef = db.Collection("popular");
        Query query = db.Collection("popular").Limit(1);
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
            {
                Dictionary<string, object> word = documentSnapshot.ToDictionary();
                SortedDictionary<string, object> ordWord = new SortedDictionary<string, object>();
                Debug.Log(String.Format("Document {0} returned by query State=CA", documentSnapshot.Id));
                foreach (KeyValuePair<string, object> pair in word) {
                    // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                    ordWord.Add(pair.Key, pair.Value);
                }
                foreach (KeyValuePair<string, object> paire in ordWord) {
                    // Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                    langList += $"{paire.Key}{Environment.NewLine}";
                }
            }
            textList.GetComponent<TMPro.TextMeshProUGUI>().text = langList;
        });

    }


}
