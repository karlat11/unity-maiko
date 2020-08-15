using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nameSelectionManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI surnameText;
    public Button nameNext;
    public Button namePrev;
    public Button surnameNext;
    public Button surnamePrev;

    private int nameIdx;
    private int surnameIdx;
    private string[] nameList;
    private string[] surnameList;

    void Start()
    {
        nameIdx = surnameIdx = 0;
        nameList = new string[] {
            "Chiyome",
            "Chiyo",
            "Mineko",
            "Chisho",
            "Haruko",
            "Sanya",
            "Sasa"
        };
        surnameList = new string[] {
            "Iwasaki",
            "Mochizuki",
            "Takaoka",
            "Kiharu",
            "Yacco",
            "Kato",
            "Abe"
        };

        nameText.text = nameList[nameIdx];
        surnameText.text = surnameList[surnameIdx];
    }

}
