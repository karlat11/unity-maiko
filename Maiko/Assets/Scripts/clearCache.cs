using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clearCache : MonoBehaviour
{
    public GameObject popUp;

    private Button btn;
    private Button closePopUpBtn, continueBtn;

    private void Awake()
    {
        btn = GetComponent<Button>();

        Button[] children = popUp.GetComponentsInChildren<Button>();
        foreach (Button child in children)
        {
            if (child.name == "close") closePopUpBtn = child;
            else if (child.name == "continue") continueBtn = child;
        }

        popUp.SetActive(false);
    }

    private void Start()
    {
        btn.onClick.AddListener(delegate () {
            popUp.SetActive(true);
        });

        closePopUpBtn.onClick.AddListener(delegate () {
            popUp.SetActive(false);
        });

        continueBtn.onClick.AddListener(delegate () {
            ClearSavedCache();
            popUp.SetActive(false);
        });
    }

    void ClearSavedCache()
    {
        PlayerPrefs.SetString("PlayerName", "");
        PlayerPrefs.SetString("PlayerSurname", "");
        PlayerPrefs.SetString("PlayerHair", "");
        PlayerPrefs.SetString("PlayerMakeup", "");
        PlayerPrefs.SetString("PlayerDress", "");
        PlayerPrefs.SetInt("lvlsUnlocked", 1);

        for (int j = 4; j < 12; j++)
        {
            PlayerPrefs.SetString(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(j)) + "_completed", "");
        }
    }
}
