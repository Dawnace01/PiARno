using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ManagingUserData : MonoBehaviour
{

    public string fileName;

    // Start is called before the first frame update
    void Start()
    {
        string toParse = Read();
        JSONNode parsed = JSON.Parse(toParse);

        this.GetComponent<TextMesh>().text = parsed.GetValueOrDefault("Name", "Guest") + "\nLvl: " + parsed.GetValueOrDefault("Niveau", "None");
        
    }

    private string Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "StreamingAssets/UserData/" + fileName + ".json");
        string content = sr.ReadToEnd();
        sr.Close();

        return content;
    }
}
