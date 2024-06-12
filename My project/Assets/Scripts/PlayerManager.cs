using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerLeft, playerRight;
    [SerializeField] private Transform leftSpawnPoint, rightSpawnPoint;
    void Start()
    {
        playerLeft = GameObject.FindGameObjectWithTag("PlayerLeft");
        playerRight = GameObject.FindGameObjectWithTag("PlayerRight");
        leftSpawnPoint = GameObject.FindWithTag("LeftSpawnPoint").GetComponent<Transform>();
        rightSpawnPoint = GameObject.FindWithTag("RightSpawnPoint").GetComponent<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerLeft.name);
        Debug.Log(playerRight.name);

        if (playerLeft.transform.position.y < -20)
        {
            RespawnAtSpawnpoint(playerLeft, leftSpawnPoint);
            RespawnAtSpawnpoint(playerRight, rightSpawnPoint);
        }        
        
    }

    private void RespawnAtSpawnpoint(GameObject player, Transform spawnPoint)
    {
        player.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
        player.transform.position = spawnPoint.position;
        player.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

    }
    
}
