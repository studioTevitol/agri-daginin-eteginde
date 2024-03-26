using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    private GameObject pivot,line,target;
    [SerializeField] private GameObject mainCamera;
    private SpriteRenderer sr_player,sr_line,sr_target;
    private Rigidbody2D rb2dplayer;
    private int currentState = 0;
    [SerializeField] private float strength = 10f;
    public CameraScript cameraScript;
    [SerializeField] private float waitTime = 1f;

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
    }

    void startRotationTarget(){
        sr_line.enabled = true;
        StartCoroutine(RotatePivot());
    }

    IEnumerator RotatePivot(){
        float lineRotateSpeed = 180f; // Set the rotation speed here
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
        sr_player.color = Color.white;
        Vector3 shootVector = target.transform.position - pivot.transform.position;
        rb2dplayer.AddForce(shootVector*strength);
        float time = 0f;
        yield return null;
        while(true){
            Debug.Log("mainCamera position: "+mainCamera.transform.position);
            Debug.Log("player position: "+(transform.position+new Vector3(cameraScript.cameraxOffset,cameraScript.camerayOffset,mainCamera.transform.position.z)));
            if(transform.position+new Vector3(cameraScript.cameraxOffset,cameraScript.camerayOffset,mainCamera.transform.position.z) == mainCamera.transform.position){
                time += Time.deltaTime;
            }
            else{
                time = 0f;
            }
            if (time > waitTime){
                Debug.Log("mainCamera stopped and passed");
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
        sr_player.color = new Color(0.5f,1f,0.5f,1f);
    }
}
