using UnityEngine;

// Manage particle to let them alive when a bullet is destroy
[RequireComponent(typeof(ParticleSystem))]
public class BulletParticles : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particles = null;
	[SerializeField] private Transform _particleParent = null;                       // Transform which contains particle

	public int ParticleCount => _particles.particleCount;

	// Position particles in his original position
	public void Link()
	{
		transform.SetParent(_particleParent, false);
		_particles.Play(true);
	}

	// Loacl position to world position
	public void Unlink()
	{
		transform.SetParent(null, false);
		_particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
}
