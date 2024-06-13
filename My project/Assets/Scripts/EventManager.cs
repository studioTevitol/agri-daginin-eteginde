using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public UnityEvent lWin=new UnityEvent();
    public UnityEvent rWin=new UnityEvent();
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject o_lLose,o_rLose,o_lWin,o_rWin;
    private Text t_lScore,t_rScore;
    [SerializeField] private GameObject SlimeL,SlimeR;
    //[SerializeField] private GameObject[] levels;
    public int i_lScore=0,i_rScore=0;



    
    private void Start()
    {
        
        lWin.AddListener(lGameOver);
        rWin.AddListener(rGameOver);

        o_lLose =Canvas.transform.Find("lLose").gameObject;
        o_lWin  =Canvas.transform.Find("lWin").gameObject;
        o_rLose =Canvas.transform.Find("rLose").gameObject;
        o_rWin  =Canvas.transform.Find("rWin").gameObject;
        t_lScore=Canvas.transform.Find("lScore").gameObject.GetComponent<Text>();
        t_rScore=Canvas.transform.Find("rScore").gameObject.GetComponent<Text>();

        o_lLose.SetActive(false);
        o_rLose.SetActive(false);
        o_lWin.SetActive(false);
        o_rWin.SetActive(false);

        //Object.Instantiate(levels[Random.Range(0,levels.Length)],transform.position,transform.rotation);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    
    //these gets called when lwin and rWin events invoked
    //lwin and rwin events gets controlled in wincondition.cs
    
    private void lGameOver(){
        o_rLose.SetActive(true);
        o_lWin.SetActive(true);
        i_lScore++;
        StartCoroutine(GameStart());
    }

    private void rGameOver(){
        o_lLose.SetActive(true);
        o_rWin.SetActive(true);
        i_rScore++;
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart(){//for both players
        t_lScore.text=i_lScore.ToString();
        t_rScore.text=i_rScore.ToString();

        yield return new WaitForSeconds(2.0f);
        o_lLose.SetActive(false);
        o_rLose.SetActive(false);
        o_lWin.SetActive(false);
        o_rWin.SetActive(false);

        //new level generation starts here ig
        //i will use method to level loading
        //i have to control scene array somewhere
        
        
        
        
        
        
        
        /**
        SlimeL.transform.SetPositionAndRotation(new Vector3(-10,0,0),SlimeL.transform.rotation);
        SlimeR.transform.SetPositionAndRotation(new Vector3(10,0,0) ,SlimeR.transform.rotation);
        Destroy(GameObject.FindWithTag("Finish"));
        Object.Instantiate(levels[Random.Range(0,levels.Length)],transform.position,transform.rotation);
        yield return null;
        */
    }

    
}
