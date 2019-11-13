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
    public GameObject hit_effect;

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

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hiting " + col.gameObject.name);

        ContactPoint contact = col.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if(hit_effect != null)
        {
            var hit = Instantiate(hit_effect, pos, rot);
            var ps_hit = hit.GetComponent<ParticleSystem>();
            if (ps_hit != null)
                Destroy(hit, ps_hit.main.duration);
            else
            {
                var ps_child = hit.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hit, ps_child.main.duration);
            }
        }

        if (col.gameObject.tag == "Enemy")
        {
            EnemyShot(col.gameObject.name, damage);
        }

        if (col.gameObject.tag != "Gun" && col.gameObject.tag != "Projectile")
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
