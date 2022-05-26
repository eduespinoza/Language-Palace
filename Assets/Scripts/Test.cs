using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test {
    public int nWords;
    public int nGoodGuess;

    public List<Question> eachQuestion;

    public Test(){
        nWords = GameManager.wordsDiscovered.Count;
        nGoodGuess = 0;
        SetQuestions();
    }

    private void SetQuestions(){
        eachQuestion = new List<Question>();
        Debug.Log($"words {GameManager.wordsToLearn }");
        foreach(Dictionary<string, object> word in GameManager.wordsToLearn){
            // var toBeGuess = "Holas";
            // var correctGuess =  "Holas2";
            // var wrongGuess1 = "Holas333";
            // var wrongGuess2 = "Holas22";
            var toBeGuess = (string) word[Player.language2];
            var correctGuess = (string) word[Player.language1]; 
            var wrongGuess1 = GameManager.GetWordForTest();
            var wrongGuess2 = GameManager.GetWordForTest();
            // cuanta suerte tiene que haber para que wguess1==wguess2 ???
            var question = new Question(toBeGuess,correctGuess,wrongGuess1,wrongGuess2);
            eachQuestion.Add(question);
        }
    }

    public Question NextQuestion(){
        var size = eachQuestion.Count;
        if(size == 0)
            return null;
        var random = new System.Random();
        var randomIndex = random.Next(0,size);
        var question = eachQuestion[randomIndex];
        eachQuestion.RemoveAt(randomIndex);
        return question;
    }
}