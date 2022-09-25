using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class SciLevel : Level
{
    public override int numWords {get {return 11;}}
    public override string levelName {get {
        return "SciWorld";}}
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
                },
                new Dictionary<string,object>(){
                    {"position", new Vector3(0,1,0)},
                    {"rotation", 90}
                },
                new Dictionary<string,object>(){
                    {"position", new Vector3(0,1,0)},
                    {"rotation", 90}
                },
                new Dictionary<string,object>(){
                    {"position", new Vector3(0,1,0)},
                    {"rotation", 90}
                },
            };
        }
    }

    public override Dictionary<string, Dictionary<string, object>> levelObjects {
        get{
            return new Dictionary<string, Dictionary<string,object>>
        {
                { "bust", new Dictionary<string,object>{
                    {"Croatian","busto"},
                    {"Hungarian","mellböség"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "fan", new Dictionary<string,object>{
                    {"Croatian","ventilador"},
                    {"Hungarian","fläkt"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "megaphon", new Dictionary<string,object>{
                    {"Croatian","megáfono"},
                    {"Hungarian","hangszóró"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "helmet", new Dictionary<string,object>{
                    {"Croatian","casco"},
                    {"Hungarian","hjälm"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "drill", new Dictionary<string,object>{
                    {"Croatian","taladro"},
                    {"Hungarian","fúró"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "radio", new Dictionary<string,object>{
                    {"Croatian","radio"},
                    {"Hungarian","rádióüzenet"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "sunglasses", new Dictionary<string,object>{
                    {"Croatian","gafas de sol"},
                    {"Hungarian","napszemüveg"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "sword", new Dictionary<string,object>{
                    {"Croatian","espada"},
                    {"Hungarian","kard"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "telescope", new Dictionary<string,object>{
                    {"Croatian","telescopio"},
                    {"Hungarian","távcsö"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "chest", new Dictionary<string,object>{
                    {"Croatian","cofre"},
                    {"Hungarian","mellkas"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
                { "umbrella", new Dictionary<string,object>{
                    {"Croatian","paraguas"},
                    {"Hungarian","esernyö"},
                    {"x",null},{"y",null},{"z",null},{"rot",null}}},
        };
        }
    }
}