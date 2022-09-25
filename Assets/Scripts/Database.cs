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
public class Database
{
    private FirebaseFirestore db;
    private GameManager gm;
    private static Database instance;
    private DocumentReference user;
    private DocumentReference userGames;
    private DocumentReference userTests;
    private DocumentReference levels;

    private Database(){
        db = FirebaseFirestore.DefaultInstance;
        gm = GameManager.get();
        user = db.Collection("user").Document("testUser");
        levels = db.Collection("levels").Document();
    }

    public static Database getInstance(){
        if(Database.instance == null)
            Database.instance = new Database();
        return Database.instance;
    }

    public void newUserGamesReference(){
        userGames = db.Collection("user").Document("testUser").Collection("games").Document();
    }
    public void newUserTestsReference(){
        userTests = db.Collection("user").Document("testUser").Collection("tests").Document();
    }

    public void setUserGame(){
        newUserGamesReference();
        Dictionary<string, object> game = new Dictionary<string, object>
        {
                { "startTime", DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                { "finishTime", null },
                { "world", SceneManager.GetActiveScene().name },
                { "objectsDiscovered", null},
        };
        userGames.SetAsync(game);
    }

    public void updateUserGame(string field, object value){
        userGames.UpdateAsync(field,value);
    }
    public void updateUserTest(string field, object value){
        userTests.UpdateAsync(field,value);
    }
    public void updateLevelConfig(string field, object value){
        levels.UpdateAsync(field,value);
    }

    public void getRandomWords(Action<object> onSuccess){
        db.Collection("popular").GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            List<Dictionary<string, object>> words = new List<Dictionary<string, object>>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents){
                Dictionary<string, object> word = documentSnapshot.ToDictionary();
                words.Add(word);
            }
            //return the words list
            onSuccess?.Invoke(words);
        });
    }

    public void setUserTest(){
        newUserTestsReference();
        Dictionary<string, object> test = new Dictionary<string, object>
        {
                { "startTime", DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") },
                { "finishTime", null },
                { "score", null},
                { "questions", null}
        };
        userTests.SetAsync(test);
    }

    public void getLevelsConfiguration(){
        db.Collection("levels").OrderBy("date").GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            var index = 1;
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents){
                Dictionary<string, object> config = documentSnapshot.ToDictionary();
                config.Add("index", index);
                index++;
                gm.levelsConfig.Add(config);
            }
        });
    }

    public void setLevelConfiguration(){
        // to doooo
        levels = db.Collection("levels").Document();
        Dictionary<string, object> config = new Dictionary<string, object>
        {
            { "OfficeWorld", null },
            { "SciWorld", null},
            { "date", DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")}
        };
        levels.SetAsync(config);
    }

}