using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossAi : MonoBehaviour
{
    [SerializeField] private Animator bossAnim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private List<Transform> destinationPoints = new List<Transform>();
    private Transform newDestination;
    private int rnd;

    private void Start()
    {
        rnd = Random.Range(0, destinationPoints.Count);
        newDestination = destinationPoints[rnd];
        agent.destination = newDestination.position;
    }

    private void Update()
    {
        if ((transform.position - newDestination.position).magnitude <= 1.5f)
        {
            agent.enabled = false;
            StartCoroutine(BossWaiting(4f));
        }
    }

    private IEnumerator BossWaiting(float value)
    {
        transform.DOLookAt(newDestination.position, 0.5f, AxisConstraint.Y);
        //bossAnim.SetBool("Walk",false);
        int rndAnim = Random.Range(0, 2);
        if (rndAnim == 0)
        {
            bossAnim.SetBool("Angry1", true);
        }
        else
        {
            bossAnim.SetBool("Angry2", true);
        }

        for (int i = 0; i < 10; i++)
        {
            rnd = Random.Range(0, destinationPoints.Count);
            if (newDestination != destinationPoints[rnd])
            {
                newDestination = destinationPoints[rnd];
                break;
            }
        }

        yield return new WaitForSeconds(value);
        bossAnim.SetBool("Angry1", false);
        bossAnim.SetBool("Angry2", false);
        agent.enabled = true;
        agent.destination = newDestination.position;
        //bossAnim.SetBool("Walk",true);
    }
}