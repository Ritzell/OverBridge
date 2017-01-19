using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracker : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	public void SoundCracker(){
		StartCoroutine (SoundCrackerInOrder ());
	}

	private IEnumerator SoundCrackerInOrder(){
		foreach(Transform child in transform){
			child.gameObject.SetActive (true);
			yield return new WaitForSeconds(Random.Range(0.05f,0.2f));
		}
	}
}
