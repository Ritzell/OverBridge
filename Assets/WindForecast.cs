using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindForecast : MonoBehaviour {
	private float _time = 0;
	private float _power = 0;
	public IEnumerator _nextState;

	[SerializeField]
	private Text TimeText;
	[SerializeField]
	private Text PowerText;

	void Start(){
		_nextState = NextState ();
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
		TimeText.text = time.ToString ();
		Debug.Log (time.ToString ());
	}

	void DisplyPowerText(){
		PowerText.text = _power.ToString ();
	}

	public IEnumerator NextState(){
		GetComponent<Image> ().color = new Color (193 / 255, 188 / 255, 255 / 255, 50);
		yield return null;
		GetComponent<Image> ().color = new Color (193 / 255, 188 / 255, 255 / 255, 110);
		yield return null;
		GetComponent<Image> ().color = new Color (193 / 255, 188 / 255, 255 / 255, 170);
		yield return new WaitForSeconds (_time);
		Destroy (gameObject);
	}
}
