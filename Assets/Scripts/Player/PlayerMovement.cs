using UnityEngine;
using Bomber8Bit.Manager;

//TODO Isolate inputs on PlayerInputPC class

namespace Bomber8Bit.Player
{
	public class PlayerMovement : MonoBehaviour 
	{
		[Header("Player parameters")]
		[Range(1, 2)]
		[SerializeField]  private int playerNumber = 1; 	//Indicates what player this is: P1 or P2
		[SerializeField]  private bool canMove = true; 		//Can the player move?

		[Header("Components references")]
		[SerializeField] private Transform myTransform;
		[SerializeField] private Rigidbody rigidBody;
		[SerializeField] private Animator animator;
		[SerializeField] private PlayerAttack playerAttack;
		[SerializeField] private PlayerStats stats;

		private void Reset()
		{
			myTransform = this.transform;
			rigidBody = GetComponent<Rigidbody> ();
			animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
			playerAttack = GetComponent<PlayerAttack> ();
			stats = GetComponent<PlayerStats> ();
		}

		// Use this for initialization
		private void Start() 
		{
			//Cache the attached components for better performance and less typing
			rigidBody = GetComponent<Rigidbody>();
			stats = GetComponent<PlayerStats> ();
			myTransform = transform;
			animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
		}

		// Update is called once per frame
		private void Update() 
		{
			if (GlobalManager.Instance.IsGameOver)
				return;
			UpdateControls();
		}

		private void UpdateControls() 
		{
			animator.SetBool("Walking", false);
			if (!canMove) 
				return;
			//Depending on the player number, use different input for moving
			if (playerNumber == 1) 
				UpdatePlayer1Controls();
			else 
				UpdatePlayer2Controls();
		}

		public void OnTriggerEnter(Collider other) 
		{
			if (other.CompareTag("Explosion")) 
			{
				Debug.Log("P" + playerNumber + " hit by explosion!");
				canMove = false;
				//Notifies the global state manager that the player died
				GlobalManager.Instance.PlayerDied(playerNumber);
				//Destroys the player GameObject
				Destroy(gameObject);
			}
		}

		#region Standalone Inputs
		/// <summary>
		/// Updates Player 1's movement and facing rotation using the WASD keys and drops bombs using Space
		/// </summary>
		private void UpdatePlayer1Controls() 
		{
			if (Input.GetKey(KeyCode.W)) 
			{ 
				MoveUp ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.A)) 
			{ 
				MoveLeft ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.S)) 
			{ 
				MoveDown ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.D)) 
			{ 
				MoveRight ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKeyDown(KeyCode.Space)) 
			{ 
				//Drop bomb
				playerAttack.DropBomb();
			}

			if (Input.GetKeyDown(KeyCode.Q) && stats.RemoteBomb) 
			{
				//Remote Explode
				Bomb[] activeBombs = FindObjectsOfType<Bomb>();
				foreach (var item in activeBombs)
				{
					if (item.owner.gameObject == this.gameObject)
					{
						item.Explode ();
					}
				}
			}
		}

		/// <summary>
		/// Updates Player 2's movement and facing rotation using the arrow keys and drops bombs using Enter or Return
		/// </summary>
		private void UpdatePlayer2Controls() 
		{
			if (Input.GetKey(KeyCode.UpArrow)) 
			{ 
				MoveUp ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.LeftArrow)) 
			{ 
				MoveLeft ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.DownArrow)) 
			{ 
				MoveDown ();
				animator.SetBool("Walking",true);
			}

			if (Input.GetKey(KeyCode.RightArrow)) 
			{ 
				MoveRight ();
				animator.SetBool("Walking",true);
			}

			if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) 
			{ 
				//Drop bomb
				playerAttack.DropBomb();
			}

			if (Input.GetKeyDown(KeyCode.P) && stats.RemoteBomb) 
			{
				//Remote Explode
				Bomb[] activeBombs = FindObjectsOfType<Bomb>();
				foreach (var item in activeBombs)
				{
					if (item.owner.gameObject == this.gameObject)
					{
						item.Explode ();
					}
				}
			}
		}
		#endregion

		#region Move Directions
		private void MoveUp()
		{
			//Up movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, stats.MoveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 0, 0);
		}

		private void MoveDown()
		{
			//Down movement
			rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -stats.MoveSpeed);
			myTransform.rotation = Quaternion.Euler(0, 180, 0);
		}

		private void MoveRight()
		{
			//Right movement
			rigidBody.velocity = new Vector3(stats.MoveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 90, 0);
		}

		private void MoveLeft()
		{
			//Left movement
			rigidBody.velocity = new Vector3(-stats.MoveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
			myTransform.rotation = Quaternion.Euler(0, 270, 0);
		}
		#endregion
	}
}