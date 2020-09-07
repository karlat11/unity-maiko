using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class npcPanelManager : MonoBehaviour
{
    [HideInInspector]
    public static string detectedBy = "";

    private static TextMeshProUGUI copy;
    private static TextMeshProUGUI npcName;
    private static TextMeshProUGUI scoreCounter;
    private static Image npcImg;
    private static npcPanelManager self;

    private void Awake()
    {
        detectedBy = "";

        TextMeshProUGUI[] textObjects = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI obj in textObjects)
        {
            if (obj.tag == "Copy") copy = obj;
            else if (obj.tag == "npcName") npcName = obj;
            else if (obj.tag == "scoreCounter") scoreCounter = obj;
        }

        Image[] imgObjects = GetComponentsInChildren<Image>();
        foreach (Image obj in imgObjects)
        {
            if (obj.tag == "npcImage") npcImg = obj;
        }
    }

    public static void UpdateDetection(string name, string dialogue, Sprite img)
    {
        npcName.text = name + ":";
        copy.text = dialogue;
        npcImg.sprite = img;
    }
    void UpdateInteractables()
    {
        //TODO
    }
}
