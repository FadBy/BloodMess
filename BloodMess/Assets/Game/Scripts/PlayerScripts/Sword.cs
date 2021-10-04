using UnityEngine;

public class Sword : MonoBehaviour
{
    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void ParticleEmmision()
    {
        particles.Play();
    }
}
