using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParticleSystemExecutor : MonoBehaviour
{
    public List<ParticleSystem> startParticleSystems = new List<ParticleSystem>();
    public List<ParticleSystem> pushBackParticleSystems = new List<ParticleSystem>();
    public AudioClip whooshSound;


    public void PlayStartParticle()
    {
        AudioSource.PlayClipAtPoint(whooshSound, transform.position);
        foreach(ParticleSystem particleSystem in startParticleSystems)
        {
            particleSystem.Play();
        }
    }
    public void StartPushBackParticleSystem()
    {
        AudioSource.PlayClipAtPoint(whooshSound, transform.position);
        foreach(ParticleSystem particleSystem in pushBackParticleSystems)
        {
            particleSystem.Play();
        }
    }

}
