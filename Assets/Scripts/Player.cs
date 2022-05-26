using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;


[FirestoreData]
public class Player
{
    public static string name = "Default";
    public static string language1 = "";
    public static string language2 = "";
    public static string topic = "";

    public Player(){}

    public void updateName(string newName){
        name = newName;
    }
}
