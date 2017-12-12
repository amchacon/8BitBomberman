using UnityEngine;

namespace Bomber8Bit.Player
{
	public class PlayerAttack : MonoBehaviour 
	{
		[Header("Bomb parameters")]
		[SerializeField] private GameObject bombPrefab;
		[HideInInspector] public int droppedBombs = 0;

		[Header("Components references")]
		[SerializeField] private Transform myTransform;
		[SerializeField] private PlayerStats stats;


		private void Reset()
		{
			myTransform = this.transform;
			stats = GetComponent<PlayerStats> ();
		}

		private void Start() 
		{
			myTransform = transform;
			stats = GetComponent<PlayerStats> ();
		}

		private bool CanDropBombs ()
		{
			if (droppedBombs >= stats.BombsLimit)
				return false;
			return true;
		}

		/// <summary>
		/// Drops a bomb beneath the player
		/// </summary>
		public void DropBomb()
		{
			if (CanDropBombs())
			{
				//Check if bomb prefab is assigned first
				if (bombPrefab) 
				{
					GameObject bombGo = Instantiate
						(
							bombPrefab, 
							new Vector3(Mathf.RoundToInt(myTransform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)),
							bombPrefab.transform.rotation
						) as GameObject;
					bombGo.GetComponent<Bomb> ().SetOwner(this);
					droppedBombs++;
				}
			}
		}
	}
}