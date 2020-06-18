using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable, IPushable
{
	// The player
	[SerializeField] private Player player;

	// public	
	public bool PickedUp;

	public bool GetState() { return PickedUp; }

	private void Awake()
	{
		// We don't override the current interactable
		updatePlayerInteractable = false;
		showInteractableGizmos = false;
	}

	private void LateUpdate()
	{
		if (PickedUp && player)
		{
			transform.position = player.transform.position + Vector3.up * player.Height;
		}
	}

	public void Push() { PickedUp = true; }
	public void Drop() { PickedUp = false; if (player) { DropBox(); } }

	// Place the box in front of the player
	public void DropBox()
	{
		// Just drop if in front of me for now
		transform.position = player.transform.position + -player.transform.right;
	}

	public override void OnEnterInteractable(Player player)
	{
		this.player = player;
		player.Pushable = this;
	}
	public override void OnExitInteractable(Player player)
	{
		player.Pushable = null;
		this.player = null;
	}

	// Override the Interactables OnTriggerExit
	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player)) {
			if (player.Pushable == (IPushable)this)
			{
				OnExitInteractable(player);
			}
		}
	}
}
