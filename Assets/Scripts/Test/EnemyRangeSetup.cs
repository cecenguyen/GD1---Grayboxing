using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyRange))]
public class EnemyRangeSetup : MonoBehaviour
{
    private EnemyRange enemy;
    string enemy_id;
    public static int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyRange>();
        enemy.Setup();
        //Debug.Log("Name of Enemy " + enemy.enemy_name);
        enemy_id = enemy.enemy_name + "Range" + id.ToString();
        //Giving each enemy a unique id
        GameManager.RegisterRangeEnemy(enemy_id, enemy);

        id++;
        //Debug.Log("Id: " + id);
    }
}
