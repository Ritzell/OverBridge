using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static bool IsGameOver = false;
    
    public float GetDegree(float x1, float x2, float y1, float y2)
    {
        float radian = Mathf.Atan2(y1 - y2, x1 - x2);
        return radian * Mathf.Rad2Deg;
    }

    public float AbsDistance(Vector3 p1, Vector3 p2)
    {
        return Mathf.Abs(Vector3.Distance(p1, p2));
    }

    public float AbsDistance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Abs(Vector2.Distance(p1, p2));
    }
    /// <summary>
    /// ベジェ曲線
    /// </summary>
    /// <param name="t">T.</param>
    /// <param name="p1">P1.</param>
    /// <param name="p2">P2.</param>
    /// <param name="p3">P3.</param>
    /// <param name="p4">P4.</param>
    public float Veje(float t, float p1, float p2, float p3, float p4)
    {
        float pos = (1 - t) * (1 - t) * (1 - t) * p1 + 3 * (1 - t) * (1 - t) * t * p2 + 3 * (1 - t) * t * t * p3 + t * t * t * p4;
        return pos;
    }

    /// <summary>
    /// 符号を維持した三平方の定理 
    /// 二次元ベクトルの計算の場合はベクトルごとに分けて計算
    /// </summary>
    /// <returns>The theorem.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    public float PythagoreanTheorem(float a, float b)
    {
        return a + b < 0 ? -(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)) : Mathf.Pow(a, 2) + Mathf.Pow(b, 2);
    }


    /// <summary>
    /// 符号を維持した平方根
    /// </summary>
    /// <param name="sqrt">Sqrt.</param>
    /// <param name="origin">Origin.</param>
    public float ImaginarySqrt(float c, float sign)
    {
        float sqrt = Mathf.Sqrt(Mathf.Abs(c));
        return sign == -1 ? -sqrt : sqrt;
    }
}

