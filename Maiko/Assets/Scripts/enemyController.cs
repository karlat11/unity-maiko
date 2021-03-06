﻿using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public GameObject waypointPatrol;
    public string npcName;
    public string npcCopy;
    public Sprite npcImage;

    private Transform waypointTarget;
    private Transform[] wPoints;
    private int waypointIdx = 0;
    private fieldOfView fov;
    private bool targetVisible = false;
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;
    private bool playerInSight = false;

    private void Awake()
    {
        wPoints = new Transform[waypointPatrol.transform.childCount];
        for (int i = 0; i < wPoints.Length; i++) wPoints[i] = waypointPatrol.transform.GetChild(i);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        fov = GetComponent<fieldOfView>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        waypointTarget = wPoints[waypointIdx];

        agent.SetDestination(waypointTarget.position);
    }

    private void Update()
    {
        if (!playerControlManager.detected)
        {
            if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
            {
                animator.Play("Base Layer.idle");

                if (waypointIdx < wPoints.Length - 1) waypointIdx++;
                else waypointIdx = 0;

                waypointTarget = wPoints[waypointIdx];
                agent.SetDestination(waypointTarget.position);
            }

            else
            {
                animator.Play("Base Layer.walk");
            }
        }

        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        RaycastHit hit;

        if (angle < fov.viewAngle * 0.5f)
        {
            Physics.Raycast(transform.position, direction, out hit);

            if (hit.collider && hit.collider.gameObject == player && direction.magnitude < fov.viewRad)
            {
                Debug.DrawRay(transform.position, direction, Color.red);
                if (!playerInSight) fov.visibleTargets.Add(player.transform);
                playerInSight = true;
            }
            else
            {
                Debug.DrawRay(transform.position, direction, Color.yellow);
                fov.visibleTargets.Clear();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.green);
        }
    }

    private void LateUpdate()
    {
        if (fov.visibleTargets != null && fov.visibleTargets.Count != 0)
        {
            if (!targetVisible)
            {
                if (animator != null) animator.Play("Base Layer.found");
                targetVisible = true;

                foreach (Transform target in fov.visibleTargets)
                {
                    playerControlManager targetManager = target.GetComponent<playerControlManager>();
                    if (!playerControlManager.detected)
                    {
                        targetManager.enemyToFace = transform;
                        playerControlManager.detected = true;
                        transform.LookAt(target);
                        agent.isStopped = true;
                        npcPanelManager.UpdateDetection(npcName, npcCopy, npcImage);
                    }
                }
            }
        }

        else
        {
            if (targetVisible)
            {
                targetVisible = false;
                if (animator != null) animator.Play("Base Layer.coolDown");

                if (playerControlManager.detected) playerControlManager.detected = false;
            }
        }

        if (playerControlManager.detected && !agent.isStopped)
        {
            animator.Play("Base Layer.idle");
            agent.isStopped = true;
        }
    }
}
