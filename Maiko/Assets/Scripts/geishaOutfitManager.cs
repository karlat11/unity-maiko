using UnityEngine;

public class geishaOutfitManager : MonoBehaviour
{
    public GameObject[] list;

    private void Awake()
    {
        foreach(GameObject child in list)
        {
            if (child.name == PlayerPrefs.GetString("PlayerHair") ||
                child.name == PlayerPrefs.GetString("PlayerMakeup") ||
                child.name == PlayerPrefs.GetString("PlayerDress"))
            {
                child.SetActive(true);
            } 
            
            else child.SetActive(false);
        }
    }
}
