using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

public class characterGenerationManager : MonoBehaviour
{
    public Button nextBtn;
    public Button prevBtn;
    public Button hairBtn;
    public Button makeupBtn;
    public Button dressBtn;
    public GameObject hairMaiko, hairGeisha, hairOiran;
    public GameObject makeupMaiko, makeupGeisha, makeupOiran;
    public GameObject dressMaiko, dressGeisha, dressOiran;
    public GameObject model;
    public GameObject popUpPanel;
    public textCreator TextCreator;
    public TextMeshProUGUI copy;

    private int[] idList;
    private int currentId;
    private int initialPopUpDelay = 1;
    private GameObject[] hairList, makeupList, dressList, currentList;
    private GameObject[][] lists;
    private Button[] btnList;
    private bool pressedModel;
    private bool hairPopUpDisplayed, makeupPopUpDisplayed, kimonoPopUpDisplayed = false;
    private Vector3 hitPos;
    private float dist = 0;
    private IEnumerator coroutine;

    void Start()
    {
        popUpPanel.SetActive(false);
        coroutine = delayInitialPopUp();

        hairList = new GameObject[] {
            hairMaiko,
            hairGeisha,
            hairOiran
        };

        makeupList = new GameObject[] {
            makeupMaiko,
            makeupGeisha,
            makeupOiran
        };

        dressList = new GameObject[] {
            dressMaiko,
            dressGeisha,
            dressOiran
        };

        idList = new int[]
        {
            0, //hair
            0, //make up
            0 //dress
        };

        lists = new GameObject[][]
        {
            hairList, //hair
            makeupList, //make up
            dressList //dress
        };

        btnList = new Button[] { hairBtn, makeupBtn, dressBtn };

        currentList = lists[0];
        currentId = idList[0];

        pressedModel = false;

        hairBtn.onClick.AddListener(delegate () {
            currentList = lists[0];

            foreach (Button btn in btnList) btn.interactable = true;
            hairBtn.interactable = false;
        });

        makeupBtn.onClick.AddListener(delegate () {
            currentList = lists[1];

            foreach (Button btn in btnList) btn.interactable = true;
            makeupBtn.interactable = false;

            if (!makeupPopUpDisplayed) makeupPopUpDisplayed = DisplayPopUp(makeupPopUpDisplayed, TextCreator.sentences[1]);
        });

        dressBtn.onClick.AddListener(delegate () {
            currentList = lists[2];

            foreach (Button btn in btnList) btn.interactable = true;
            dressBtn.interactable = false;

            if (!kimonoPopUpDisplayed) kimonoPopUpDisplayed = DisplayPopUp(kimonoPopUpDisplayed, TextCreator.sentences[2]);
        });

        nextBtn.onClick.AddListener(delegate () {
            if (currentId < currentList.Length - 1) currentId++;
            else currentId = 0;

            var idx = Array.IndexOf(lists, currentList);
            idList[idx] = currentId;

            foreach (GameObject child in currentList)
            {
                child.SetActive(false);
            }

            currentList[currentId].SetActive(true);
        });

        prevBtn.onClick.AddListener(delegate () {
            if (currentId > 0) currentId--;
            else currentId = currentList.Length - 1;

            var idx = Array.IndexOf(lists, currentList);
            idList[idx] = currentId;

            foreach (GameObject child in currentList)
            {
                child.SetActive(false);
            }

            currentList[currentId].SetActive(true);
        });

        hairBtn.onClick.Invoke();
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.collider.tag == "Model")
                {
                    pressedModel = true;
                    hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) pressedModel = false;

        if (pressedModel)
        {
            if (dist != (hitPos.x - Input.mousePosition.x))
            {
                dist = hitPos.x - Input.mousePosition.x;
                model.transform.Rotate(0, model.transform.rotation.y + dist/2, 0);
                hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            }
        }
    }

    IEnumerator delayInitialPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        popUpPanel.SetActive(true);
        hairPopUpDisplayed = true;
        StopCoroutine(coroutine);
    }

    bool DisplayPopUp(bool checkBool, string text)
    {
        copy.text = text;
        popUpPanel.SetActive(true);
        return checkBool = true;
    }
}
