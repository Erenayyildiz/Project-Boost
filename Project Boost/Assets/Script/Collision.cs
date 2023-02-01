using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem succesParticle;
    [SerializeField] ParticleSystem failedParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
    }
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (isTransitioning)
        {
            return;
        }

        if (collision.gameObject.tag == "DeathArea")
        {
            StartCrash();
        }

        if (collision.gameObject.tag == "Finish")
        {
           StartSuccess();
        }
            
    }

    void StartSuccess()
    {
        isTransitioning= true;
        audioSource.Stop();
        succesParticle.Play();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadNextLevel", 1f);
    }

    void StartCrash()
    {
        isTransitioning= true;
        audioSource.Stop();
        failedParticle.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void ReloadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
