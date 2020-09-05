using UnityEngine;
using UnityEngine.UI;

public class levelSelectionManager : MonoBehaviour
{
    public Button[] levelBtns;
    public GameObject btns;

    private int levelsUnlocked;
    //private Button[] lvlBtns;

    private void Start()
    {
        Button[] test = btns.GetComponentsInChildren<Button>();
        //lvlBtns = test;

        if (test != null && test.Length != 0)
        {
            foreach (Button btn in test)
            {
                btn.interactable = false;
                //GameObject[] img = btn.GetComponentsInChildren<GameObject>();
                //foreach(GameObject im in img) Debug.Log("Here: " + im);

                //img.enabled = true;
            }
        }
    }
}
