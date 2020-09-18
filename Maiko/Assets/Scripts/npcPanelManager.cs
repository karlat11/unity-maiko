using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class npcPanelManager : MonoBehaviour
{
    public static string detectedBy = "";
    public static bool scoreIncreased = false;
    public string customBriefing;

    private static TextMeshProUGUI copy;
    private static TextMeshProUGUI npcName;
    private static Image npcImg;
    private Button closePopUpBtn;

    private void Awake()
    {
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
    }

    private void Start()
    {        
        closePopUpBtn.onClick.AddListener(delegate () {
            UIManager.nPanel.SetActive(false);
        });

        copy.text = "Kunoichi " + PlayerPrefs.GetString("PlayerSurname") + " " + PlayerPrefs.GetString("PlayerName") + customBriefing;
    }

    public static void UpdateDetection(string name, string dialogue, Sprite img)
    {
        npcName.text = name + ":";
        copy.text = dialogue;
        npcImg.sprite = img;
        UIManager.nPanel.SetActive(true);
    }
}
