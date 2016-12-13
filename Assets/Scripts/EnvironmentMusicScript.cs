using UnityEngine;
using System.Collections;

public class EnvironmentMusicScript : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip currentlyPlaying;

	public bool fightOnGoing;
	public AudioClip[] explorationTracks;
	public AudioClip[] fightTracks;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		currentlyPlaying = explorationTracks [Random.Range (0, explorationTracks.Length - 1)];
		audioSource.clip = currentlyPlaying;
		audioSource.Play ();
	}

	void Update () {
		if (fightOnGoing && !audioSource.isPlaying) {
			currentlyPlaying = fightTracks [Random.Range (0, fightTracks.Length - 1)];
			audioSource.clip = currentlyPlaying;
			audioSource.Play ();
		}

		if (!audioSource.isPlaying) {
			currentlyPlaying = explorationTracks [Random.Range (0, explorationTracks.Length - 1)];
			audioSource.clip = currentlyPlaying;
			audioSource.Play ();
		}

		if (Input.GetKeyDown (KeyCode.M)) {
			FightStarted ();
		}

		if (Input.GetKeyDown (KeyCode.N)) {
			FightStopped ();
		}
	}

	public void FightStarted(){
		currentlyPlaying = fightTracks [Random.Range (0, fightTracks.Length - 1)];
		audioSource.clip = currentlyPlaying;
		audioSource.Play ();
		fightOnGoing = true;
	}

	public void FightStopped(){
		currentlyPlaying = explorationTracks [Random.Range (0, explorationTracks.Length - 1)];
		audioSource.clip = currentlyPlaying;
		audioSource.Play ();
		fightOnGoing = false;
	}
}
