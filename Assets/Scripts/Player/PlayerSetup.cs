using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSetup : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        player.Setup();
        //Debug.Log("Name of Player " + player.p_name);
        GameManager.RegisterPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
