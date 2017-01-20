using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip : MonoBehaviour {
	[SerializeField]
	private float scale = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.GetComponent<Rigidbody> ().AddForce (Mathf.PerlinNoise(Time.time,Time.deltaTime) - 0.5f, 0, Mathf.PerlinNoise(Time.deltaTime,Time.time) - 0.5f,ForceMode.Acceleration);
	}
}
