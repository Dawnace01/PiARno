using Microsoft.MixedReality.Toolkit.Utilities;
using SimpleJSON;
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

        //Only for test
        for (int i = 0; i < 20; i++)
        {
            GameObject temp;
            temp = Instantiate(prefabElementList, listParent.transform);
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
