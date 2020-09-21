using UnityEngine;
using UnityEngine.SceneManagement;

public class audioManager : MonoBehaviour
{
    private static audioManager instance = null;
    private AudioSource audio;
    public static audioManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        } else
        {
            instance = this;
        }
        Debug.Log("scene: " + SceneManager.GetActiveScene().name);
        DontDestroyOnLoad(gameObject);

        audio = GetComponent<AudioSource>();
    }

    void PlayMusic()
    {
        if (audio.isPlaying) return;
        audio.Play();
    }

    void StopMusic()
    {
        audio.Stop();
    } 
}