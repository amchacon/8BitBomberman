using UnityEngine;
using Bomber8Bit.Player;
using Bomber8Bit.Util;

namespace Bomber8Bit.Interactable
{
	public class PWRemoteBomb : Interactable
	{
		public override void Interact (PlayerStats p)
		{
			Debug.Log ("Remote Bomb On");
			p.GetComponent<RemoteControl> ().EnableRemote();
		}
	}
}