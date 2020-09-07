using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public GameObject waypointPatrol;

    private Transform waypointTarget;
    private Transform[] wPoints;
    private int waypointIdx = 0;
    private fieldOfView fov;
    private bool targetVisible = false;
    private Animator animator;
    private NavMeshAgent agent;
    private void Awake()
    {
        wPoints = new Transform[waypointPatrol.transform.childCount];
        for (int i = 0; i < wPoints.Length; i++) wPoints[i] = waypointPatrol.transform.GetChild(i);
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
                    if (!targetManager.detected)
                    {
                        targetManager.enemyToFace = transform;
                        targetManager.detected = true;
                        transform.LookAt(target);
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

                foreach (Transform target in fov.visibleTargets)
                {
                    playerControlManager targetManager = target.GetComponent<playerControlManager>();
                    if (targetManager.detected) targetManager.detected = false;
                }
            }
        }
    }

}
