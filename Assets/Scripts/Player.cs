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

	public IEnumerator StrongWind(float Sign,float time){
		source.panStereo = Sign;
		source.Play ();
		yield return new WaitForSeconds (time);
		source.Stop ();
		yield return null;
	}


}
