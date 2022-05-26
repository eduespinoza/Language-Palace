using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question{
    public string wordToBeGuessed; 
    public string correctGuess; 
    private string wrongGuess1; 
    private string wrongGuess2;

    public Question(string toBeGuess, string gGuess, string wGuess1, string wGuess2 ){
        wordToBeGuessed = toBeGuess;
        correctGuess = gGuess;
        wrongGuess1 = wGuess1;
        wrongGuess2 = wGuess2;
    }

    public List<string> GetGuesses(){
        return new List<string>{correctGuess,wrongGuess1,wrongGuess2};
    }
}