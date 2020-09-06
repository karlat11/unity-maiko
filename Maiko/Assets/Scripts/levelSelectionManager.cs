using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class levelSelectionManager : MonoBehaviour
{
    public GameObject btns;
    public TextMeshProUGUI briefing;

    private int levelsUnlocked;
    private Button[] levelBtns;

    private void Start()
    {
        levelBtns = btns.GetComponentsInChildren<Button>();
        levelsUnlocked = 3;

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
                    changeSelectedLvl(btn, bCreator.sentences);
                });

                i++;
            }
        }
    }

    void updateLvlBtn(bool unlocked, Button btn, Image img)
    {
        btn.interactable = unlocked;
        img.enabled = unlocked ? false : true;
    }

    void changeSelectedLvl(Button btn, string[] copy)
    {
        foreach (Button child in levelBtns) child.interactable = true;
        btn.interactable = false;
        briefing.text = copy[0];
    }
}
