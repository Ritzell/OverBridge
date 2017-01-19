using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squeaking : MonoBehaviour {
	private AudioSource source;

	[SerializeField]
	private AudioClip[] Squeakings;

	void Start(){
		source = GetComponent<AudioSource> ();
		source.clip = Squeakings[Mathf.FloorToInt(Random.Range(0.1f,2.9f))];
		source.pitch = Random.Range (0.9f, 1.1f);
		source.Play ();
	}


	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			Destroy (gameObject);
		}
	}
}
