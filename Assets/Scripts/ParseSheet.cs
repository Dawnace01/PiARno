using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;
using System.IO;

public class ParseSheet : MonoBehaviour
{
    /** 
     * (au format .json) 
     * **/
    public string fileName; 

    public GameObject prefabTile = null;

    public float ConstHeightBloc = 1f;

    private int iterator = 0;

    public double x_scale_key = 1;

    // Start is called before the first frame update
    void Start()
    {
        string str = Read();

        JSONNode json = JSON.Parse(str);

        foreach (JSONNode item in json["content"][2])
        {
            foreach (JSONNode currentHandprint in item["currentHandprint"])
            {
                if (int.Parse(currentHandprint["duration"].Value) > 0) {
                    float y_scale_temp = ConstHeightBloc * int.Parse(currentHandprint["duration"].Value);
                    GameObject temp = Instantiate(prefabTile, new Vector3((float)x_scale_key * int.Parse(currentHandprint["key"].Value), iterator + (y_scale_temp / 2), 0), Quaternion.identity);
                    temp.transform.localScale = new Vector3((float)x_scale_key, y_scale_temp, temp.transform.localScale.z);
                }
            }
            iterator++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private string Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/txtFile/" + fileName + ".json");
        string content = sr.ReadToEnd();
        sr.Close();

        return content;
    }
}
