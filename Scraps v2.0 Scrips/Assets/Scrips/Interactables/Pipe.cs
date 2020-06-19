using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : Interactable
{
	public List<Transform> Path;

	public override void Interact(Player player)
	{
		StartCoroutine(coFollowPath(player));
	}

	private IEnumerator coFollowPath(Player player)
	{
		EnterPath(player);

		// Send list of things to look at
		CameraController.Instance.LookAtTarget(Path, Vector3.zero, false);

		// Wait till we are finished looking
		// NOTE:(Nathen) This might be longer than you think is multiple things are qued up for the camera to look at
		while (CameraController.Instance.Looking) yield return null;

		ExitPath(player);
	}

	private void EnterPath(Player player)
	{

	}

	private void ExitPath(Player player)
	{
		player.transform.position = Path[Path.Count - 1].position;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 0, 1, 0.1f);
		Gizmos.DrawCube(transform.position + Vector3.up, Vector3.one * 2);

		for (int index = 0; index < Path.Count; index++) {
			if (index != 0)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(Path[index - 1].position, Path[index].position);
			}
		}
	}
}
