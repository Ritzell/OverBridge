using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField]
	private float speed = 1;

	private float lateOnWoodPosition = 0;
	private AudioSource source;

	void Start(){
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis ("Vertical") * Time.deltaTime * speed);
	}

	void OnCollisionStay(Collision col){
		if (col.gameObject.name == "Wood" && col.contacts[0].point.z - lateOnWoodPosition > 1f + Random.Range(-0.5f,0.5f) ) {
			source.Play ();
			lateOnWoodPosition = col.contacts [0].point.z;
		}
	}
}
