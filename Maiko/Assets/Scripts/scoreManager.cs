using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public GameObject popUpPanel;
    public string customEndTitle, customEndCopy;

    private TextMeshProUGUI scoreCounter;
    private int initialPopUpDelay = 1;
    private TextMeshProUGUI endPanelTitle;
    private TextMeshProUGUI endPanelCopy;
    private Button endPanelCompleteBtn;

    private void Awake()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", 0);
        scoreCounter = GetComponent<TextMeshProUGUI>();

        wireUpPopUp();
    }

    private void Start()
    {
        scoreCounter.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString() + "/" + playerControlManager.interactibles.Length.ToString();
    }

    private void Update()
    {
        if (npcPanelManager.scoreIncreased) increaseScore();
    }

    private void increaseScore()
    {
        npcPanelManager.scoreIncreased = false;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score") + 1);
        scoreCounter.text = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score").ToString() + "/" + playerControlManager.interactibles.Length.ToString();
        checkIfFinished();
    }

    private void checkIfFinished()
    {
        if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_score") == playerControlManager.interactibles.Length)
        {
            StartCoroutine(showPopUp());
        }
    }

    IEnumerator showPopUp()
    {
        yield return new WaitForSeconds(initialPopUpDelay);
        updatePopUp();
        UIManager.nPanel.SetActive(false);
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
        UIManager.nPanel.SetActive(false);
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
}
