using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
		if (!GameManager.IsGameOver) {
			FindObjectOfType<Cracker> ().SoundCracker ();
			source.Play ();
			GameManager.ReStart (8f);
		}
    }
}
