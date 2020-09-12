using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class npcPanelManager : MonoBehaviour
{
    public static string detectedBy = "";
    public static bool scoreIncreased = false;
    public GameObject popUpPanel;

    private static TextMeshProUGUI copy;
    private static TextMeshProUGUI npcName;
    private static Image npcImg;
    private TextMeshProUGUI scoreCounter;
    private TextMeshProUGUI endPanelTitle;
    private TextMeshProUGUI endPanelCopy;
    private Button endPanelCompleteBtn;
    private npcPanelManager self;
    private int initialPopUpDelay = 1;

    private void Awake()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", 0);
        detectedBy = "";

        TextMeshProUGUI[] textObjects = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textObjects)
        {
            if (obj.tag == "Copy") copy = obj;
            else if (obj.tag == "npcName") npcName = obj;
            else if (obj.tag == "scoreCounter") scoreCounter = obj;
        }

        Image[] imgObjects = GetComponentsInChildren<Image>();
        foreach (Image obj in imgObjects)
        {
            if (obj.tag == "npcImage") npcImg = obj;
        }

        wireUpPopUp();
    }

    private void Start()
    {
        scoreCounter.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString() + "/" + playerControlManager.interactibles.Length.ToString();
    }

    private void Update()
    {
        if (scoreIncreased)
        {
            increaseScore();
        }
    }

    private void checkIfFinished()
    {
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score") == playerControlManager.interactibles.Length)
        {
            StartCoroutine(showPopUp());
        }
    }

    public static void UpdateDetection(string name, string dialogue, Sprite img)
    {
        npcName.text = name + ":";
        copy.text = dialogue;
        npcImg.sprite = img;
    }
    private void increaseScore()
    {
        scoreIncreased = false;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score") + 1);
        scoreCounter.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString() + "/" + playerControlManager.interactibles.Length.ToString();
        checkIfFinished();
    }

    private void wireUpPopUp()
    {
        for (var i = 0; i < popUpPanel.transform.childCount; i++)
        {
            Transform child = popUpPanel.transform.GetChild(i);
            if (child.name == "Title") endPanelTitle = child.GetComponent<TextMeshProUGUI>();
            else if (child.name == "Copy") endPanelCopy = child.GetComponent<TextMeshProUGUI>();
            else if (child.name == "continue") endPanelCompleteBtn = child.GetComponent<Button>();
        }
    }

    private void updatePopUp()
    {
        endPanelTitle.text = "Woo Hoo!";
        endPanelCopy.text = "You did it! You have collected all the secter documents needed and completed the mission undetected!";
        endPanelCompleteBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene("levelSelection_screen");
        });
        popUpPanel.SetActive(true);
    }

    IEnumerator showPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        updatePopUp();
        UIManager.nPanel.SetActive(false);
    }
}
