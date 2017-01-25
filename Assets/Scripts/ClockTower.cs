using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ClockTower : MonoBehaviour {
    [SerializeField]
    private GameObject hand;
	[SerializeField]
	private Bell bell;
	[SerializeField]
	private float[] PlayBellTime;
    [SerializeField]
    public float TimeLimitSeconds = 10;

    private static DateTime StartTime;
    private static TimeSpan RestTime;
    private Coroutine timer;
	private int faze = 0;

    void Start()
    {
        StartTime = DateTime.Now;
        timer = StartCoroutine(Timer());
    }


    /// <summary>
    /// 制限時間の設定と残り時間を計算するメソッドの実行
    /// </summary>
    private IEnumerator Timer()
    {
        TimeSpan LimitTime = new TimeSpan(00, 0, (int)TimeLimitSeconds);
		while (!GameManager.IsGameOver)
        {
            DisplayTime(LimitTime);
            yield return null;
        }
    }


    /// <summary>
    /// GUITextに残り時間を表記する。
    /// </summary>
    /// <param name="Timetext">Timetext.</param>
    /// <param name="limitTime">Limit time.</param>
    private void DisplayTime(TimeSpan limitTime)
    {
        TimeCalculation(limitTime);
        //Timetext.text = TimeCastToString(RestTime);
    }

    /// <summary>
    /// 残り時間を計算
    /// </summary>
    private void TimeCalculation(TimeSpan limitTime)
    {
        TimeSpan elapsedTime = (TimeSpan)(DateTime.Now - StartTime);
        RestTime = limitTime - elapsedTime;
        RotateSecondsHand((360/TimeLimitSeconds) * (float)elapsedTime.Seconds);
		var time = (360 / TimeLimitSeconds) * (float)elapsedTime.Seconds;
		if (time >= PlayBellTime [faze]) {
			faze++;
			bell.PlayBell ();
		}
		if (time >= 360) {
			GameManager.ReStart (5f);
			StopCoroutine (timer);
		}
    }

    void RotateSecondsHand(float angle)
    {
		hand.transform.localEulerAngles = new Vector3(-180, 0, angle);
    }


}
