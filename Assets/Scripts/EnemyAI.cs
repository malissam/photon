using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
 {
    public Transform target;
    public string enemyTag = "Barricade";
    float f_RotSpeed = 3.0f;
    float f_MoveSpeed = 3.0f;
    public float attackDist = 2f;
    public float attackTimer = 3f;
    public float cooldown;
    public int health = 150;
    public GameObject theCrate;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        cooldown = 2.0f;
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        if (Random.value > 0.9)
        {
            GameObject crateSpawn = (GameObject)Instantiate(theCrate, transform.position, transform.rotation);
        }
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;






        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            // target = nearestEnemy.transform;
            target = enemies[Random.Range(0, enemies.Length)].transform;
        }
        else
        {
            // target = null;
        }


    }

    void Update()
    {


        if (target == null)
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), f_RotSpeed * Time.deltaTime); ;
        //move to player


        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
            attackTimer = 0;

        if (attackTimer == 0)
        {

            Attack();
            attackTimer = cooldown;
        }

    }


    private void Attack()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);


        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);


        if (distance < attackDist)
        {
            if (direction > 0)
            {

                Debug.Log("Atacked");
            }
        }
    }

}