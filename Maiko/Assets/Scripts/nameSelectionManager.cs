using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class nameSelectionManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI surnameText;
    public Button nameNext;
    public Button namePrev;
    public Button surnameNext;
    public Button surnamePrev;
    public GameObject panel;

    private int nameIdx;
    private int surnameIdx;
    private int initialPopUpDelay = 1;
    private string[] nameList;
    private string[] surnameList;
    private IEnumerator coroutine;

    void Start()
    {
        panel.SetActive(false);
        coroutine = delayInitialPopUp();

        nameIdx = surnameIdx = 0;
        nameList = new string[] {
            "Chiyome",
            "Chiyo",
            "Mineko",
            "Chisho",
            "Haruko",
            "Sanya",
            "Sasa"
        };
        surnameList = new string[] {
            "Iwasaki",
            "Mochizuki",
            "Takaoka",
            "Kiharu",
            "Yacco",
            "Kato",
            "Abe"
        };

        nameText.text = nameList[nameIdx];
        surnameText.text = surnameList[surnameIdx];

        PlayerPrefs.SetString("PlayerName", nameText.text);
        PlayerPrefs.SetString("PlayerSurname", surnameText.text);

        surnameNext.onClick.AddListener(delegate () {
            surnameIdx = nextIdx(surnameIdx, surnameText, surnameList);
            PlayerPrefs.SetString("PlayerSurname", surnameText.text);
        });

        surnamePrev.onClick.AddListener(delegate () {
            surnameIdx = prevIdx(surnameIdx, surnameText, surnameList);
            PlayerPrefs.SetString("PlayerSurname", surnameText.text);
        });

        nameNext.onClick.AddListener(delegate () {
            nameIdx = nextIdx(nameIdx, nameText, nameList);
            PlayerPrefs.SetString("PlayerName", nameText.text);
        });

        namePrev.onClick.AddListener(delegate () {
            nameIdx = prevIdx(nameIdx, nameText, nameList);
            PlayerPrefs.SetString("PlayerName", nameText.text);
        });

        StartCoroutine(coroutine);
    }

    private int prevIdx(int currentIdx, TextMeshProUGUI text, string[] list)
    {
        if (currentIdx > 0) currentIdx--;
        else currentIdx = list.Length - 1;
        text.text = list[currentIdx];

        return currentIdx;
    }

    private int nextIdx(int currentIdx, TextMeshProUGUI text, string[] list)
    {
        if (currentIdx < list.Length - 1) currentIdx++;
        else currentIdx = 0;
        text.text = list[currentIdx];
        return currentIdx;
    }

    IEnumerator delayInitialPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        panel.SetActive(true);
        StopCoroutine(coroutine);
    }
}
