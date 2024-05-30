using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject o_EventManager;
    private EventManager s_EventManager;
    private UnityEvent e_lWin,e_rWin;
    // Start is called before the first frame update
    void Start()
    {
        s_EventManager=o_EventManager.GetComponent<EventManager>();
        e_lWin=s_EventManager.lWin;
        e_rWin=s_EventManager.rWin;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("win");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("win2");
    }
}
