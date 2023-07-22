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

    Rigidbody rb;
    AudioSource audioSource;

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

        if (Input.GetKey(KeyCode.S)) {
            ApplyThrust(Vector3.down, reverseThrustPower);
        }

        if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
            audioSource.Stop();
        }
    }

    private void ApplyThrust(Vector3 direction, float power)
    {
        if (!audioSource.isPlaying) { 
            audioSource.PlayOneShot(mainEngine);
        }
        rb.AddRelativeForce(direction * power * Time.deltaTime);
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            RotateBody(Vector3.forward, rotationPower);
        }

        else if (Input.GetKey(KeyCode.D)) {
            RotateBody(Vector3.back, rotationPower);
        }
    }

    private void RotateBody(Vector3 direction, float power)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(direction * power * Time.deltaTime, Space.Self);
        rb.freezeRotation = false; // unfreezing rotation so the physics system takes over;
    }
}
