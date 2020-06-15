using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightDamageZone : Interactable
{
	public float Height = 5;
	public float Range = 2;

	public bool InRange;

	public Transform Target;

	// Update is called once per frame
	void Update()
	{
		if (Target != null)
		{
			float playerX = Mathf.Abs(Target.transform.position.x - transform.position.x);

			float playerY = transform.position.y - Target.transform.position.y;
			float maxX = (playerY / Height) * Range;

			InRange = playerX < maxX;
		}
	}

	public override void OnEnterInteractable(Player player)
	{
		Target = player.transform;
	}
	public override void OnExitInteractable(Player player)
	{
		Target = null;
	}

	private void OnDrawGizmos()
	{
		Color color = InRange ? Color.red : Color.green;

		Gizmos.color = color;
		Gizmos.DrawSphere(transform.position, 0.2f);

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.up * -Height);
		Gizmos.DrawCube(transform.position + Vector3.up * -Height, Vector3.one * 0.2f);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(Range, -Height, 0));
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(-Range, -Height, 0));

		if (Target != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(new Vector3(transform.position.x, Target.transform.position.y, 0),
							new Vector3(Target.transform.position.x, Target.transform.position.y, 0));
		}
	}
}