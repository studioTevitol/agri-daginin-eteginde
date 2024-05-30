using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public UnityEvent lWin=new UnityEvent();
    public UnityEvent rWin=new UnityEvent();
    [SerializeField] private GameObject Canvas;
    private GameObject o_lLose,o_rLose,o_lWin,o_rWin;
    // Start is called before the first frame update
    private void Start()
    {
        lWin.AddListener(lGameOver);
        rWin.AddListener(rGameOver);

        o_lLose=Canvas.transform.Find("lLose").gameObject;
        o_lWin =Canvas.transform.Find("lWin").gameObject;
        o_rLose=Canvas.transform.Find("rLose").gameObject;
        o_rWin =Canvas.transform.Find("rWin").gameObject;

        o_lLose.SetActive(false);
        o_rLose.SetActive(false);
        o_lWin.SetActive(false);
        o_rWin.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void lGameOver(){
        o_rLose.SetActive(true);
        o_lWin.SetActive(true);
        StartCoroutine(GameOver());
    }

    private void rGameOver(){
        o_lLose.SetActive(true);
        o_rWin.SetActive(true);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver(){//for both players
        yield return new WaitForSeconds(2.0f);
        o_lLose.SetActive(false);
        o_rLose.SetActive(false);
        o_lWin.SetActive(false);
        o_rWin.SetActive(false);
        yield return null;
    }

}
