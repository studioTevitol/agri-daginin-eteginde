using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private GameObject pivot,line,target;
    private SpriteRenderer sr_line,sr_target;
    private Rigidbody2D rb2dplayer;
    private int currentState = 0;
    [SerializeField] private float strength = 10f;

    void Start(){
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
    }

    void startRotationTarget(){
        sr_line.enabled = true;
        StartCoroutine(RotatePivot());
    }

    IEnumerator RotatePivot(){
        float lineRotateSpeed = 90f; // Set the rotation speed here
        Quaternion initialRotation = pivot.transform.rotation;

        while (true){
            int flag = 0;
            for (float angle = 0f; angle <= 90f; angle += lineRotateSpeed * Time.deltaTime){
                if (currentState > 1){
                    flag = 1;
                    break;
                }
                pivot.transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
                yield return null;
            }
            if (flag == 1)break;
            for (float angle = 90f; angle >= 0f; angle -= lineRotateSpeed * Time.deltaTime){
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
        Debug.Log("ChangeLineLength started");
        float lineChangeSpeed = 1f; // Set the line change speed here

        while (true){
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
    }

    void shootPlayer(){
        sr_line.enabled = false;
        sr_target.enabled = false;
        StartCoroutine(_shootPlayer());
    }

    IEnumerator _shootPlayer(){
        Vector3 shootVector = target.transform.position - pivot.transform.position;
        rb2dplayer.AddForce(shootVector*strength);
        float time = 0f;
        yield return null;
        while(true){
            time += Time.deltaTime;
            if (rb2dplayer.velocity.sqrMagnitude == 0f && time > 1f){
                Debug.Log("Player stopped");
                resetTransform();
                currentState = 0;
                break;
            }
            yield return null;
        }
    }

    void resetTransform(){
        pivot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        target.transform.localPosition = new Vector3(0.5f,0f,0f);
        rb2dplayer.velocity = Vector2.zero;
        rb2dplayer.angularVelocity = 0f;
    }
}




//90 derece boyunca git gel yaparak yön belirleyen 
//yukarı aşağı yaparak 