using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
    public static Audio instance = null;
    public AudioSource efxSource;
    void Awake() {
        //dead.clip = Resources.Load<AudioClip>("Sounds/gameover");
        //ambient.clip = Resources.Load<AudioClip>("Sounds/ambientover");
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        //if (Application.loadedLevelName != "MainMenu")
          //  DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip) {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    public void GameOver() {
        GameObject.Find("Main Camera").GetComponent<CameraFilterPack_FX_Glitch1>().enabled = true;
        AudioClip over = Resources.Load<AudioClip>("Sounds/gameover") as AudioClip;
        AudioSource.PlayClipAtPoint(over, Vector2.zero);
        StartCoroutine(HaltOver());
    }

    private IEnumerator HaltOver() {
        yield return new WaitForSeconds(1f);
        AudioClip reel = Resources.Load<AudioClip>("Sounds/ambientover") as AudioClip;
        AudioSource.PlayClipAtPoint(reel, Vector2.zero);
        yield return new WaitForSeconds(1f);
        Application.LoadLevel("MainMenu");
    }
}
