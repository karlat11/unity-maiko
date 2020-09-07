using UnityEngine;

public class foxController : MonoBehaviour
{
    private fieldOfView fov;
    private bool targetVisible = false;
    private Animator animator;

    private void Start()
    {
        fov = GetComponent<fieldOfView>();
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (fov.visibleTargets != null && fov.visibleTargets.Count != 0)
        {
            playerControlManager targetManager = fov.visibleTargets[0].GetComponent<playerControlManager>();
            if (!targetManager.sneaking && !targetVisible)
            {
                if (animator != null) animator.Play("Base Layer.wakeup");
                targetVisible = true;
                targetManager.enemyToFace = transform;
                playerControlManager.detected = true;
                transform.LookAt(fov.visibleTargets[0]);
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
}
