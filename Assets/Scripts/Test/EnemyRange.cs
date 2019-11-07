using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [SerializeField]
    private int max_hp = 100;
    [SerializeField]
    private float alert_time = 200000;

    private int cur_hp;
    public string enemy_name = "Ratty";

    private EnemyControllerRange controller;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;  //save for respawn

    private bool alive = true;
    public bool isAlive
    {
        get { return alive; }
        protected set { alive = value; }
    }

    void Start()
    {
        controller = GetComponent<EnemyControllerRange>();
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++) //store default setting
            wasEnabled[i] = disableOnDeath[i].enabled;

        SetDefault();
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive)
        {
            Debug.Log(transform.name + " is already DEAD");
            return;
        }

        cur_hp -= damage;
        Debug.Log("Enemy now has " + cur_hp + " health");
        Alert();
        if (cur_hp <= 0)
            EnemyKilled(transform.name);
    }

    public void Alert()
    {
        controller.radius = 15f;
        Invoke("Idle", 5);
    }

    public void Idle()
    {
        controller.radius = controller.default_radius;
    }

    public void EnemyKilled(string id)
    {
        isAlive = false;
        //Destroy(this.gameObject);
        Disable();

        Debug.Log(transform.name + " is DEAD");
        //GameManager.UnregisterEnemy(id);
    }

    #region disablecomponent
    public void Disable()
    {
        for (int i = 0; i < disableOnDeath.Length; i++) //deactivate default setting on death
            disableOnDeath[i].enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        EnemyControllerRange controller = GetComponent<EnemyControllerRange>();
        if (controller != null)
            controller.enabled = false;

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;

    }
    #endregion

    public void SetDefault()
    {
        isAlive = true;

        cur_hp = max_hp;

        for (int i = 0; i < disableOnDeath.Length; i++) //load default setting
            disableOnDeath[i].enabled = wasEnabled[i];
    }
}
