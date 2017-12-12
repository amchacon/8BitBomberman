using UnityEngine;
using Bomber8Bit.Player;

namespace Bomber8Bit.Interactable
{
	public class PWSpeed : Interactable
	{
		public override void Interact (PlayerStats p)
		{
			Debug.Log ("Increase Player Speed");
			p.MoveSpeed *= 1.25f;
		}
	}
}