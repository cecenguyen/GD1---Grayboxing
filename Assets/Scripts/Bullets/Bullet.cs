using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 60f;

    private Rigidbody rb;
    private Vector3 force = Vector3.zero;
    private Vector3 direction = Vector3.zero;

    private float time = 2f;
    private int damage;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Launching();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    public void SetDamage(int d)
    {
        damage = d;
    }

    void Launching()
    {
        force = direction * speed;
        rb.AddForce(force * Time.fixedDeltaTime, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(this.gameObject, time));
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hiting " + col.name);
        
        if (col.tag == "Enemy")
        {
            EnemyShot(col.name, damage);
        } 

        if (col.tag != "Gun" && col.tag != "Projectile")
            Destroy(this.gameObject);
    }

    void EnemyShot(string name, int damage)
    {
        Debug.Log(name + " has been shot");

        Enemy enemy = GameManager.GetEnemy(name);
        if (enemy.isAlive == true)
            enemy.TakeDamage(damage);
    }
}
