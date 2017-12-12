using UnityEngine;

namespace Bomber8Bit.Util
{
	/// <summary>
	/// Script for easily destroying an object after a while
	/// </summary>
	public class DestroySelf : MonoBehaviour 
	{
	    public float Delay = 3f; //Delay in seconds before destroying the gameobject

	    void Start() 
		{
	        Destroy(gameObject, Delay);
	    }
	}
}