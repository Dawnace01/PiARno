using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;
using System.IO;

using PianoUtilities;

public class ParseSheet : MonoBehaviour
{
    #region public variables
    public string fileName;

    public GameObject prefabTile = null;

    public float ConstHeightBloc = 1f;

    private float spacing;

    public double x_scale_key_white = 1;
    public double x_scale_key_black = .5f;

    public GameObject parent;

    public bool isActive = false;

    public float totalHeight;

    [SerializeField]
    [Range(0f, 20f)]
    float speed = 15f;

    public GameObject[] tabOfKeys;

    public Color leftHandBlocColor = new Color(109, 157, 228);
    public Color rightHandBlocColor = new Color(255, 158, 0);

    private List<GameObject> partitionBlocsCurrent;

    public float score = 0;
    public float scoreError = 0;
    public float scoreTotal = 0;
    #endregion

    #region initial procedures
    // Start is called before the first frame update
    void Start()
    {
        
        partitionBlocsCurrent = new List<GameObject>();
        togglePlayerMode();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && parent.transform.position.y > (totalHeight * -1))
        {
            parent.transform.Translate(Vector3.down * speed / 10 * Time.deltaTime);
            score = 0;
            scoreError = 0;
            scoreTotal = 0;

        }
        else if (totalHeight != 0 && parent.transform.position.y <= -totalHeight)
        {
            Debug.Log("Fin de la partition !");
            parent.transform.position = new Vector3(parent.transform.position.x, 0, parent.transform.position.z);
            isActive = false;
            calcScore();
        }
    }
    #endregion

    #region privates methodes
    private string Read()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/StreamingAssets/txtFile/" + fileName + ".json");
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
                return 0 + getXPosition(numberOfTheKey - 1);
            return 0;
        }

        if (numberOfTheKey > 0)
            return (float)x_scale_key_white + getXPosition(numberOfTheKey - 1);
        return (float)x_scale_key_white / 2;
    }

    private float getTotalHeight()
    {
        float maxHeight = 0f;


        foreach (Transform t in this.parent.GetComponentsInChildren<Transform>())
        {
            if (t.position.y + (t.localScale.y / 2) > maxHeight)
            {
                maxHeight = t.position.y + (t.localScale.y / 2);
            }
        }

        return maxHeight;
    }
    #endregion

    #region public methods
    public void toggleSheet()
    {
        isActive = !isActive;
    }

    public void togglePlayerMode()
    {
        foreach (GameObject go in tabOfKeys)
        {
            go.GetComponent<KeyStates>().isPlayerMode = !go.GetComponent<KeyStates>().isPlayerMode;
        }
    }

    public void calcScore()
    {
        foreach (GameObject go in tabOfKeys)
        {    
            scoreError += go.GetComponent<KeyStates>().cptError;
            scoreTotal += go.GetComponent<KeyStates>().cptTotal;
        }
        score = scoreError / scoreTotal;
        
    }

    public void startGame(string partition)
    {
        parent.transform.position = new Vector3(parent.transform.position.x, 0, parent.transform.position.z);

        fileName = partition;
        float spacing = ConstHeightBloc;
        string str = Read();

        JSONNode json = JSON.Parse(str);


        if (partitionBlocsCurrent.Count > 0)
        {
            foreach (GameObject element in partitionBlocsCurrent)
            {
                Destroy(element);
            }
            partitionBlocsCurrent.Clear();
        }

        partitionBlocsCurrent = new List<GameObject>();

        foreach (JSONNode item in json["content"][2])
        {
            foreach (JSONNode currentHandprint in item["currentHandprint"])
            {
                if (int.Parse(currentHandprint["duration"].Value) > 0)
                {
                    float y_scale_temp = ConstHeightBloc * int.Parse(currentHandprint["duration"].Value);

                    GameObject temp;

                    if (isBlack(int.Parse(currentHandprint["key"].Value)))
                        temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)) + (float)x_scale_key_black - (float)0.4965, spacing + (y_scale_temp / 2) + 1, .6595f + 0.06f), Quaternion.identity, parent.transform);
                    else
                        temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)) - (float)0.4965, spacing + (y_scale_temp / 2) + 1, .6595f + 0.06f), Quaternion.identity, parent.transform);

                    partitionBlocsCurrent.Add(temp);
                    temp.name = currentHandprint["name"][1].Value;
                    // récupération du doigt
                    switch (int.Parse(currentHandprint["finger"].Value))
                    {
                        case 1:
                            temp.GetComponent<Tile>().finger = Fingering.ONE;
                            break;
                        case 2:
                            temp.GetComponent<Tile>().finger = Fingering.TWO;
                            break;
                        case 3:
                            temp.GetComponent<Tile>().finger = Fingering.THREE;
                            break;
                        case 4:
                            temp.GetComponent<Tile>().finger = Fingering.FOUR;
                            break;
                        case 5:
                            temp.GetComponent<Tile>().finger = Fingering.FIVE;
                            break;
                    }
                    // récupération de la main
                    switch (currentHandprint["hand"].Value)
                    {
                        case "RIGHT":
                            temp.GetComponent<Tile>().hand = Hand.RIGHT;
                            temp.GetComponent<MeshRenderer>().material.color = rightHandBlocColor;
                            break;
                        case "LEFT":
                            temp.GetComponent<Tile>().hand = Hand.LEFT;
                            temp.GetComponent<MeshRenderer>().material.color = leftHandBlocColor;
                            break;
                    }

                    if (isBlack(int.Parse(currentHandprint["key"].Value)))
                        temp.transform.localScale = new Vector3((float)x_scale_key_black, y_scale_temp, temp.transform.localScale.z);
                    else
                        temp.transform.localScale = new Vector3((float)x_scale_key_white, y_scale_temp, temp.transform.localScale.z);
                }
            }
            spacing += ConstHeightBloc;
        }

        totalHeight = getTotalHeight();
        Debug.Log(totalHeight);
        Debug.Log(partitionBlocsCurrent);
    }
    #endregion
}