using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Diagnostics;

public class WinCondition : MonoBehaviour
{
    private GameObject o_EventManager;
    private GameObject scoreManager;
    private EventManager s_EventManager;
    private UnityEvent e_lWin,e_rWin;
    private bool controlflag=false;

    void Start()
    {
        o_EventManager = GameObject.Find("EventManager");
        s_EventManager=o_EventManager.GetComponent<EventManager>();
        e_lWin=s_EventManager.lWin;
        e_rWin=s_EventManager.rWin;
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(controlflag)return;
        StartCoroutine(controlWin());

        UnityEngine.Debug.Log("win2");
        GameObject winner=collision.gameObject;
        System.Diagnostics.Debug.Assert(winner.name=="SlimeL" || winner.name=="SlimeR","who won?");
        if(winner.name=="SlimeL")e_lWin.Invoke();
        else e_rWin.Invoke();
        
        
    }

    IEnumerator controlWin(){
        controlflag=true;
        yield return new WaitForSeconds(2.0f);
        controlflag=false;
    }
}
