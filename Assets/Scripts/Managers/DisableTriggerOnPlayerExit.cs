using UnityEngine;

namespace Bomber8Bit.Manager
{
	/// <summary>
	/// This script makes sure that a bomb can be laid down at the player's feet without causing buggy movement when the player walks away.
	/// It disables the trigger on the collider, essentially making the object solid.
	/// </summary>
	public class DisableTriggerOnPlayerExit : MonoBehaviour 
	{
	    public void OnTriggerExit(Collider other) 
		{
			//When the player exits the trigger area
	        if (other.gameObject.CompareTag("Player")) 
			{
				//Disable the trigger
	            GetComponent<Collider>().isTrigger = false; 
	        }
	    }
	}
}