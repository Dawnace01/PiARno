using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReadMusic : MonoBehaviour
{
    [SerializeField]
    List<GameObject> piano;
    [SerializeField]
    float tempo_a_la_noire = 0.5f;
    [SerializeField]
    GameObject prefab;

    public void jouer(string file)
    {
        StreamReader sr = new StreamReader(file);
        string line = sr.ReadLine();
        List<int> sheetMusic = new List<int>();
        List<float> tempoValues = new List<float>();
        //List<AudioSource> notes = new List<AudioSource>();
        bool resume;
        
        if (line == null)
        {
            Debug.LogError("file empty or not correctly loaded");
        }

        else
        {
            while (line != null)
            {
                string[] words = line.Split(' ');
                resume = true;
                for (int i = 0; i < piano.Capacity && resume; i++) {
                    if (piano[i].transform.name == (string)words.GetValue(0))
                    {
                        sheetMusic.Add(i);
                        resume = false;
                    }
                }
                
                if (words.Length > 1)
                    tempoValues.Add((float)Convert.ToDouble((string)words.GetValue(1)));
                else
                    tempoValues.Add(1f);
                line = sr.ReadLine();
            }
            StartCoroutine(playAutonomous(sheetMusic, tempoValues));
        }
        sr.Close();
    }

    IEnumerator playAutonomous(List<int> notes, List<float> types)
    {
        bool test;
        string s;
        float f;

        for (int i= 0; i < notes.Capacity; i++)
        {
            
            test = true;
            try
            {
                s = piano[notes[i]].transform.name; 
                f = types[i];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                test = false;
            }

            if (test)
                yield return StartCoroutine(play(notes[i], types[i]));
            else
                yield return null;
        }
    }

    IEnumerator play(int i, float type)
    {
        Material mat = piano[i].GetComponent<MeshRenderer>().material;
        AudioSource sound = piano[i].GetComponent<AudioSource>();
        Color old = mat.color;
        mat.color = Color.cyan;
        sound.Play();        
        yield return new WaitForSeconds(tempo_a_la_noire * type);
        sound.Stop();
        mat.color = old;
    }
}
