using UnityEngine;
using System.Collections;
using Bomber8Bit.Player;
using Bomber8Bit.Util;

public class Bomb : MonoBehaviour 
{
	[SerializeField] private GameObject explosionPrefab;
	[SerializeField] private LayerMask levelMask;
	private bool exploded = false;
	private int range = 0;
	public PlayerAttack owner;
	private ShakeCam shakeCam;

	// Use this for initialization
	private void Start () 
	{
		shakeCam = FindObjectOfType<ShakeCam> ();
		range += owner.GetComponent<PlayerStats> ().BombRange;
		if (range <= 2)
			range = 2;
		
		if (!owner.GetComponent<PlayerStats>().RemoteBomb)
			Invoke ("Explode", 3f);
	}

	/// <summary>
	/// 
	/// </summary>
	public void Explode() {
		//Spawns an explosion at the bomb’s position
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));  
		//Disables the mesh renderer, making the bomb invisible
		GetComponent<MeshRenderer>().enabled = false;
		exploded = true;
		//Disables the collider, allowing players to move through and walk into an explosion.
		transform.Find("Collider").gameObject.SetActive(false);
		//Destroys the bomb after 0.3 seconds; this ensures all explosions will spawn before the GameObject is destroyed
		Destroy(gameObject, .3f);
		owner.droppedBombs--;
		shakeCam.CameraShake (.35f, .27f);
	} 


	/// <summary>
	/// 
	/// </summary>
	private IEnumerator CreateExplosions(Vector3 direction) 
	{
		//Iterates a for loop for every unit of distance you want the explosions to cover. In this case, the explosion will reach two meters
		for (int i = 1; i < range; i++) 
		{ 
			//A RaycastHit object holds all the information about what and at which position the Raycast hits -- or doesn't hit
			RaycastHit hit; 
			//Sends out a raycast from the center of the bomb towards the direction passed through the StartCoroutine call. 
			//It then outputs the result to the RaycastHit object. The i parameter dictates the distance the ray should travel. 
			//Finally, it uses a LayerMask named levelMask to make sure the ray only checks for blocks in the level and ignores the player and other colliders
			Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, i, levelMask); 

			//If the raycast doesn't hit anything then it's a free tile
			if (!hit.collider) { 
				Instantiate(explosionPrefab, transform.position + (i * direction),
					//Spawns an explosion at the position the raycast checked 
					explosionPrefab.transform.rotation); 
				//The raycast hits a block
			} else { 
				//Once the raycast hits a block, it breaks out of the for loop. This ensures the explosion can't jump over walls
				break; 
			}

			//Waits for 0.05 seconds before doing the next iteration of the for loop
			yield return new WaitForSeconds(.05f); 
		}
	}

	/// <summary>
	/// Check if the bomb was touched by an explosion from another, so the bomb should explode 
	/// </summary>
	public void OnTriggerEnter(Collider other) 
	{
		// Checks the the bomb hasn't exploded and if the trigger collider has the Explosion tag assigned  
		if (!exploded && other.CompareTag("Explosion")) 
		{
			//Cancel the already called Explode invocation by dropping the bomb
			CancelInvoke("Explode");
			//Explode!!!
			Explode();
		}  
	}

	public void SetOwner (PlayerAttack player)
	{
		owner = player;
	}
}
