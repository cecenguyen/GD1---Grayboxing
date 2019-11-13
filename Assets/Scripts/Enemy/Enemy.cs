using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int max_hp = 100;

    private float alert_radius = 15f;
    private float alert_time = 5f;
    private int cur_hp;
    public string enemy_name = "Ratty";

    private EnemyController controller;
    private EnemyGraphic graphic;
    private Animator animator;

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
        controller = GetComponent<EnemyController>();
        graphic = GetComponent<EnemyGraphic>();
        animator = GetComponentInChildren<Animator>();
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

        graphic.blood.Play();

        cur_hp -= damage;
        Debug.Log("Enemy now has " + cur_hp + " health");

        Alert();

        if (cur_hp <= 0)
            EnemyKilled(transform.name);
    }

    public void Alert()
    {
        controller.detected = true;
    }

    public void EnemyKilled(string id)
    {
        isAlive = false;
        graphic.explode.Play();
        animator.SetTrigger("Die");
        Disable();

        Debug.Log(transform.name + " is DEAD");
        GameManager.UnregisterEnemy(id);
        Invoke("Destroy", 3);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
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

        EnemyController controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.enabled = false;

        EnemyRangeController range_controller = GetComponent<EnemyRangeController>();
        if (range_controller != null)
            range_controller.enabled = false;

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
