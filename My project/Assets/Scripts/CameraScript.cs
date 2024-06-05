using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float maxSpeed=1f;
    [SerializeField] private GameObject player;
    public float cameraxOffset=0f;
    public float camerayOffset=0f;
    // Start is called before the first frame update
    void Start(){
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update(){
        float dx = player.transform.position.x - transform.position.x + cameraxOffset;
        float dy = player.transform.position.y - transform.position.y + camerayOffset;
        if(Mathf.Sqrt(dx*dx+dy*dy)<=maxSpeed*Time.deltaTime){
            transform.position = new Vector3(player.transform.position.x + cameraxOffset,player.transform.position.y + camerayOffset,transform.position.z);
        }
        else{
            float angle = Mathf.Atan2(dy,dx);
            transform.position = new Vector3(transform.position.x+maxSpeed*Time.deltaTime*Mathf.Cos(angle),transform.position.y+maxSpeed*Time.deltaTime*Mathf.Sin(angle),transform.position.z);
        }
    }
}
