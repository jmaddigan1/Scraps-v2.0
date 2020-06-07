using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public abstract class Interactable : MonoBehaviour, IInteractable
{
	public virtual void Interact(Player player) { }

	public virtual void OnEnterInteractable() {}
	public virtual void OnExitInteractable() {}

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
			player.selectedInteractable = null;
			OnExitInteractable();
		}
	}
}
