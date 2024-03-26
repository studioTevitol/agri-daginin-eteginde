using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour{

    public int life = 3;
    public GameObject spawnPoint;
    [SerializeField] private float deathHeight = -50f;

    void Update(){
        if(transform.position.y < deathHeight){
            life--;
            transform.position = spawnPoint.transform.position;
        }
        if(life <= 0){
            StartCoroutine(KillGame());
        }
    }

    IEnumerator KillGame(){
        Debug.Log("Game Over");
        yield return new WaitForSeconds(0.5f);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
