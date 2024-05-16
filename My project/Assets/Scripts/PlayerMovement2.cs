using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour{
    private GameObject pivot,line,target;
    private SpriteRenderer sr_player,sr_line,sr_target;
    private Rigidbody2D rb2dplayer;
    [SerializeField] private int currentState = 0;
    [SerializeField] private float strength;

    void Start(){
        strength = 500f;

        pivot = transform.GetChild(0).gameObject;
        line = pivot.transform.GetChild(0).gameObject;
        target = line.transform.GetChild(0).gameObject;
        Debug.Log(pivot.name);
        Debug.Log(line.name);
        Debug.Log(target.name);
        rb2dplayer = GetComponent<Rigidbody2D>();
        sr_line = line.GetComponent<SpriteRenderer>();
        sr_target = target.GetComponent<SpriteRenderer>();
        sr_line.enabled = false;
        sr_target.enabled = false;
        sr_player = GetComponent<SpriteRenderer>();
        resetTransform();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space pressed");
            currentState++;
            if (currentState == 1)startRotationTarget();
            if (currentState == 2)startForceTarget();
            if (currentState == 3)shootPlayer();
        }

        //to test jumping
        if(Input.GetKeyDown(KeyCode.P)){
            Debug.Log("p pressed");
            currentState=0;
            resetTransform();
        }

        //checking if player stopped and resetting it
        if(isPlayerStopped()){
            resetTransform();
            currentState=0;

        }

        //testing target position
        //it starts at fucking random position
        Debug.Log(target.transform.localPosition);
    }

    void startRotationTarget(){
        sr_line.enabled = true;
        StartCoroutine(RotatePivot());
    }

    IEnumerator RotatePivot(){
        float lineRotateSpeed = 360f; // Set the rotation speed here
        Quaternion initialRotation = pivot.transform.rotation;
        
        while (true){
            int flag = 0;
            for (float angle = 0f; angle <= 180f; angle += lineRotateSpeed * Time.deltaTime){
                if (currentState > 1){
                    flag = 1;
                    break;
                }
                pivot.transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
                yield return null;
            }
            if (flag == 1)break;
            for (float angle = 180f; angle >= 0f; angle -= lineRotateSpeed * Time.deltaTime){
                if (currentState > 1){
                    flag = 1;
                    break;
                }
                pivot.transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
                yield return null;
            }
            if (flag == 1)break;
        }
        Debug.Log("Rotation finished");
    }

    void startForceTarget(){
        sr_target.enabled = true;
        StartCoroutine(ChangeLineLength());
    }

    IEnumerator ChangeLineLength(){
        Debug.Log(target.transform.localPosition);
        target.transform.localPosition = new Vector3(-0.5f,0f,0f);
        
        Debug.Log("ChangeLineLength started");
        float lineChangeSpeed = 5f; // Set the line change speed here

        //trying to stop loop below //letss gooo i've done it 
        //however i guess i'm working pretty its fricking 4.52am and i've just messing with these bugs and to fix them i'm just pluging in some lines of code idk 
    
            while (true && currentState==2){

                target.transform.localPosition = new Vector3(-0.5f,0f,0f);

                int flag = 0;
                for(float x=-0.5f;x<=0.5f;x+=lineChangeSpeed*Time.deltaTime){
                    if (currentState > 2){
                        flag = 1;
                        break;
                    }
                    target.transform.localPosition = new Vector3(x,0f,0f);
                    yield return null;
                }
                for(float x=0.5f;x>=-0.5f;x-=lineChangeSpeed*Time.deltaTime){
                    if (currentState > 2){
                        flag = 1;
                        break;
                    }
                    target.transform.localPosition = new Vector3(x,0f,0f);
                    yield return null;
                }
                if (flag == 1)break;
            }
        

        Debug.Log("ChangeLineLength finished");
        

        yield return null;
    }

    void shootPlayer(){
        sr_line.enabled = false;
        sr_target.enabled = false;
        StartCoroutine(_shootPlayer());
    }

    IEnumerator _shootPlayer(){
        sr_player.color = Color.white;
        Vector3 shootVector = target.transform.position - pivot.transform.position;
        rb2dplayer.AddForce(shootVector*strength); 

        yield return null;
    }

    void resetTransform(){
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        target.transform.localPosition = new Vector3(0.5f,0f,0f);
        rb2dplayer.velocity = Vector2.zero;
        rb2dplayer.angularVelocity = 0f;
        sr_player.color = new Color(0.5f,1f,0.5f,1f);
    }

    //checking if player has stopped
    bool isPlayerStopped(){
        
        if(currentState == 3 && rb2dplayer.velocity.magnitude <= 0.05f)return true;
        else return false;
        
    }

}
