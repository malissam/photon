using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    NavMeshAgent nav;
    Transform player;
    Animator controller;
    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponentInParent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
        controller.SetFloat("speed", Mathf.Abs(nav.velocity.x) + Mathf.Abs(nav.velocity.z));
    }
}
