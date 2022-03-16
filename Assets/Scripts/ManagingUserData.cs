using Microsoft.MixedReality.Toolkit.Utilities;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ManagingUserData : MonoBehaviour
{
    //Generating user's history
    public GridObjectCollection listParent;
    public GameObject prefabElementList;

    //Getting user's data
    public string fileName;
    public TextMesh userNameAndLvl;

    // Start is called before the first frame update
    void Start()
    {
        string toParse = Read();
        JSONNode parsed = JSON.Parse(toParse);

        userNameAndLvl.text = parsed.GetValueOrDefault("Name", "Guest") + "\nLvl: " + parsed.GetValueOrDefault("Niveau", "None");

        foreach(JSONNode game in parsed.GetValueOrDefault("Game", "none"))
        {
            GameObject temp;
            temp = Instantiate(prefabElementList, listParent.transform);
            temp.transform.Find("Song").gameObject.GetComponent<TextMesh>().text = game.GetValueOrDefault("Song", "Error");
            temp.transform.Find("Score").gameObject.GetComponent<TextMesh>().text = game.GetValueOrDefault("Score", "Error");
            string date = game.GetValueOrDefault("Date", "Error");
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(long.Parse(date));
            temp.transform.Find("Date").gameObject.GetComponent<TextMesh>().text = dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year;
            listParent.UpdateCollection();
        }
    }

    private string Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets/UserData/" + fileName + ".json");
        string content = sr.ReadToEnd();
        sr.Close();

        return content;
    }

    public void generateHistory()
    {
        GameObject temp;
        temp = Instantiate(prefabElementList);
    }
}
