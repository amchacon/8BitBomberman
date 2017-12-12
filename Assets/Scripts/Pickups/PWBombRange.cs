using UnityEngine;
using Bomber8Bit.Player;

namespace Bomber8Bit.Interactable
{
	public class PWBombRange : Interactable
	{
		public override void Interact (PlayerStats p)
		{
			Debug.Log ("Increase Bomb Range");
			p.BombRange++;
		}
	}
}