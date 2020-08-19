using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button homeBtn, nextBtn, backBtn, quitBtn;

    private void Start()
    {
        homeBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(0);
        });

        nextBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        backBtn.onClick.AddListener(delegate () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        });

        quitBtn.onClick.AddListener(delegate () {
            Application.Quit();
        });
    }

}
