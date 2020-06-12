using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] Player player;

    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxisRaw("Horizontal");

		if (Input.GetKeyDown(KeyCode.Space)) {
			player.Jump();
		}

		player.Move(-x);
	}
}
