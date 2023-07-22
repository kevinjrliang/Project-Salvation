using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float respawnDelay = 2f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip successAudio;

    AudioSource audioSource;
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isTransitioning) {
            switch (other.gameObject.tag) {
                case "Friendly":
                    Debug.Log("you hit a friendly!");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    void ReloadLevel() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // retrieves the index of the current scene
        SceneManager.LoadScene(sceneIndex);
    }

    void NextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // retrieves the index of the current scene and add 1
        if (SceneManager.sceneCount < nextSceneIndex) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        Invoke("ReloadLevel", respawnDelay);
    }

    void StartSuccessSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        Invoke("NextLevel", respawnDelay);
    }
}
