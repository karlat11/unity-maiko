using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    private int[] idList;
    private int currentId;
    private GameObject[] hairList, makeupList, dressList, currentList;
    private GameObject[][] lists;
    private Button[] btnList;

    void Start()
    {

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

        hairBtn.onClick.AddListener(delegate () {
            currentList = lists[0];

            foreach (Button btn in btnList) btn.interactable = true;
            hairBtn.interactable = false;
        });

        makeupBtn.onClick.AddListener(delegate () {
            currentList = lists[1];

            foreach (Button btn in btnList) btn.interactable = true;
            makeupBtn.interactable = false;
        });

        dressBtn.onClick.AddListener(delegate () {
            currentList = lists[2];

            foreach (Button btn in btnList) btn.interactable = true;
            dressBtn.interactable = false;
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
    }
}
