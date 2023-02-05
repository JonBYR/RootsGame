using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource PressSFX;
    public void Pressed()
    {
        PressSFX.Play();
    }
}
