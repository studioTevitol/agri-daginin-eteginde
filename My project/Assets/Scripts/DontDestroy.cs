using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public bool isOnMenus; 
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.GameObject());

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);
        //Debug.Log(isOnMenus);
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Credits")
        {
            isOnMenus = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            isOnMenus = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }       
    }
}
