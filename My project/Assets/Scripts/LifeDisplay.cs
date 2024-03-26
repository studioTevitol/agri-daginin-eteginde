using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LifeDisplay : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public PlayerLife playerLife;
    public Image youDied;

    void Start(){
        if(lifeText == null )Debug.Log("lifeText is null");
        if(playerLife == null)Debug.Log("playerLife is null");
        if(playerLife.life == 0)Debug.Log("playerLife.life is null");
        if(youDied == null)Debug.Log("youDied is null");
        youDied.enabled = false;
    }
    void Update(){
        if(playerLife.life <= 0){
            youDied.enabled = true;
        }
        lifeText.text=playerLife.life.ToString();
    }
}
