using UnityEngine;

public class enemyController : MonoBehaviour
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
            if (!targetVisible)
            {
                if (animator != null) animator.Play("Base Layer.found");
                targetVisible = true;

                foreach (Transform target in fov.visibleTargets)
                {
                    playerControlManager targetManager = target.GetComponent<playerControlManager>();
                    if (!targetManager.detected)
                    {
                        targetManager.detected = true;
                        targetManager.enemyToFace = transform;
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
