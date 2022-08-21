using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReceiverScript : MonoBehaviour
{
    public AudioSource leftSource;
    public AudioSource rightSource;
    
    void leftFootFall()
    { 
        leftSource.Play();   
    }

    void rightFootFall()
    {
         rightSource.Play();
    }
}
