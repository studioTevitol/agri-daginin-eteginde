using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] private float rotation;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            rb2d.AddTorque(Mathf.Pow(2,rotation));    
        }
    }
}
