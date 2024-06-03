using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Diagnostics;

public class WinCondition : MonoBehaviour
{
    private GameObject o_EventManager;
    private EventManager s_EventManager;
    private UnityEvent e_lWin,e_rWin;
    // Start is called before the first frame update
    void Start()
    {
        o_EventManager=GameObject.Find("EventManager");
        s_EventManager=o_EventManager.GetComponent<EventManager>();
        e_lWin=s_EventManager.lWin;
        e_rWin=s_EventManager.rWin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("win2");
        GameObject winner=collision.gameObject;

        System.Diagnostics.Debug.Assert(winner.name=="SlimeL" || winner.name=="SlimeR","who won?");
        if(winner.name=="SlimeL")e_lWin.Invoke();
        else e_rWin.Invoke();
    }
}
