using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    
    void Awake()
    {
        if(GM != null)
            GameObject.Destroy(GM);
        else
            GM = this;
        
        DontDestroyOnLoad(this);
    }

    public GameObject[] items;

    public static List<Dictionary<string,object>> words = new List<Dictionary<string,object>>();
    public static List<Dictionary<string,object>> wordsToLearn = new List<Dictionary<string,object>>();
    public static List<Dictionary<string,object>> wordsDiscovered = new List<Dictionary<string,object>>();

    FirebaseFirestore db;
    // private Random random = new Random();

    // Start is called before the first frame update

    bool noporfavor = true;
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        GetWordsFromDb();
    }

    // Update is called once per frame
    void Update()
    {
    }

    async void GetWordsFromDb(){
        Query query = db.Collection(Player.topic);
        await query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents){
                Dictionary<string, object> word = documentSnapshot.ToDictionary();
                words.Add(word);
            }
            SetWordsToItems();
        });

    }


    // método para setear las palabras con los objetos
    private static Dictionary<string, object> GetRandomWords(){
        var random = new System.Random();
        var number = random.Next(0,words.Count);
        var word = words[number];
        return word;
    }

    private void SetWordsToItems(){
        var index = 0;
        while(index < items.Length){
            var word = GetRandomWords();
            items[index].GetComponent<ShowWord>().word = word;
            wordsToLearn.Add(word);
            index++;
        }
    }


    // método para obtener palabras random de L2 para enviar al test
    public static string GetWordForTest(){
        var duplicated = false;
        while (!duplicated){
            var word =(Dictionary<string,object>) GetRandomWords();
            if(!wordsToLearn.Contains(word))
                return (string) word[Player.language2];
                // duplicated = true;
        }
        return "";
    }
    
    public static void StartPlay(){
        if(wordsDiscovered.Count == 2){
            SceneManager.LoadScene("Test");
        }
    }

}
