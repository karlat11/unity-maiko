using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class playerControlManager : MonoBehaviour
{
    [HideInInspector]
    public bool detected = false;
    [HideInInspector]
    public bool sneaking = false;
    [HideInInspector]
    public Transform enemyToFace;
    public GameObject popUpPanel;
    public UIManager uiManager;

    private NavMeshAgent agent;
    private Animator animator;
    private AnimatorStateInfo state;
    private int initialPopUpDelay = 5;
    private IEnumerator coroutine;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        coroutine = delayInitialPopUp();
    }
    void Update()
    {
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0) && !detected && !uiManager.gamePaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (animator != null) animator.Play("Base Layer.walk");

                agent.SetDestination(hit.point);
                agent.speed = 1.75f;
                agent.angularSpeed = 700f;
                sneaking = false;
            }
        }
        
        else if (Input.GetMouseButtonDown(1) && !detected && !uiManager.gamePaused)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (animator != null) animator.Play("Base Layer.sneak");

                agent.SetDestination(hit.point);
                agent.speed = 1f;
                agent.angularSpeed = 700f;
                sneaking = true;
            }
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) &&
            !detected)
        {
            if (!state.IsName("idle")) animator.Play("Base Layer.idle");
        }

        if (detected)
        {
            if (!state.IsName("found"))
            {
                animator.Play("Base Layer.found");
                transform.LookAt(enemyToFace);
                agent.isStopped = true;
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator delayInitialPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        popUpPanel.SetActive(true);
        StopCoroutine(coroutine);
    }
}
