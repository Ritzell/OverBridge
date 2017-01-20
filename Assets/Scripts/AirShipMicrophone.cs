using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipMicrophone : MonoBehaviour {

	void Start() 
	{
		StartCoroutine (OutPutMicrophone ());
	}

	private IEnumerator OutPutMicrophone(){
		while (true) {
			// 空の Audio Sourceを取得
			var audio = GetComponent<AudioSource> ();
			// Audio Source の Audio Clip をマイク入力に設定
			// 引数は、デバイス名（null ならデフォルト）、ループ、何秒取るか、サンプリング周波数
			audio.clip = Microphone.Start (null, false, (int)FindObjectOfType<ClockTower>().TimeLimitSeconds, 44100);
			// マイクが Ready になるまで待機（一瞬）
			while (Microphone.GetPosition (null) <= 0) {
			}
			// 再生開始（録った先から再生、スピーカーから出力するとハウリングします）
			audio.Play ();
			yield return null;
			while (audio.isPlaying) {
				yield return null;
			}
		}
	}
}
