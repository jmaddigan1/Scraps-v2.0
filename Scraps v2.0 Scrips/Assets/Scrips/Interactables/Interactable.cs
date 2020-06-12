using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public abstract class Interactable : MonoBehaviour
{
	public virtual void Interact(Player player) { }

	public virtual void OnEnterInteractable() { Debug.Log($"Player entered: {gameObject.name}"); }
	public virtual void OnExitInteractable() { }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player)) {
			player.selectedInteractable = this;
			OnEnterInteractable();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player)) {
			if (player.selectedInteractable == this)
			{
				player.selectedInteractable = null;
				OnExitInteractable();
			}
		}
	}

	private void OnDrawGizmos()
	{
		BoxCollider collider = GetComponent<BoxCollider>();
		Gizmos.DrawCube(transform.position + collider.center, collider.size);
	}
}
