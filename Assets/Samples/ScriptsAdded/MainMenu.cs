using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;



public class MainMenu : MonoBehaviour
{
    FirebaseFirestore db;
    int id = 0;
    Player player = new Player(); 
    public void Start(){
        db = FirebaseFirestore.DefaultInstance;
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
}
