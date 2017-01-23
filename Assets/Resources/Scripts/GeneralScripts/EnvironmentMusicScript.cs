using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnvironmentMusicScript : MonoBehaviour {

	private AudioSource audioSource;
	private AudioClip currentlyPlaying;
	private UnityAction fightListener;
	private UnityAction noFightListener;


	public bool fightOnGoing;
	public AudioClip[] explorationTracks;
	public AudioClip[] fightTracks;

	void Awake(){
		fightListener = new UnityAction (FightStarted);
		noFightListener = new UnityAction (FightStopped);
	}

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		currentlyPlaying = explorationTracks [Random.Range (0, explorationTracks.Length - 1)];
		audioSource.clip = currentlyPlaying;
		audioSource.Play ();
	}

	void OnEnable(){
		EventManagerScript.StartListening ("fightTrack", fightListener);
		EventManagerScript.StartListening ("noFightTrack", noFightListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("fightTrack", fightListener);
		EventManagerScript.StopListening ("noFightTrack", noFightListener);
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
			if (audioSource.volume == 0f) {
				audioSource.volume = 0.5f;
			} else {
				audioSource.volume = 0f;
			}
		}
	}

	public void FightStarted(){
		if (!fightOnGoing) {
			currentlyPlaying = fightTracks [Random.Range (0, fightTracks.Length - 1)];
			audioSource.clip = currentlyPlaying;
			audioSource.Play ();
			fightOnGoing = true;
		}
	}

	public void FightStopped(){
		if (fightOnGoing) {
			currentlyPlaying = explorationTracks [Random.Range (0, explorationTracks.Length - 1)];
			audioSource.clip = currentlyPlaying;
			audioSource.Play ();
			fightOnGoing = false;
		}
	}

//	void FightNow(){
//		fightOnGoing = true;
//	}
//
//	void NoFight(){
//		fightOnGoing = false;
//	}
}
