using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button homeBtn, nextBtn, backBtn, quitBtn, openPopUpBtn, pauseBtn, resumeBtn, referenceBtn, referenceCloseBtn;
    public Button[] closePopUp;
    public GameObject popUpPanel, pauseScreen, npcPanel, referencePanel;
    public string levelId;

    public static bool gamePaused = false;
    public static GameObject nPanel;

    private void Awake()
    {
        nPanel = npcPanel;
    }

    private void Start()
    {
        if (pauseScreen) pauseScreen.SetActive(false);
        if (nPanel) nPanel.SetActive(true);
        if (referencePanel) referencePanel.SetActive(false);

        if (homeBtn) homeBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(0);
        });

        if (nextBtn) nextBtn.onClick.AddListener(delegate () {
            if (SceneManager.GetActiveScene().name == "splash" &&
            (PlayerPrefs.GetString("PlayerName") != "" || PlayerPrefs.GetString("PlayerName") != null) &&
            (PlayerPrefs.GetString("PlayerSurname") != "" || PlayerPrefs.GetString("PlayerSurname") != null) &&
            (PlayerPrefs.GetString("PlayerHair") != "" || PlayerPrefs.GetString("PlayerHair") != null) &&
            (PlayerPrefs.GetString("PlayerMakeup") != "" || PlayerPrefs.GetString("PlayerMakeup") != null) &&
            (PlayerPrefs.GetString("PlayerDress") != "" || PlayerPrefs.GetString("PlayerDress") != null))
            {
                SceneManager.LoadScene(3);
            }
            
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        if (backBtn) backBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });

        if (quitBtn) quitBtn.onClick.AddListener(delegate () {
            Application.Quit();
        });

        if (openPopUpBtn && popUpPanel) openPopUpBtn.onClick.AddListener(delegate () {
            if (SceneManager.GetActiveScene().name == "splash" &&
            (PlayerPrefs.GetString("PlayerName") != "" || PlayerPrefs.GetString("PlayerName") != null) &&
            (PlayerPrefs.GetString("PlayerSurname") != "" || PlayerPrefs.GetString("PlayerSurname") != null) &&
            (PlayerPrefs.GetString("PlayerHair") != "" || PlayerPrefs.GetString("PlayerHair") != null) &&
            (PlayerPrefs.GetString("PlayerMakeup") != "" || PlayerPrefs.GetString("PlayerMakeup") != null) &&
            (PlayerPrefs.GetString("PlayerDress") != "" || PlayerPrefs.GetString("PlayerDress") != null))
            {
                nextBtn.onClick.Invoke();
            }
            else popUpPanel.SetActive(true);
        });

        if (pauseBtn && pauseScreen) pauseBtn.onClick.AddListener(delegate () {
            nPanel.SetActive(false);
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;

        });

        if (resumeBtn && pauseScreen) resumeBtn.onClick.AddListener(delegate () {
            nPanel.SetActive(true);
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
        });

        if (referenceBtn && referencePanel) referenceBtn.onClick.AddListener(delegate () {
            referencePanel.SetActive(true);
        });

        if (referenceCloseBtn && referencePanel) referenceCloseBtn.onClick.AddListener(delegate () {
            referencePanel.SetActive(false);
        });

        if ((closePopUp != null && closePopUp.Length != 0) && popUpPanel)
        {
            foreach (Button btn in closePopUp)
            {
                btn.onClick.AddListener(delegate () {
                    popUpPanel.SetActive(false);
                });
            }
        }
    }

    public void LoadLevel(string lvlName = default(string))
    {
        string lvl = lvlName != null && lvlName != "" ? lvlName : levelId;
        SceneManager.LoadScene(lvl);
    }
}
