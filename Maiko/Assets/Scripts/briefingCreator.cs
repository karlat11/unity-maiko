using UnityEngine;

[System.Serializable]
public class briefingCreator : MonoBehaviour
{
    [TextArea(2, 10)]
    public string[] sentences;

    private void Start()
    {
        
    }
}
