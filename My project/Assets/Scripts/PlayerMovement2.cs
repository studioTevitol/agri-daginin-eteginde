using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour{
    private GameObject pivot,line,target;
    private SpriteRenderer sr_player,sr_line,sr_target;
    private Rigidbody2D rb2dplayer;
    private int currentState = 0;// should be fully private!
    [SerializeField] private float lineRotateSpeed = 250f;
    [SerializeField] private KeyCode jumpKey=KeyCode.Space;
    [SerializeField] private KeyCode debugKey=KeyCode.V;
    [SerializeField] private float strength=500f;
    private bool isPlayerStopped=false;
    private float epsilon=0.1f;

    void Start(){
        //commenting bcs value from editor would be ignored
        //strength = 500f;

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
        StartCoroutine(checkPlayerStopped());
        resetTransform();
    }

    void Update(){

        //its not a bug its feature you can jump mid air 
        //but because the target will start at lowest point even tough a player spams jump he/she cant launch itself
        if (Input.GetKeyDown(jumpKey)){
            Debug.Log("Space pressed");
            if (currentState != 0 || isPlayerStopped){
                currentState=(currentState+1)%3;//optimize: one module instead of three :)
                if (currentState == 1)startRotationTarget();
                if (currentState == 2)startForceTarget();
                if (currentState == 0)shootPlayer();
            }
        }

        //to test jumping
        if(Input.GetKeyDown(debugKey)){
            Debug.Log("debugKey pressed");
            currentState=0;
            resetTransform();
        }
        //Debug.Log("currentstate: "+currentState % 3);
    }

    void startRotationTarget(){
        sr_line.enabled = true;
        StartCoroutine(RotatePivot());
    }

    IEnumerator RotatePivot(){
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);// reset pivot rotation
        Quaternion initialRotation = pivot.transform.rotation;
        
        while (true){
            int flag = 0;
            for (float angle = 0f; angle <= 180f; angle += lineRotateSpeed * Time.deltaTime){
                if (currentState % 3 > 1){
                    flag = 1;
                    break;
                }
                pivot.transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);

                yield return null;
            }
            if (flag == 1)break;
            for (float angle = 180f; angle >= 0f; angle -= lineRotateSpeed * Time.deltaTime){
                if (currentState % 3 > 1){
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
        //fuck it lets make currentstate divided by three so it can loop


            while (true){
                target.transform.localPosition = new Vector3(-0.5f,0f,0f);

                int flag = 0;
                for(float x=-0.5f;x<=0.5f && flag==0;x+=lineChangeSpeed*Time.deltaTime){
                    if (currentState % 3 != 2)flag = 1;
                    target.transform.localPosition = new Vector3(x,0f,0f);
                    yield return null;
                }
                for(float x=0.5f;x>=-0.5f && flag==0;x-=lineChangeSpeed*Time.deltaTime){
                    target.transform.localPosition = new Vector3(x,0f,0f);
                    yield return null;
                    if (currentState % 3 != 2)flag = 1;
                }
                if (flag == 1)break;
            }
        

        Debug.Log("ChangeLineLength finished");
        

        yield return null;
    }

    void shootPlayer(){
        sr_line.enabled = false;
        sr_target.enabled = false;
        isPlayerStopped = false;
        StartCoroutine(_shootPlayer());
        StartCoroutine(checkPlayerStopped());
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
    IEnumerator checkPlayerStopped(){
        yield return new WaitForSeconds(.5f);
        while(!isPlayerStopped){
            if(Mathf.Abs(rb2dplayer.velocity.sqrMagnitude) < epsilon){
                float oldSqrMagnitude = Mathf.Abs(rb2dplayer.velocity.sqrMagnitude);
                yield return new WaitForSeconds(.05f);
                if(Mathf.Abs(rb2dplayer.velocity.sqrMagnitude) == oldSqrMagnitude)isPlayerStopped = true;
            }
            yield return null;
        }
        Debug.Log("Player stopped");
        resetTransform();
        yield return null;
    }

}
