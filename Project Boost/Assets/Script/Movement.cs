using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem MainEngineParticle;
    [SerializeField] ParticleSystem LeftEngineParticle;
    [SerializeField] ParticleSystem RightEngineParticle;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            MainEngineParticle.Play();

            if(!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!MainEngineParticle.isPlaying)
            {
                MainEngineParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            MainEngineParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);

            if (!LeftEngineParticle.isPlaying)
            {
                LeftEngineParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);

            if (!RightEngineParticle.isPlaying)
            {
                RightEngineParticle.Play();
            }
        }
        else
        {
            RightEngineParticle.Stop();
            LeftEngineParticle.Stop();
        }
    }

    void ApplyRotation(float rotationFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
