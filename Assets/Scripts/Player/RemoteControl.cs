using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bomber8Bit.Player
{
	public class RemoteControl : MonoBehaviour 
	{
		[SerializeField] Slider slider;
		private PlayerStats stats;
		float timeToDisable;
		static WaitForSeconds updateDelay = new WaitForSeconds (0.15f);

		void Reset()
		{
			slider = GetComponent<Slider> ();
		}
			
		void Awake () 
		{
			slider.minValue = 0f;
			slider.maxValue = 0f;
		}

		void Start()
		{
			stats = GetComponent<PlayerStats> ();
			stats.bombsLimitTemp = stats.BombsLimit;
		}

		private void Update()
		{
			if (stats.RemoteBomb)
				stats.BombsLimit = 1;

			if (Time.time >= timeToDisable && stats.RemoteBomb)
			{
				DisableRemote ();
			}
		}

		public void EnableRemote()
		{
			slider.transform.parent.parent.gameObject.SetActive (true);
			timeToDisable = Time.time + 10f;
			stats.bombsLimitTemp = stats.BombsLimit;
			stats.RemoteBomb = true;
			BeginCountdown (10);
		}

		private void DisableRemote()
		{
			slider.transform.parent.parent.gameObject.SetActive (false);
			stats.RemoteBomb = false;
			stats.BombsLimit = stats.bombsLimitTemp;

			Bomb[] activeBombs = FindObjectsOfType<Bomb>();
			foreach (var item in activeBombs)
			{
				if (item.owner.gameObject == this.gameObject)
				{
					item.Explode ();
				}
			}
		}

		#region Countdown slider
		public void BeginCountdown (float cooldown) 
		{
			//Grava o tempo(momento) em que o cooldown irá terminar
			timeToDisable = Time.time + cooldown;
			//Define o valor máximo do slider
			slider.maxValue = cooldown;
			//Inicializa o valor atual do slider = ao máximo
			slider.value = cooldown;

			StartCoroutine (UpdateCountdownBar());
		}

		//Coroutine para reduzir o valor do slider, assim o player saberá 
		//quando o ataque está disponível novamente (ready)
		IEnumerator UpdateCountdownBar ()
		{
			//Enquanto o valor do slider for maior que 0...
			while (slider.value > 0f)
			{
				//...reduz o valor do slider considerando o tempo
				//no qual o slider deverá acabar - tempo atual
				slider.value = timeToDisable - Time.time;
				yield return updateDelay;
			}
			//Uma vez que o countdown termine, define o valor
			//máximo para 0 fazendo com que o slider desapareça
			slider.maxValue = 0f;
		}
		#endregion

	}
}
