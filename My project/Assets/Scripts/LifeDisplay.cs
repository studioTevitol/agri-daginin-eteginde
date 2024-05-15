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
    [SerializeField] private List<Color> colors;

    void Start(){
        //Editorden arraye color ekleyince "çekemiyordu" rengi direkt buradan atadım
        Color thirdLifeColor = new Color(210,0,0);  colors.Add(thirdLifeColor);
        Color secondLifeColor = new Color(255,220,0); colors.Add(secondLifeColor);
        Color firstLifeColor = new Color(0,210,0); colors.Add(firstLifeColor);


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
        lifeText.color = colors[playerLife.life-1];
    }

}
