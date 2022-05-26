using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    
    public GameObject score;
    public GameObject word;
    public GameObject[] guesses;
    private Question currentQuestion;

    public Test test;



    // Start is called before the first frame update
    void Start()
    {
        test = new Test();
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
        
        PrintButtons("green",currentQuestion.correctGuess);

        if(currentQuestion.correctGuess == value){
            test.nGoodGuess++;
            SetScore($"score : {test.nGoodGuess}/{test.nWords}");
        }

        if(currentQuestion.correctGuess != value){
            PrintButtons("red",value);
        }

        yield return new WaitForSeconds(3);

        if(!SetTest()){
            print("test acabado");
        }
    }


}
