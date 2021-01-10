using UnityEngine;

public class ParticlePool : MonoBehaviour
{
	[SerializeField]
	private BulletParticles[] _bulletParticles      // Let particle when bullet his alive 
		= new BulletParticles[0];
	[SerializeField]
	private SettingsHandler _settingsHandler;       // If we show the particle or not

	private BulletParticles _currentParticle;

	private void Start()
	{
		if (!_settingsHandler)
		{
			Debug.LogError($"Settings Handler is undefined in {name}");
			return;
		}

		if (!_settingsHandler.Current.ShowParticule)
		{
			Destroy(gameObject);
		}
	}

	// Select one particle which have no particle / inactive
	public void LinkWithParticles()
	{
		foreach (var particle in _bulletParticles)
		{
			if (particle.ParticleCount <= 0)
			{
				Link(particle);
				break;
			}
		}
	}

	// Position particles in his original position
	public void Link(BulletParticles bulletParticle)
	{
		_currentParticle = bulletParticle;
		_currentParticle.Transform.SetParent(transform, false);
		_currentParticle.Particle.Play(true);
		_currentParticle.Transform.gameObject.SetActive(true);
	}

	// Loacl position to world position
	public void Unlink()
	{
		if (_currentParticle == null) { return; }

		_currentParticle.Transform.SetParent(null, false);
		_currentParticle.Particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
}
