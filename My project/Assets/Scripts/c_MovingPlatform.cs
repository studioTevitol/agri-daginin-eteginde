using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class c_MovingPlatform : MonoBehaviour
{
    private Vector3 _startPos;
    [SerializeField] private float speed=2.5f,length=5.0f;
    private float cycle=0.0f;

    void Start(){
        _startPos=transform.position;
    }

    void Update(){
        transform.position=new Vector3(_startPos.x+Mathf.Sin(cycle)*length,_startPos.y+Mathf.Cos(cycle)*length,_startPos.z);
        cycle+=speed*Time.deltaTime/length;
    }
}
