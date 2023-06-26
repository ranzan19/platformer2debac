using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayHelperJump : MonoBehaviour
{
    public KeyCode keycode = KeyCode.J;
    public AudioSource audioSource;

    private void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            PlayJ();
        }
    }

    public void PlayJ()
    {
        audioSource.Play();
    }
}
