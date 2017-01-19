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
			GameManager.IsGameOver = true;
			FindObjectOfType<Cracker> ().SoundCracker ();
			source.Play ();
		}
    }    
}
