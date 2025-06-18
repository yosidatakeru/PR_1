using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    public ParticleSystem sparkR;
    public ParticleSystem sparkL;

    void Update()
    {
        if (transform.position.x >= 34f)
        {
            if (!sparkR.isPlaying) sparkR.Play();
        }
        else
        {
            if (sparkR.isPlaying) sparkR.Stop();
        }

        if (transform.position.x <= -34f)
        {
            if (!sparkL.isPlaying) sparkL.Play();
        }
        else
        {
            if (sparkL.isPlaying) sparkL.Stop();
        }
    }
}
