using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;
using System.IO;

public class ParseSheet : MonoBehaviour
{

    public string fileName;
    public ArrayList tab = new ArrayList();

    public GameObject prefabTile = null;

    // Start is called before the first frame update
    void Start()
    {
        string str = Read();

        JSONNode json = JSON.Parse(str);

        foreach (JSONNode item in json["content"][2])
        {
            foreach (JSONNode currentHandprint in item["currentHandprint"])
                tab.Add(Instantiate(prefabTile, new Vector3(int.Parse(currentHandprint["key"].Value) / 100, 0, 0), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string Read()
    {
        Debug.LogWarning(Application.dataPath + "/txtFile/" + fileName);
        StreamReader sr = new StreamReader(Application.dataPath + "/txtFile/" + fileName);
        string content = sr.ReadToEnd();
        sr.Close();

        return content;
    }
}
