using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour {
	private AudioSource bell;
	void Start(){
		bell = GetComponent<AudioSource> ();
	}

	public void PlayBell(){
		bell.Play ();
	}
}
