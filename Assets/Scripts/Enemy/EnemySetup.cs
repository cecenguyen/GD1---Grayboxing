using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemySetup : MonoBehaviour
{
    private Enemy enemy;
    string enemy_id;
    public static int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemy.Setup();
        //Debug.Log("Name of Enemy " + enemy.enemy_name);
        enemy_id = enemy.enemy_name + id.ToString();
        //Giving each enemy a unique id
        GameManager.RegisterEnemy(enemy_id, enemy);

        id++;
        //Debug.Log("Id: " + id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
