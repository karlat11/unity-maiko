using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelSelectionManager : MonoBehaviour
{
    public GameObject btns;
    public GameObject completedText;
    public TextMeshProUGUI briefing;
    public Image levelImg;
    
    private int levelsUnlocked;
    private Button[] levelBtns;
    private UIManager uiManag;

    private void Start()
    {
        Time.timeScale = 1f;
        uiManag = GetComponent<UIManager>();
        levelBtns = btns.GetComponentsInChildren<Button>();
        if (PlayerPrefs.GetInt("lvlsUnlocked") == 0)
        {
            PlayerPrefs.SetInt("lvlsUnlocked", 1);
            levelsUnlocked = 1;
        }
        else levelsUnlocked = PlayerPrefs.GetInt("lvlsUnlocked");

        completedText.SetActive(false);

        if (levelBtns != null && levelBtns.Length != 0)
        {
            int i = 0;
            foreach (Button btn in levelBtns)
            {
                bool unlocked = i < levelsUnlocked;

                Image[] img = btn.GetComponentsInChildren<Image>();
                foreach (Image child in img)
                {
                    if (child.tag == "Locked")
                    {
                        Image childImg = child.GetComponentInChildren<Image>();
                        updateLvlBtn(unlocked, btn, childImg);
                    }
                }

                briefingCreator bCreator = btn.GetComponentInChildren<briefingCreator>();

                btn.onClick.AddListener(delegate () {
                    changeSelectedLvl(btn, bCreator.sentences, bCreator.levelImage, bCreator.levelName);
                    uiManag.levelId = bCreator.levelName;
                });

                //btn.onClick.Invoke();
                if (i == 0) btn.onClick.Invoke();

                i++;
            }
        }
    }

    void updateLvlBtn(bool unlocked, Button btn, Image img)
    {
        btn.interactable = unlocked;
        img.enabled = unlocked ? false : true;
    }

    void changeSelectedLvl(Button btn, string[] copy, Sprite img, string lvlName)
    {
        foreach (Button child in levelBtns)
        {
            Image[] images = child.GetComponentsInChildren<Image>();
            foreach (Image image in images) 
                if (image.tag == "Locked" && image.enabled == false) child.interactable = true;
        }

        btn.interactable = false;
        briefing.text = copy[0];
        levelImg.sprite = img;
        if (PlayerPrefs.GetString(lvlName + "_completed") != null && PlayerPrefs.GetString(lvlName + "_completed") != "") completedText.SetActive(true);
        else completedText.SetActive(false);
    }
}
