using UnityEngine;
using Bomber8Bit.Player;
using System.Collections;

namespace Bomber8Bit.Interactable
{
	public abstract class Interactable : MonoBehaviour 
	{
		IEnumerator Start()
		{
			yield return new WaitForSeconds (0.6f);
			GetComponent<SphereCollider> ().enabled = true;
		}

		public abstract void Interact(PlayerStats player);

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag ("Player"))
			{
				Interact (other.GetComponent<PlayerStats>());
				Destroy (gameObject);
			}

			if (other.CompareTag("Explosion")) 
			{
				Destroy(gameObject);
			}
		}
	}
}