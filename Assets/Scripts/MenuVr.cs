using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Analytics;

public class MenuVr : MonoBehaviour
{
    public GameObject btnOffice;
    public GameObject btnFactory;

    private string worldSelected = "";

    private Database db;

    void Awake(){
        db = Database.getInstance();
        db.getLevelsConfiguration();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log($"start vrr ");
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void setWorld(string world)
    {
        worldSelected = world;
    }

    public void setButtonColor(string btn){
        var office = btnOffice.GetComponentInChildren<Button>();
        var factory = btnFactory.GetComponentInChildren<Button>();
        var cb = office.colors;
        cb.normalColor = Color.green;

        if(btn.Contains("office"))
            office.colors = cb;
        else factory.colors = cb;

        cb.normalColor = Color.white;

        if(btn.Contains("factory"))
            office.colors = cb;
        else factory.colors = cb;
    }

    public void StartPlay()
    {
        SceneManager.LoadScene("OfficeWorld");
    }
}
