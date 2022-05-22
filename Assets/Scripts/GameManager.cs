using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;

public class GameManager : MonoBehaviour
{

    public GameObject[] items;

    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        GetWords();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void GetWords(){
        var size = items.Length;
        var index = 0;
        Query query = db.Collection("popular").Limit(size);
        await query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents){
                Dictionary<string, object> word = documentSnapshot.ToDictionary();
                items[index].GetComponent<ShowWord>().word1 = (string) word[Player.language1];
                items[index].GetComponent<ShowWord>().word2 = (string) word[Player.language2];
                index++;
            }
        });

    }



}
