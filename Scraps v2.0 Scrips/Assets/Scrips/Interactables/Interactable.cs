using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public abstract class Interactable : MonoBehaviour
{
	[HideInInspector] public bool updatePlayerInteractable = true;

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
		BoxCollider collider = GetComponent<BoxCollider>();
		Gizmos.DrawCube(transform.position + collider.center, collider.size);
	}
}
