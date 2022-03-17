using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;
using System.IO;

using PianoUtilities;
using TMPro;

public class ParseSheet : MonoBehaviour
{
    #region public variables
    public string fileName;

    public GameObject prefabTile = null;
    public GameObject prefabTextFingering = null;

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

    public TextMeshPro debugTxt;
    public GameObject plate;
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
            afficheScore();
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
        return tabOfKeys[numberOfTheKey - 1].transform.name.Contains("b") && tabOfKeys[numberOfTheKey - 1].transform.name.Contains("#");
    }

    private float getXPosition(int numberOfTheKey)
    {
        if (numberOfTheKey <= 0 || numberOfTheKey > 88)
        {
            return -1;
        }

        /*

        float cpt = 0;

        for(int i = 1; i < numberOfTheKey; i++)
        {
            if (!isBlack(i)) 
            {
                cpt += (float)x_scale_key_white;
            }
        }

        if (isBlack(numberOfTheKey))
            return cpt + (float)x_scale_key_black;
        return cpt;
        */
        
        return tabOfKeys[numberOfTheKey - 1].transform.position.x;
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

    [System.Obsolete]
    public void startGame(string partition)
    {
        debugTxt.enabled = false;
        plate.SetActive(false);
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

                    GameObject temp,tempText;

                    /*
                    if (isBlack(int.Parse(currentHandprint["key"].Value)))
                        temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)) - (float)0.4965, spacing + (y_scale_temp / 2) + 1, .73f), Quaternion.identity, parent.transform);
                    else
                        temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)) - (float)0.4965, spacing + (y_scale_temp / 2) + 1, .73f), Quaternion.identity, parent.transform);

                    */
                    temp = Instantiate(prefabTile, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)), spacing + (y_scale_temp / 2) + 1, .73f), Quaternion.identity, parent.transform);
                    tempText = Instantiate(prefabTextFingering, new Vector3(getXPosition(int.Parse(currentHandprint["key"].Value)) + .005f, spacing + 1, .73f), Quaternion.identity, parent.transform);

                    partitionBlocsCurrent.Add(temp);
                    temp.name = currentHandprint["name"][1].Value;
                    tempText.name = "Text" + currentHandprint["name"][1].Value;
                    // récupération du doigt
                    switch (int.Parse(currentHandprint["finger"].Value))
                    {
                        case 1:
                            temp.GetComponent<Tile>().finger = Fingering.ONE;
                            tempText.GetComponentInChildren<TextMeshPro>().SetText("1");
                            break;
                        case 2:
                            temp.GetComponent<Tile>().finger = Fingering.TWO;
                            tempText.GetComponentInChildren<TextMeshPro>().SetText("2");
                            break;
                        case 3:
                            temp.GetComponent<Tile>().finger = Fingering.THREE;
                            tempText.GetComponentInChildren<TextMeshPro>().SetText("3");
                            break;
                        case 4:
                            temp.GetComponent<Tile>().finger = Fingering.FOUR;
                            tempText.GetComponentInChildren<TextMeshPro>().SetText("4");
                            break;
                        case 5:
                            temp.GetComponent<Tile>().finger = Fingering.FIVE;
                            tempText.GetComponentInChildren<TextMeshPro>().SetText("5");
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
        
        foreach (GameObject go in tabOfKeys)
        {
            go.GetComponent<KeyStates>().cptError = 0;
            go.GetComponent<KeyStates>().cptTotal = 0;
        }
    

        totalHeight = getTotalHeight();
        Debug.Log(totalHeight);
        Debug.Log(partitionBlocsCurrent);
        
    }

    public void clearPlace()
    {
        if (partitionBlocsCurrent.Count > 0)
        {
            foreach (GameObject element in partitionBlocsCurrent)
            {
                Destroy(element);
            }
            partitionBlocsCurrent.Clear();
        }
        if(isActive == true)
        {
            isActive = !isActive;
        }
    }

    public void afficheScore()
    {
        debugTxt.enabled = true;
        plate.SetActive(true);
        if (score < 0.25)
<<<<<<< HEAD
            debugTxt.SetText("Excellent score ! Passe tout de suite au niveau supérieur.");
        else if (score < 0.50)
            debugTxt.SetText("Bravo ! Encore un petit effort pour atteindre l'excellence.");
        else if (score < 0.75)
            debugTxt.SetText("Aie, quelques erreurs. Rejoue plusieurs fois le niveau ou les précédents.");
        else if (score < 1)
            debugTxt.SetText("Que s'est-il passé ? Ce morceau est trop compliqué, essaies-en un moins difficile !");

=======
            debugTxt.SetText("Parfait");
        else if (score < 0.50)
            debugTxt.SetText("Bien");
        else if (score < 0.75)
            debugTxt.SetText("Moyen");
        else if (score < 1)
            debugTxt.SetText("Dommage");
>>>>>>> 8e237f250489be96668f9ce0512dbfa088f0f89f
    }
    #endregion
}