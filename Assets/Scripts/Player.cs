using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {
	[SerializeField]
	private float speed = 1;

	private float lateOnWoodPosition = 0;
	private AudioSource source;

	private float slope = 0;

	void Start(){
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis ("Vertical") * Time.deltaTime * speed);
	}

	public IEnumerator StrongWind(float Sign,float time){
		source.panStereo = Sign;
		bool isStop = false;
		source.Play ();
		Coroutine balance = StartCoroutine(Balance(Sign));
		yield return new WaitForSeconds (time);
		StopCoroutine (balance);
		if (source.pitch >= 2f) {
			Debug.Log ("gameover");
			GameManager.IsGameOver = true;
			GetComponent<Rigidbody> ().AddForce (80 * Sign, 0, 0, ForceMode.VelocityChange);
		}
		source.pitch = 1;
		source.Stop ();
		yield return null;
	}

	private IEnumerator Balance(float Sign){
		while (true) {
			source.pitch = Mathf.Clamp (source.pitch + (Time.deltaTime / 2) - ((slope/Sign) * 3 * Time.deltaTime), 1, 2);
			yield return null;
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.layer == 11) {
			StopAllCoroutines ();
			source.pitch = 1;
			source.Stop ();
		}
	}


}
