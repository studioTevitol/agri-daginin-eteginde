using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody2D rb;
    private int slimeState = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetState());
    }

    IEnumerator SetState()
    {
        if(rb.velocity.y>0.01f){
            float oldY = rb.velocity.y;
            yield return new WaitForSeconds(0.1f);
            if(rb.velocity.y>0.01f)slimeState = 1;
        }
        else if(rb.velocity.y<-0.01f){
            float oldY = rb.velocity.y;
            yield return new WaitForSeconds(0.05f);
            if(rb.velocity.y<-0.01f)slimeState = 2;
        }
        else slimeState = 0;

        animator.SetInteger("state", slimeState);
        yield return null;
    }
}
