using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public abstract class Interactable : MonoBehaviour
{
	public bool updatePlayerInteractable = true; // If we want this to override the current  interactable 
	public bool showInteractableGizmos = true; // If we want to show this interactables gizmos

	public virtual void Interact(Player player) { }

	public virtual void OnEnterInteractable(Player player) { Debug.Log($"Player entered: {gameObject.name}"); }
	public virtual void OnExitInteractable(Player player) { }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player)) {
			if (updatePlayerInteractable) player.selectedInteractable = this;
			OnEnterInteractable(player);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player)) {
			if (player.selectedInteractable == this)
			{
				if (updatePlayerInteractable) player.selectedInteractable = null;
				OnExitInteractable(player);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (showInteractableGizmos == false) return;

		BoxCollider collider = GetComponent<BoxCollider>();
		Gizmos.DrawCube(transform.position + collider.center, collider.size);
	}
}
