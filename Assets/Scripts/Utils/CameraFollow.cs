using UnityEngine;
using System.Collections;

namespace Bomber8Bit.Util
{
	public class CameraFollow : MonoBehaviour 
	{
		public float padding = 1.15f;
		public float smoothing = 15f;
		public float cameraSize = 6.0f; 

		public Vector3 offset = new Vector3(0,10,0); 
		private GameObject[] players; 
		private float distance;

		private Vector3 cameraTarget;
		private Transform myTransform;

		void Awake()
		{
			myTransform = transform;
		}

		void OnEnable ()
		{
			players = GameObject.FindGameObjectsWithTag("Player"); 
		}

		void Update() 
		{
			cameraTarget = (players[0].transform.position + players[1].transform.position) * 0.5f;
			myTransform.position = Vector3.Lerp(myTransform.position, cameraTarget + offset, smoothing * Time.deltaTime);
			distance = Vector3.Distance(players[0].transform.position, players[1].transform.position);
			float size = Mathf.Clamp(distance / Camera.main.aspect * padding, cameraSize, float.MaxValue);
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size, smoothing * Time.deltaTime);
		}
	} 
}