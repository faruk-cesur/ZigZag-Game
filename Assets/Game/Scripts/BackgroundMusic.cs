using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Singleton Design Pattern & Dont Destroy On Load Music
    private static BackgroundMusic music = null;

    void Awake()
    {
        if (music == null)
        {
            music = this;
            DontDestroyOnLoad(this);
        }
        else if (this != music)
        {
            Destroy(gameObject);
        }
    }
}