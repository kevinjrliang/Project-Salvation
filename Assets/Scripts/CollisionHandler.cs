using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] int startingScene = 0;
    [SerializeField] float respawnDelay = 2f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip successAudio;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    bool enableDebug = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other) {
        if (!isTransitioning && !collisionDisabled) {
            switch (other.gameObject.tag) {
                case "Friendly":
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

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            enableDebug = !enableDebug;
        }
        if (enableDebug) {
            RespondToDebugKeys();
        }
    }

    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel(); // Skip level
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            ReloadLevel();  // reload level
        }
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void ReloadLevel() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // retrieves the index of the current scene
        SceneManager.LoadScene(sceneIndex);
    }

    void NextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // retrieves the index of the current scene and add 1
        if (SceneManager.sceneCountInBuildSettings <= nextSceneIndex) {
            nextSceneIndex = startingScene;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        explosionParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        Invoke("ReloadLevel", respawnDelay);
    }

    void StartSuccessSequence() {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);
        Invoke("NextLevel", respawnDelay);
    }
}
