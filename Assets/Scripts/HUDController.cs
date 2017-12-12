using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Bomber8Bit.UI
{
	public class HUDController : MonoBehaviour 
	{
		public static HUDController Instance;

		[SerializeField] private GameObject gameOverPanel;
		[SerializeField] private Text playerWinText;

		private void Awake () 
		{
			if (Instance == null)
				Instance = this;
			else if (Instance != this)
				Destroy (this.gameObject);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R) && gameOverPanel.activeInHierarchy)
				RestartGame ();
		}

		/// <summary>
		/// 
		/// </summary>
		private void ShowGameOverPanel(bool active)
		{
			gameOverPanel.SetActive (active);
		}

		/// <summary>
		/// 
		/// </summary>
		public void SetWinInfo(int killer)
		{
			ShowGameOverPanel (true);
			if (killer == 1)
			{
				playerWinText.text = "RED Wins";
				playerWinText.color = new Color32 (247, 124, 90, 255);
			} 
			else if (killer == 2)
			{
				playerWinText.text = "BLUE Wins";
				playerWinText.color = new Color32 (81, 173, 255, 255);
			} 
			else
			{
				playerWinText.text = "Draw :'(";
				playerWinText.color = new Color32 (255, 255, 255, 255);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void RestartGame()
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}
}