using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button homeBtn, nextBtn, backBtn, quitBtn, openPopUpBtn, pauseBtn, resumeBtn;
    public Button[] closePopUp;
    public GameObject popUpPanel, pauseScreen;
    public string levelId;

    [HideInInspector]
    public bool gamePaused = false;

    private void Start()
    {
        if (pauseScreen) pauseScreen.SetActive(false);

        if (homeBtn) homeBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(0);
        });

        if (nextBtn) nextBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        if (backBtn) backBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });

        if (quitBtn) quitBtn.onClick.AddListener(delegate () {
            Application.Quit();
        });

        if (openPopUpBtn && popUpPanel) openPopUpBtn.onClick.AddListener(delegate () {
            popUpPanel.SetActive(true);
        });

        if (pauseBtn && pauseScreen) pauseBtn.onClick.AddListener(delegate () {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
        });

        if (resumeBtn && pauseScreen) resumeBtn.onClick.AddListener(delegate () {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            gamePaused = false;
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

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelId);
    }
}
