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
    [SerializeField] private GameObject SlimeL,SlimeR;
    [SerializeField] private GameObject[] levels;
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
        StartCoroutine(GameStart());
    }

    private void rGameOver(){
        o_lLose.SetActive(true);
        o_rWin.SetActive(true);
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart(){//for both players
        yield return new WaitForSeconds(2.0f);
        o_lLose.SetActive(false);
        o_rLose.SetActive(false);
        o_lWin.SetActive(false);
        o_rWin.SetActive(false);

        SlimeL.transform.SetPositionAndRotation(new Vector3(-10,0,0),SlimeL.transform.rotation);
        SlimeR.transform.SetPositionAndRotation(new Vector3(10,0,0) ,SlimeR.transform.rotation);
        Destroy(GameObject.FindWithTag("Finish"));
        Object.Instantiate(levels[Random.Range(0,levels.Length)],transform.position,transform.rotation);
        yield return null;
    }

}
