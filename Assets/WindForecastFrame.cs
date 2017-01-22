using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class WindForecastFrame : MonoBehaviour {
	/// <summary>
	/// time and power
	/// </summary>
	public static List<Vector2> Forecasts = new List<Vector2>();
	public Vector3[] ForecastPosition = new Vector3[4];
	public static List<GameObject> ForecastsObject = new List<GameObject>();

	[SerializeField]
	private Canvas canvas;

	[SerializeField]
	private GameObject ForecastPrefab;

	void Start(){
		for (int i = 0; i < 3; i++) {
			Add (i);
		}
	}

	// Update is called once per frame
	void Next () {
		Destroy (ForecastsObject [0]);
		for (int i = 0; i < 2; i++) {
			ForecastsObject [i].transform.position = ForecastPosition [i];
			ForecastsObject [i].GetComponent<WindForecast> ()._nextState.MoveNext();
		}
		Add (2);
	}

	void Add(int Number){
		GameObject forecast = (GameObject)Instantiate (ForecastPrefab, canvas.transform);//ForecastPrefab,ForecastPosition[Number],Quaternion.identity);
		WindForecast forecastScript = forecast.GetComponent<WindForecast> ();
		forecastScript.time = Forecasts [0].x;
		forecastScript.power = Forecasts [0].y;
		Forecasts.RemoveAt (0);
		forecast.GetComponent<RectTransform> ().anchoredPosition = ForecastPosition [Number];
		for (int i = 0; i < 3 - Number; i++) {
			//forecastScript._nextState.MoveNext ();
		}
	}
}
