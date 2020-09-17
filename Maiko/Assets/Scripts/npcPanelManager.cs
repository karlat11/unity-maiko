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
    public TextMeshProUGUI scoreCounter;
    public string customBriefing, customEndTitle, customEndCopy;

    private static TextMeshProUGUI copy;
    private static TextMeshProUGUI npcName;
    private static Image npcImg;
    private TextMeshProUGUI endPanelTitle;
    private TextMeshProUGUI endPanelCopy;
    private Button endPanelCompleteBtn;
    private Button closePopUpBtn;
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
        }

        Image[] imgObjects = GetComponentsInChildren<Image>();
        foreach (Image obj in imgObjects)
        {
            if (obj.tag == "npcImage") npcImg = obj;
        }

        closePopUpBtn = GetComponentInChildren<Button>();

        wireUpPopUp();
    }

    private void Start()
    {
        scoreCounter.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString() + "/" + playerControlManager.interactibles.Length.ToString();
        
        closePopUpBtn.onClick.AddListener(delegate () {
            UIManager.nPanel.SetActive(false);
        });

        copy.text = "Kunoichi " + PlayerPrefs.GetString("PlayerSurname") + " " + PlayerPrefs.GetString("PlayerName") + customBriefing;
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
        UIManager.nPanel.SetActive(true);
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
        endPanelTitle.text = (customEndTitle != null && customEndTitle != "") ? customEndTitle : "Woo Hoo!";
        endPanelCopy.text = (customEndCopy != null && customEndCopy != "") ? customEndCopy : "You did it! You have collected all the secret documents needed and completed the mission undetected!";
        endPanelCompleteBtn.onClick.AddListener(delegate () {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_completed") == null ||
            PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_completed") == "")
            {
                PlayerPrefs.SetInt("lvlsUnlocked", PlayerPrefs.GetInt("lvlsUnlocked") + 1);
            }

            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_completed", "true");
            SceneManager.LoadScene("levelSelection_screen");
        });
        popUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator showPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        updatePopUp();
        UIManager.nPanel.SetActive(false);
    }
}
