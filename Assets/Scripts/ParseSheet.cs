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

    public double x_scale_key_white = 1;
    public double x_scale_key_black = .5f;

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

                    GameObject temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)), iterator + (y_scale_temp / 2), 0), Quaternion.identity);
                    
                    if (isBlack(int.Parse(currentHandprint["key"].Value)))
                        temp.transform.localScale = new Vector3((float)x_scale_key_black, y_scale_temp, temp.transform.localScale.z);
                    else
                        temp.transform.localScale = new Vector3((float)x_scale_key_white, y_scale_temp, temp.transform.localScale.z);
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

    private bool isBlack(int numberOfTheKey)
    {
        int localKeyValue = (numberOfTheKey - 4) % 12 + 1;
        return numberOfTheKey == 2 ||
                        localKeyValue == 2 ||
                        localKeyValue == 4 ||
                        localKeyValue == 7 ||
                        localKeyValue == 9 ||
                        localKeyValue == 11;
    }

    private float getXPosition(int numberOfTheKey)
    {

        if (isBlack(numberOfTheKey))
        {
            if (numberOfTheKey > 0)
                return (float)x_scale_key_black + getXPosition(numberOfTheKey - 1);

            return (float)x_scale_key_black;
        }

        if (numberOfTheKey > 0)
            return (float)x_scale_key_white + getXPosition(numberOfTheKey - 1);

        return (float)x_scale_key_white;
    }
}
