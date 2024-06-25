using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class LevelManager : MonoBehaviour
{
       
    //levels array
    //[SerializeField] private Scene[] levels;
    [SerializeField] private List<String> levelNames;
    void Start()
    {
        
    }
    
    void Update()
    {
        //Debug.Log(levelNames.Count);
    }

    
    //will generate array of levels from builded scenes
    private List<String> newlevels()
    {
        Debug.Log("newlevels array method");
        List<String> temp = new List<String>();
        
        /**
        temp.Add(SceneManager.GetSceneAt(2));
        Debug.Log("first scene successful");
        temp.Add(SceneManager.GetSceneAt(3));
        Debug.Log("second scene successful");
        temp.Add(SceneManager.GetSceneAt(4));
        Debug.Log("third scene successful");
        */
        
        //the zeroth and first elemnents are mainmenu scene and credits scene second index is level
        //i've added another scene ccalled winner screen so n has to start from 3
        for (int n = 3; n < SceneManager.sceneCountInBuildSettings; n++)
        {
            //Debug.Log("inside loop" + n);
            temp.Add(SceneUtility.GetScenePathByBuildIndex(n));
        }
        

        return temp;
    }

    //can use it on start button on mainmenu
    public void SetLevelsArray()
    {
        Debug.Log("SET LEVELS ARRAY");
        levelNames = newlevels();
    }
    
    //gets random level by randomindex
    public void LoadNewScene()
    {
        int randomIndex = Random.Range(0, levelNames.Count);
        SceneManager.LoadScene(levelNames[randomIndex]);
        levelNames.RemoveAt(randomIndex);

        //again new scene added to it has to be increased to 3 from 2
        if (levelNames.Count < 3)
        {
            SetLevelsArray();
        }
    }
    
}