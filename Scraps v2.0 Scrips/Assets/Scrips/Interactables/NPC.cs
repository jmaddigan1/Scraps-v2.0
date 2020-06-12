using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
	public override void Interact(Player player)
	{
		DialogueManager.Instance.SetText("Yo dog whaz up?");
	}

	public override void OnEnterInteractable()
	{
		Debug.Log("Talk to the Dude");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 0, 1, 0.1f);
		Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one * 2);
	}
}
