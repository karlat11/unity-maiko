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
            }
        }

        else
        {
            if (targetVisible)
            {
                targetVisible = false;
                if (animator != null) animator.Play("Base Layer.coolDown");
            }
        }
    }

}
