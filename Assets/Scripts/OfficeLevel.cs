using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class OfficeLevel : Level
{
    public override int numWords {get {return 7;}}
    public override string levelName {get {
        return "OfficeWorld";}}

    public override Vector3[] positions{
        get{
            return new Vector3[]{
        new Vector3(0,1,0),new Vector3(0,2,0),new Vector3(0,3,0),new Vector3(0,4,0),new Vector3(0,5,0),
        new Vector3(0,6,0),new Vector3(0,7,0),new Vector3(0,8,0),new Vector3(0,9,0),new Vector3(0,10,0),
        new Vector3(0,11,0),new Vector3(0,12,0),new Vector3(0,13,0),new Vector3(0,14,0),new Vector3(0,15,0)};
        }
    }

    public override Dictionary<string, object>[] positionss {
        get{
            return new []{
                new Dictionary<string,object>(){
                    {"position", new Vector3(0,1,0)},
                    {"rotation", 90}
                }
            };
        }
    }


    public override Dictionary<string, Dictionary<string, object>> levelObjects {
        get{
            return new Dictionary<string, Dictionary<string,object>>
        {
                { "banana", new Dictionary<string,object>{
                    {"Croatian","plátano"},
                    {"Hungarian","banán"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "carrot", new Dictionary<string,object>{
                    {"Croatian","zanahoria"},
                    {"Hungarian","sárgarépa"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "cheese", new Dictionary<string,object>{
                    {"Croatian","queso"},
                    {"Hungarian","sajt"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "pumpkin", new Dictionary<string,object>{
                    {"Croatian","calabaza"},
                    {"Hungarian","tök"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "watermelon", new Dictionary<string,object>{
                    {"Croatian","sandía"},
                    {"Hungarian","görögdinnye"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "strawberry", new Dictionary<string,object>{
                    {"Croatian","fresa"},
                    {"Hungarian","eper"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "garlic_low", new Dictionary<string,object>{
                    {"Croatian","ajo"},
                    {"Hungarian","fokhagyma"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}}
        };
        }
    }
}