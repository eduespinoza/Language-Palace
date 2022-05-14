using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;


[FirestoreData]
public class Player
{
    public static string name = "Default";

    public Player(){}

    public void updateName(string newName){
        name = newName;
    }
}
