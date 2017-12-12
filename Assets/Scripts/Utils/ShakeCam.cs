using UnityEngine;
using System.Collections;

namespace Bomber8Bit.Util
{
	public class ShakeCam : MonoBehaviour 
	{

		bool isShaking;

		public void CameraShake(float shakeDuration = 0.1f, float shakeMagnitude = 0.1f){
			if (!isShaking) {
				StartCoroutine (Shake(shakeDuration, shakeMagnitude));
			}
		}

		IEnumerator Shake(float shakeDuration, float shakeMagnitude) {
			isShaking = true;
			float elapsed = 0.0f;

			Vector3 originalCamPos = this.transform.position;

			while (elapsed < shakeDuration) {

				elapsed += Time.deltaTime;          

				float percentComplete = elapsed / shakeDuration;         
				float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

				// map value to [-1, 1]
				float x = Random.value * 2.0f - 1.0f;
				float y = Random.value * 2.0f - 1.0f;
				x *= shakeMagnitude * damper;
				y *= shakeMagnitude * damper;

				this.transform.position = new Vector3(originalCamPos.x+x, originalCamPos.y+y, originalCamPos.z);

				yield return null;
			}

			this.transform.position = originalCamPos;
			isShaking = false;
		}
	}
}