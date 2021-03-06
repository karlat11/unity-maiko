﻿using UnityEngine;

public class foxController : MonoBehaviour
{
    public string npcName;
    public string npcCopy;
    public Sprite npcImage;

    private fieldOfView fov;
    private bool targetVisible = false;
    private Animator animator;
    private GameObject player;
    private bool playerInSight = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        fov = GetComponent<fieldOfView>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckForPlayer();
    }

    private void LateUpdate()
    {
        if (playerInSight)
        {
            
            playerControlManager targetManager = player.GetComponent<playerControlManager>();
            if (!targetManager.sneaking && !targetVisible)
            {
                if (animator != null) animator.Play("Base Layer.wakeup");
                targetVisible = true;
                targetManager.enemyToFace = transform;
                playerControlManager.detected = true;
                transform.LookAt(player.transform);
                npcPanelManager.UpdateDetection(npcName, npcCopy, npcImage);
            }
        }

        else
        {
            if (targetVisible)
            {
                targetVisible = false;
                if (animator != null) animator.Play("Base Layer.sleep");
                if (playerControlManager.detected) playerControlManager.detected = false;
            }
        }
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
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
                Debug.DrawRay(transform.position, direction, Color.yellow);
            }
        }
        else Debug.DrawRay(transform.position, direction, Color.green);
    }
}
