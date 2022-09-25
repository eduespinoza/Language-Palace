using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TestManager : MonoBehaviour
{
    
    public GameObject score;
    public GameObject word;
    public GameObject panelUI;
    public GameObject[] guesses;
    private Question currentQuestion;

    private Dictionary<string, Dictionary<string, object>> questions;

    public Test test;

    private GameManager gm;

    private Database db;

    private DateTime init;
    private DateTime finish;

    public GameObject wrongWord;

    void Awake(){
        gm = GameManager.get();
        db = Database.getInstance();
    }

    void Start(){
        questions = new Dictionary<string, Dictionary<string, object>>();
        test  = new Test();
        init = DateTime.Now;
        SetTest();
    }

    void SetScore(string value){
        score.GetComponent<TMPro.TextMeshProUGUI>().text = value;
    }
    void SetWord(string value){
        word.GetComponent<TMPro.TextMeshProUGUI>().text = value;
    }
    void SetGuesses(List<string> values){
        var words = values;
        var random = new System.Random();
        foreach(GameObject guess in guesses){
            var randomIndex = random.Next(0,words.Count);
            var word = words[randomIndex];
            words.RemoveAt(randomIndex);
            guess.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = word;
        }
    }
    
    bool SetTest(){
        var question = test.NextQuestion();
        if(question == null)
            return false;
        currentQuestion = question;
        ResetButtons();
        SetScore($"score : {test.nGoodGuess}/{test.nWords}");
        SetWord(question.wordToBeGuessed);
        SetGuesses(question.GetGuesses());
        return true;
        // SetWord()
    }
    public void CheckGuess(string value){
        StartCoroutine(ExampleCoroutine(value));
    }

    void PrintButtons(string color, string text){
        foreach(GameObject guess in guesses){
            if(guess.GetComponentInChildren<TMPro.TextMeshProUGUI>().text == text)
            {
                var colors = guess.GetComponentInChildren<Button>().colors;
                colors.normalColor = (color == "red") ? Color.red : Color.green;
                guess.GetComponentInChildren<Button>().colors = colors;
            }
        }
    }


    void ResetButtons(){
        foreach(GameObject guess in guesses){
            var colors = guess.GetComponentInChildren<Button>().colors;
            colors.normalColor = Color.white;
            guess.GetComponentInChildren<Button>().colors = colors;
        }
    }

    IEnumerator ExampleCoroutine(string value)
    {
        
        questions.Add(currentQuestion.wordToBeGuessed,
            new Dictionary<string,object>{
                {"correct",currentQuestion.correctGuess},
                {"selected", value},
                {"guessed", false}
        });

        PrintButtons("green",currentQuestion.correctGuess);
        if(currentQuestion.correctGuess == value){
            test.nGoodGuess++;
            SetScore($"score : {test.nGoodGuess}/{test.nWords}");
            questions[currentQuestion.wordToBeGuessed]["guessed"] = true;
        }

        if(currentQuestion.correctGuess != value){
            test.wrongAnswers.Add(currentQuestion);
            PrintButtons("red",value);
        }

        yield return new WaitForSeconds(3);

        if(!SetTest()){
            print("test acabado");
            finish = DateTime.Now;
            // subir datos de test a bd;
            UploadTestInfo();
            db.updateUserTest("finishTime", DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            panelUI.SetActive(true);
            ShowResults();
            //show options: retake test or next level
        }
        UploadTestInfo();
    }


    private void ShowResults(){
        // panelUI.gameObject.GetComponentInChildren<NextLevel>().gameObject.SetActive(false);
        var timeTest = panelUI.gameObject.GetComponentInChildren<TimeTest>().gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TimeSpan totalTime = finish - init;
        timeTest.text = $"Tiempo {(int)totalTime.TotalMinutes}:{totalTime.Seconds:00}";

        var finalScore = panelUI.gameObject.GetComponentInChildren<ScoreTest>().gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        decimal scoreFinal =(test.nGoodGuess * 100)/test.nWords;
        finalScore.text = $"{scoreFinal} %";

        if(scoreFinal < 40){
            panelUI.gameObject.GetComponentInChildren<NextLevel>().gameObject.SetActive(false);
        }

        FillWrong();
    }
    private List<GameObject> instances = new List<GameObject>();

    private void FillWrong(){
        var wrongPanel = panelUI.gameObject.GetComponentInChildren<WrongAnswers>().gameObject;
        foreach (var item in test.wrongAnswers)
        {
            var insWrong = (GameObject)Instantiate(wrongWord, wrongPanel.transform);
            insWrong.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{item.wordToBeGuessed}";
            instances.Add(insWrong);
        }
    }
    

    private void UploadTestInfo(){  
        db.updateUserTest("score", test.nGoodGuess);
        db.updateUserTest("questions", questions);
    }

    void destroyInstances(){
        foreach(GameObject instance in instances){
            Destroy(instance);
        }
        instances = new List<GameObject>();
    }

    public void TryAgain(){
        panelUI.SetActive(false);
        destroyInstances();
        Start();
    }

    public void NextLevel(){
        panelUI.SetActive(false);
        Destroy(panelUI);
        SceneManager.LoadScene("SciWorld");
    }

    public void MainMenu(){
        panelUI.SetActive(false);
        Destroy(panelUI);
        SceneManager.LoadScene("MainVR");
    }

}
