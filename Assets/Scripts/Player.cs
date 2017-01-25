using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {
	[SerializeField]
	private float speed = 1;
	[SerializeField]
	private float BalanceScale = 1;

	private AudioSource source;
	private float slope = 0;
	[SerializeField]
	private Transform[] Hands = new Transform[2];

	void Start(){
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		if (transform.position.y <= 10) {
			GameManager.ReStart (0f);
		}
	}

	public IEnumerator StrongWind(float Sign,float time){
		source.panStereo = 0.8f * Sign;
		source.Play ();
		Coroutine balance = StartCoroutine(Balance(Sign));
		yield return new WaitForSeconds (time);
		StopCoroutine (balance);
		if (source.pitch >= 2f) {
			GameManager.IsGameOver = true;
			GetComponent<Rigidbody> ().AddForce (80 * Sign, 0, 0, ForceMode.Impulse);
		}
		source.pitch = 1;
		source.Stop ();
		yield return null;
	}

	private IEnumerator Balance(float WindSign){
		float BalanceSlope = 0;
		while (true) {
			if (!(Hands [0] == null || Hands [1] == null)) {
				BalanceSlope = SlopeInWorldSpace (Hands [0], Hands [1]);
			} else {
				BalanceSlope = 0;
			}
			source.pitch = Mathf.Clamp (source.pitch + (Time.deltaTime / 2) + ((BalanceSlope/WindSign) * BalanceScale *Time.deltaTime), 1, 2.5f);
			yield return null;
		}
	}
	/// <summary>
	/// return (a-b)
	/// </summary>
	/// <param name="a">The alpha component.</param>
	/// <param name="b">The blue component.</param>
	private float Distance(float a, float b){
		return a - b;
	}

	/// <summary>
	/// 左右どちらにどのくらい傾いてるかを返す
	/// 引数AとBは傾きが知りたいオブジェクトの両端の座標
	/// </summary>
	/// <returns>The in world space.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	private float SlopeInWorldSpace(Transform handA, Transform handB){
		//戻り値がマイナスならワールド軸の左への傾き、プラスなら右への傾きを示す
		//AからBに対するY軸距離 *　(BからAへのX軸方向(Bより右なら+1、左なら-1)　*　-　1(方向の反転))
		var distance = Distance (handA.position.y, handB.position.y);
		return distance * (Mathf.Sign(Distance(handA.position.x,handB.position.x)) * - 1);
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.layer == 11) {
			StopAllCoroutines ();
			source.pitch = 1;
			source.Stop ();
		}
	}


}
