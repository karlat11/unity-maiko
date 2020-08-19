using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        surnameNext.onClick.AddListener(delegate () {
            surnameIdx = nextIdx(surnameIdx, surnameText, surnameList);
        });

        surnamePrev.onClick.AddListener(delegate () {
            surnameIdx = prevIdx(surnameIdx, surnameText, surnameList);
        });

        nameNext.onClick.AddListener(delegate () {
            nameIdx = nextIdx(nameIdx, nameText, nameList);
        });

        namePrev.onClick.AddListener(delegate () {
            nameIdx = prevIdx(nameIdx, nameText, nameList);
        });
    }

    private int prevIdx(int currentIdx, TextMeshProUGUI text, string[] list)
    {
        if (currentIdx > 0) currentIdx--;
        else currentIdx = list.Length - 1;
        text.text = list[currentIdx];

        return currentIdx;
    }

    private int nextIdx(int currentIdx, TextMeshProUGUI text, string[] list)
    {
        if (currentIdx < list.Length - 1) currentIdx++;
        else currentIdx = 0;
        text.text = list[currentIdx];
        return currentIdx;
    }
}
