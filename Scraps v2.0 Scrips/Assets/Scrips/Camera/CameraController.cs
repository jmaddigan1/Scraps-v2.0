using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;

	public List<Transform> PlayerCharacters;
	public List<Transform> ObjectTargets;

	public Transform Target;

	public bool smoothCamera = true;

	public int CharacterIndex = 0;
	public bool Looking = false;
	public float Speed = 3.2f;

	public Vector3 Offset = new Vector3(0, 3f, 4f);

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		// Find all player object in the scene.
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{

			// Add each player to the player character list..
			// so we can cycle through then.
			PlayerCharacters.Add(player.transform);

			// Turn off each player
			player.GetComponent<Player>().SetPlayerActive(false);
		}

		// Target the first player object found.
		// And turn it on.
		if (PlayerCharacters.Count > 0)
		{
			Target = PlayerCharacters[0];
			TogglePlayer(PlayerCharacters[CharacterIndex], true);
		}
	}

	// Update is called once per frame
	// Move to the "Target"
	void Update()
	{
		// Sawp characters
		if (Input.GetKeyDown(KeyCode.T)) CameraController.Instance.SpawnActiveCharacter();

		if (Target == null) return;

		// TODO:(Nathen) Improve the cameras lerping code

		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = Target.position + Offset;

		if (smoothCamera)
		{
			float distance = Vector3.Distance(transform.position, targetPosition);
			float cameraSpeed = distance < 1 ? Speed * distance : Speed;

			if (Looking == false) {
				cameraSpeed = distance > 5 ? 35.0f : cameraSpeed;
			}

			transform.position = Vector3.MoveTowards(currentPosition, targetPosition, cameraSpeed * Time.deltaTime);
		}
		else
		{ 
			transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * 2f);
		}	
	}

	// Change the state of a player from active to false or vice versa
	public void TogglePlayer(Transform currentPlayer, bool state)
	{
		if (currentPlayer.TryGetComponent<Player>(out Player disable))
		{
			disable.SetPlayerActive(state);
		}
	}

	// Change the character we are controlling
	public void SpawnActiveCharacter()
	{
		// Turn off current player
		TogglePlayer(PlayerCharacters[CharacterIndex], false);

		// Update selected character
		CharacterIndex = (CharacterIndex + 1) % PlayerCharacters.Count;

		// Target and turn on new character
		if (Looking == false)
		{
			Target = PlayerCharacters[CharacterIndex];
		}

		TogglePlayer(PlayerCharacters[CharacterIndex], true);
	}

	// Look at a target in the environment
	// Start a coroutine
	public void LookAtTarget(Transform targets, Vector3 offset, float duration)
	{
		StartCoroutine(coLookAtTarget(new List<Transform> { targets }, offset, duration));
	}
	public void LookAtTarget(List<Transform> targets, Vector3 offset, float duration)
	{
		StartCoroutine(coLookAtTarget(targets, offset, duration));
	}

	private IEnumerator coLookAtTarget(List<Transform> targets, Vector3 offset, float duration)
	{
		// If we are currently looking at something.
		// We wait for that to end.
		while (Looking == true) yield return null;
		smoothCamera = true;

		// We are now looking at something.
		Looking = true;

		float counter = 0.0f;

		foreach (Transform target in targets)
		{
			Target = target;
			counter = 0.0f;

			while ((counter += Time.deltaTime) < duration)
			{
				yield return null;
			}
		}

		Target = PlayerCharacters[CharacterIndex];

		// We are no longer looking at somthing
		smoothCamera = false;
		Looking = false;
	}
}
