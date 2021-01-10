using UnityEngine;

// Manage particle to let them alive when a bullet is destroy
[System.Serializable]
public class BulletParticles
{
	[SerializeField] private ParticleSystem _particle;

	public int ParticleCount => _particle.particleCount;
	public ParticleSystem Particle => _particle;
	public Transform Transform => _particle.transform;
}