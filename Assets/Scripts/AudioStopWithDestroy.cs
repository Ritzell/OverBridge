using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStopWithDestroy : MonoBehaviour {
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	void Update () {
		if (!source.isPlaying) {
			Destroy (gameObject);
		}
	}
}
