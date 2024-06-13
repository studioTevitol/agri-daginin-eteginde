using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
       
    //levels array
    //[SerializeField] private Scene[] levels;
    [SerializeField] private List<Scene> levels;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    
    //will generate array of levels from builded scenes
    private List<Scene> newlevels()
    {
        List<Scene> temp = new List<Scene>();
        
        //the zeroth and first elemnents are mainmenu scene and credits scene second index is level
        for (int n = 2; n <= SceneManager.sceneCount; n++)
        {
            temp.Add(SceneManager.GetSceneAt(n));
        }

        return temp;
    }

    //can use it on start button on mainmenu
    private void SetLevelsArray()
    {
        levels = newlevels();
    }
    
    //gets random level by randomindex
    private void LoadNewScene()
    {
        int randomIndex = Random.Range(2, levels.Count);
        SceneManager.LoadScene(levels[randomIndex].name);
        levels.RemoveAt(randomIndex);
    }

    

}
