using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ClockTower : MonoBehaviour {
    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private float TimeLimitSeconds = 10;

    private static bool IsGameOver = false;
    private static DateTime StartTime;
    private static TimeSpan RestTime;
    private Coroutine timer; 

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
        TimeSpan LimitTime = new TimeSpan(00, (int)TimeLimitSeconds / 60, (int)TimeLimitSeconds % 60);
        while (!IsGameOver)
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
        if ((360 / TimeLimitSeconds) * (float)elapsedTime.Seconds >= 360)
        {
            GameManager.IsGameOver = true;
            StopCoroutine(timer);
        }
    }

    void RotateSecondsHand(float angle)
    {
		hand.transform.localEulerAngles = new Vector3(-180, 0, angle);
    }
}
