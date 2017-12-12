using UnityEngine;
using Bomber8Bit.Player;

namespace Bomber8Bit.Interactable
{
	public class PWBombLimit : Interactable
	{
		public override void Interact (PlayerStats p)
		{
			Debug.Log ("Increase Bomb Limit");
			p.BombsLimit++;
		}
	}
}