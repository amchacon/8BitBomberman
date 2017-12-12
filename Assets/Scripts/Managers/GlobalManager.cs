using UnityEngine;
using UnityEngine.UI;
using Bomber8Bit.UI;

namespace Bomber8Bit.Manager
{
	/// <summary>
	/// 
	/// </summary>
	public class GlobalManager : MonoBehaviour 
	{
		public static GlobalManager Instance;

		private int deadPlayers = 0;
		private int deadPlayerNumber = -1;

		public Text timerText;
		private float minutes;
		private float seconds;
		private float timer = 180;

		public float Timer
		{
			get { return timer; }
		}

		private bool isGameOver;

		public bool IsGameOver
		{
			get { return isGameOver; }
		}

		private void Awake () 
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy (this.gameObject);
		}

		private void Start()
		{
			TimerGlobal ();
		}

		private void Update()
		{
			if (isGameOver)
				return;
			
			TimerGlobal ();
			if (timer <= 0)
				GameOver ();
		}

		/// <summary>
		/// 
		/// </summary>
	    public void PlayerDied(int playerNumber) 
		{
			//Adds one dead player
			deadPlayers++;
			//If this is the first player that died...
			if (deadPlayers == 1) 
			{
				//It sets the dead player number to the player that died first
				deadPlayerNumber = playerNumber;
				//Checks if the other player also died or if just one bit the dust after 0.3 seconds
				Invoke("GameOver", .3f);
			}  
	    }

		/// <summary>
		/// 
		/// </summary>
		private void CheckPlayersDeath() 
		{
			//A single player died and he's the loser
			if (deadPlayers == 1)
			{ 
				//Player 1 died so Player 2 is the winner
				if (deadPlayerNumber == 1)
				{
					Debug.Log ("Player 2 is the winner!");
					HUDController.Instance.SetWinInfo (2);
				}
				//Player 2 died so Player 1 is the winner
				else
				{
					Debug.Log ("Player 1 is the winner!");
					HUDController.Instance.SetWinInfo (1);
				}
			} 
			//Both players died, so it's a draw
			else
			{
				Debug.Log ("The game ended in a draw!");
				HUDController.Instance.SetWinInfo (3);
			}
		} 

		/// <summary>
		/// 
		/// </summary>
		private void GameOver ()
		{
			isGameOver = true;
			CheckPlayersDeath ();
			timerText.enabled = false;
		}

		/// <summary>
		/// 
		/// </summary>
		private void TimerGlobal()
		{
			timer -= Time.deltaTime;
			minutes = Mathf.Floor(timer / 60);
			seconds = Mathf.RoundToInt(timer%60);
			if (seconds < 10) {
				timerText.text = minutes.ToString () + ":0" + seconds.ToString ();
			} else {
				timerText.text = minutes.ToString () + ":" + seconds.ToString ();
			}
		}
	}
}