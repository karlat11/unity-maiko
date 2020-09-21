using TMPro;
using UnityEngine;

public class nameManager : MonoBehaviour
{
    private TextMeshProUGUI nameText;

    private void Awake()
    {
        nameText = GetComponent<TextMeshProUGUI>();
        
        if (PlayerPrefs.GetString("PlayerSurname") != null && PlayerPrefs.GetString("PlayerSurname") != "" &&
            PlayerPrefs.GetString("PlayerName") != null && PlayerPrefs.GetString("PlayerName") != "")
        {
            nameText.text = PlayerPrefs.GetString("PlayerSurname") + " " + PlayerPrefs.GetString("PlayerName");
        }
    }
}
