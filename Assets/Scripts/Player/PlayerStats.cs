using UnityEngine;
using Bomber8Bit.UI;
using Bomber8Bit.Util;
using System.Collections;


namespace Bomber8Bit.Player
{
	public class PlayerStats : MonoBehaviour
	{
		[SerializeField] private float moveSpeed = 5;
		[SerializeField] private int bombRange = 2;			//Range of the bomb explosion
		[SerializeField] private int bombsLimit = 1;		//Amount of bombs that player can drop
		[HideInInspector]public int bombsLimitTemp = 0;
		[SerializeField] private bool remoteBomb = false;

		public float MoveSpeed
		{
			get { return moveSpeed; }
			set
			{ 
				moveSpeed = value;
				if (moveSpeed >= 15)
					moveSpeed = 15;
			}
		}

		public int BombsLimit
		{
			get { return bombsLimit; }
			set { bombsLimit = value; }
		}

		public int BombRange
		{
			get { return bombRange; }
			set { bombRange = value; }
		}

		public bool RemoteBomb
		{
			get { return remoteBomb; }
			set { remoteBomb = value; }
		}
	}
}