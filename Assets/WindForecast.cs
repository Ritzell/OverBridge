using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindForecast : MonoBehaviour {
	private float _time = 0;
	private float _power = 0;
	public IEnumerator _nextState;
	private WindForecastFrame frame;

	[SerializeField]
	private Text TimeText;
	[SerializeField]
	private Text PowerText;

	void Awake(){
		_nextState = NextState ();
	}
	void Start(){
		frame = FindObjectOfType<WindForecastFrame> ();
	}

	public float time{
		get{
			return _time;
		}set{
			_time = value;
			DisplayTimeText ();
		}
	}

	public float power{
		get{
			return _power;
		}set{
			_power = value;
			DisplyPowerText ();
		}
	}

	void DisplayTimeText(){
		var time = Mathf.CeilToInt(_time);
		TimeText.text = time.ToString () + "秒";
	}

	void DisplyPowerText(){
		PowerText.text = _power.ToString ();
	}

	public IEnumerator NextState(){
		StartCoroutine(ScaleUp(new Vector3 (0.8f, 0.8f, 0.8f)));
		GetComponent<Image> ().color = new Color (193f / 255f, 188f / 255f, 255 / 255, 50f / 255f);
		yield return null;
		StartCoroutine(ScaleUp(new Vector3 (0.9f, 0.9f, 0.9f)));
		GetComponent<Image> ().color = new Color (193f / 255f, 188f / 255f, 255 / 255, 110f / 255f);
		yield return null;
		StartCoroutine(ScaleUp(new Vector3 (1, 1, 1)));
		GetComponent<Image> ().color = new Color (193f / 255f, 188f / 255f, 255 / 255, 170f / 255f);
		StartCoroutine (destroy ());
	}

	private IEnumerator destroy(){
		yield return new WaitForSeconds (_time);
		frame.Next ();
		Destroy (gameObject);
	}

	public IEnumerator MoveToPoint(Vector3 point){
		RectTransform rect = GetComponent<RectTransform> ();
		Vector3 startPosition = rect.anchoredPosition;
		for (float i = 0; i < 1; i += Time.deltaTime * 2f) {
			rect.anchoredPosition = Vector3.Lerp (startPosition, point, i);
			yield return null;
		}
	}

	private IEnumerator ScaleUp(Vector3 scale){
		Vector3 startScale = transform.localScale;
		for (float i = 0; i < 1; i += Time.deltaTime * 2f) {
			transform.localScale = Vector3.Lerp (startScale, scale, i);
			yield return null;
		}
	}
}
