using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            audioSource.Play(); 
        }
    }
}
