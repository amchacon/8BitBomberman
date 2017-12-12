using UnityEngine;

namespace Bomber8Bit.Manager
{
	public class BoxBehaviour : MonoBehaviour {
		public GameObject fragmentsBox;
		public GameObject[] pickups;

		public void OnTriggerEnter(Collider other) 
		{
			if (other.CompareTag("Explosion")) 
			{
				Instantiate(fragmentsBox, transform.position, transform.rotation);

				int spawnPickupChance = Random.Range (0, 100);
				if (spawnPickupChance < 30)
					Instantiate(pickups[Random.Range(0,pickups.Length)], transform.position, transform.rotation);
				
				Destroy(gameObject);
			}
		}
	}
}