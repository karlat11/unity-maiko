using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button homeBtn, nextBtn, backBtn, quitBtn, closePopUpBtn, openPopUpBtn;
    public GameObject popUpPanel;

    private void Start()
    {
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

        if (closePopUpBtn && popUpPanel) closePopUpBtn.onClick.AddListener(delegate () {
            popUpPanel.SetActive(false);
        });
    }

}
