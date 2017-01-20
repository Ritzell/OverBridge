using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class originBalloon : MonoBehaviour {
	[SerializeField]
	private Vector2 GravityModiferRange = new Vector2(1,1);
	[SerializeField]
	private Vector3 Acceleration = new Vector3(0,0,0);
	[SerializeField]
	private GameObject balloonEffect;
	[SerializeField]
	private float destroyTime = 0;

	float GravityModifier = 1;
	// Use this for initialization
	void Start () {
		GravityModifier = Random.Range (GravityModiferRange.x, GravityModiferRange.y);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().AddForce (new Vector3 (Acceleration.x, Acceleration.y+GravityModifier, Acceleration.z), ForceMode.Acceleration);
	}

	void OnCollisionEnter(Collision col){
		Instantiate (balloonEffect, col.contacts[0].point, Quaternion.identity);
	}

	private IEnumerator FadeOut(){
		yield return new WaitForSeconds (destroyTime);
		Destroy (gameObject);
	}
}
