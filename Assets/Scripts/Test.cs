using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test {
    public int nWords;
    public int nGoodGuess;
    public List<Question> wrongAnswers = new List<Question>();

    private List<Dictionary<string, object>> wordsToLearn;

    private List<Dictionary<string,object>> words;

    public List<Question> eachQuestion;

    private Database db;

    private GameManager gm;
    public Test(){
        db = Database.getInstance();
        gm = GameManager.get();
        wordsToLearn = gm.wordsToLearn;
        words = gm.words;
        nWords = wordsToLearn.Count;
        nGoodGuess = 0;
        SetQuestions();
        SetDbInfo();
    }

    private void SetDbInfo(){
        db.setUserTest(); 
    }

    private Dictionary<string, object> GetRandomWords(){
        var random = new System.Random();
        var number = random.Next(0,words.Count);
        var word = words[number];
        return word;
    }
    private string GetWordForTest(){
        var duplicated = false;
        while (!duplicated){
            var word =(Dictionary<string,object>) GetRandomWords();
            if(!wordsToLearn.Contains(word))
                return (string) word[Player.language2];
                // duplicated = true;
        }
        return "";
    }

    private void SetQuestions(){
        eachQuestion = new List<Question>();
        foreach(Dictionary<string, object> word in wordsToLearn){
            var toBeGuess = (string) word[Player.language1];
            var correctGuess = (string) word[Player.language2];
            var wrongGuess1 = GetWordForTest();
            var wrongGuess2 = GetWordForTest();
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