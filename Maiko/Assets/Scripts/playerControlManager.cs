using UnityEngine;
using UnityEngine.AI;

public class playerControlManager : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (animator != null) animator.Play("Base Layer.walk");

                agent.SetDestination(hit.point);
                agent.speed = 1.75f;
                agent.angularSpeed = 700f;
            }
        }
        
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (animator != null) animator.Play("Base Layer.sneak");

                agent.SetDestination(hit.point);
                agent.speed = 1f;
                agent.angularSpeed = 700f;
            }
        }
    }
}
