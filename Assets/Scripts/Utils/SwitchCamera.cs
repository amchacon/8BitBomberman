using UnityEngine;

namespace Bomber8Bit.Util
{
	public class SwitchCamera : MonoBehaviour {

		public GameObject[] cameras;
		private int index = 0;

		private void Start()
		{
			cameras [0].SetActive (true);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.C))
				ToggleCamera();
		}

		public void ToggleCamera()
		{
			cameras[index].SetActive (false);
			if (++index >= cameras.Length)
				index = 0;
			cameras[index].SetActive (true);
		}
	}
}