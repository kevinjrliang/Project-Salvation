using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustPower = 1000f;
    [SerializeField] float reverseThrustPower = 750f;
    [SerializeField] float rotationPower = 50f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip rotatingThrusters;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;

    bool leftThrustersPlaying = false;
    bool rightThrustersPlaying = false;
    bool boostPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.W))
        {
            ApplyThrust(Vector3.up, thrustPower);
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            mainEngineParticle.Stop();
            StopPlaying();
        }
    }

    void StopPlaying() {
        boostPlaying = false;
        leftThrustersPlaying = false;
        rightThrustersPlaying = false;

        audioSource.Stop();
    }

    private void ApplyThrust(Vector3 direction, float power)
    {
        if (!audioSource.isPlaying || !boostPlaying) { 
            audioSource.PlayOneShot(mainEngine);
            boostPlaying = true;
        }
        if (!mainEngineParticle.isPlaying) {
            mainEngineParticle.Play();
        }
        rb.AddRelativeForce(direction * power * Time.deltaTime);
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            RotateBodyForward(rotationPower);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            rightThrusterParticle.Stop();
            StopPlaying();
        }

        if (Input.GetKey(KeyCode.D)) {
            RotateBodyBackwards(rotationPower);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            leftThrusterParticle.Stop();
            StopPlaying();
        }
    }

    private void RotateBodyForward(float power)
    {
        Vector3 direction = Vector3.forward;
        if (!rightThrusterParticle.isPlaying) {
            rightThrusterParticle.Play();
        }
        if (!audioSource.isPlaying || !rightThrustersPlaying) {
            audioSource.PlayOneShot(rotatingThrusters);
            rightThrustersPlaying = true;
        }
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(direction * power * Time.deltaTime, Space.Self);
        rb.freezeRotation = false; // unfreezing rotation so the physics system takes over;
    }

    private void RotateBodyBackwards(float power)
    {
        Vector3 direction = Vector3.back;
        if (!leftThrusterParticle.isPlaying) {
            leftThrusterParticle.Play();
        }
        if (!audioSource.isPlaying || !leftThrustersPlaying) {
            audioSource.PlayOneShot(rotatingThrusters);
            leftThrustersPlaying = true;
        }
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(direction * power * Time.deltaTime, Space.Self);
        rb.freezeRotation = false; // unfreezing rotation so the physics system takes over;
    }
}
