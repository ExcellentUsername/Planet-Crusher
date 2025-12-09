using UnityEngine;

public class SoundManager : MonoBehaviour
{
    void OnCollisionEnter()  //Plays Sound Whenever collision detected
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
