using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class playerControlManager : MonoBehaviour
{
    public static bool detected = false;
    public static GameObject[] interactibles;

    [HideInInspector]
    public bool sneaking = false;
    [HideInInspector]
    public Transform enemyToFace;
    public GameObject popUpPanel;
    public GameObject interactibleCont;
    public gameplayManager gameManager;
    public GameObject clickMarker;
    public int secondPopUpCollecibleIdx;

    [SerializeField] private string secondPopUpName, secondPopUpCopy;
    [SerializeField] private Sprite secondPopUpImg;
    private GameObject targetInteractible = null;
    private NavMeshAgent agent;
    private Animator animator;
    private AnimatorStateInfo state;
    private int initialPopUpDelay = 5;
    private IEnumerator coroutine;
    private bool isInteractible = false;
    private bool startedAnimTransition = false;
    //public LayerMask mask;

    EventSystem _event;

    private void Awake()
    {
        UIManager.gamePaused = false;
        interactibles = new GameObject[interactibleCont.transform.childCount];
        for (int i = 0; i < interactibles.Length; i++) interactibles[i] = interactibleCont.transform.GetChild(i).gameObject;
        clickMarker.SetActive(false);

        if (gameManager.isOnboardingLevel)
        {
            for (int j = 0; j < interactibles.Length; j++)
            {
                if (j == 0) interactibles[j].SetActive(true);
                else interactibles[j].SetActive(false);
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        coroutine = delayInitialPopUp();
        detected = false;

        _event = EventSystem.current;
        //mask = GetComponent<LayerMask>();
    }
    void Update()
    {
        //Debug.Log("mask " + mask);
        /*gameObject.layer = LayerMask.NameToLayer("Player");*/
        state = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetMouseButtonDown(0) && 
            !detected && !UIManager.gamePaused && 
            !_event.IsPointerOverGameObject())
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
                isInteractible = checkForInteracible(hit.collider.gameObject);
                startedAnimTransition = false;

                if (isInteractible)
                {
                    targetInteractible = hit.collider.gameObject;
                    agent.stoppingDistance = 1.5f;
                }
                else
                {
                    agent.stoppingDistance = 0.1f;
                }

                clickMarker.SetActive(true);
                clickMarker.transform.position = new Vector3(hit.point.x, clickMarker.transform.position.y, hit.point.z);
            }
        }
        
        else if (Input.GetMouseButtonDown(1) && 
            !detected && !UIManager.gamePaused &&
            !_event.IsPointerOverGameObject())
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
                isInteractible = checkForInteracible(hit.collider.gameObject);
                startedAnimTransition = false;

                if (isInteractible)
                {
                    targetInteractible = hit.collider.gameObject;
                    agent.stoppingDistance = 1.5f;
                } else
                {
                    agent.stoppingDistance = 0.1f;
                }

                clickMarker.SetActive(true);
                clickMarker.transform.position = new Vector3(hit.point.x, clickMarker.transform.position.y, hit.point.z);
            }
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) &&
            !detected)
        {
            clickMarker.SetActive(false);

            switch (isInteractible)
            {
                case true:
                    collectInteractible();
                    startedAnimTransition = true;
                    break;
                case false:
                    if (!state.IsName("collect") && !state.IsName("idle")) animator.Play("Base Layer.idle");
                    break;
            }
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

    bool checkForInteracible(GameObject obj)
    {
        foreach(GameObject child in interactibles) 
            if (child == obj) return true;

        return false;
    }

    void collectInteractible()
    {
        if (!gameManager.isOnboardingLevel)
        {
            if (!startedAnimTransition && !state.IsName("collect")) animator.Play("Base Layer.collect");

            if (startedAnimTransition && !state.IsName("collect"))
            {
                foreach (GameObject child in interactibles)
                {
                    if (child == targetInteractible)
                    {
                        child.SetActive(false);
                        targetInteractible = null;
                        isInteractible = false;
                        npcPanelManager.scoreIncreased = true;
                    }
                }
            }
        }

        else
        {
            foreach (GameObject child in interactibles)
            {
                if (child == targetInteractible)
                {
                    child.SetActive(false);
                    targetInteractible = null;
                    isInteractible = false;
                    npcPanelManager.scoreIncreased = true;
                    if (child.transform.GetSiblingIndex() < interactibles.Length-1) interactibles[child.transform.GetSiblingIndex() + 1].SetActive(true);
                    if (child.transform.GetSiblingIndex() == secondPopUpCollecibleIdx) npcPanelManager.UpdateDetection(secondPopUpName, secondPopUpCopy, secondPopUpImg);
                }
            }
        }

    }

    IEnumerator delayInitialPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        UIManager.nPanel.SetActive(false);
        popUpPanel.SetActive(true);
        StopCoroutine(coroutine);
    }
}
