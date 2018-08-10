using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier  {

	/// <summary>
	/// quadratic Bezier curve
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="p0">P0.</param>
	/// <param name="p1">P1.</param>
	/// <param name="p2">P2.</param>
	/// <param name="t">T.</param>
	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {

        t = Mathf.Clamp01(t);
        var oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * p0 +
            2f * oneMinusT * t * p1 + t * t * p2;
        // 上面的公式由下面推导而出
        /*
        var point1 = Vector3.Lerp(p0, p1, t);
        var point2 = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(point1, point2, t);
        */
    }

	/// <summary>
	/// cubic Bezier curve
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="p0">P0.</param>
	/// <param name="p1">P1.</param>
	/// <param name="p2">P2.</param>
	/// <param name="p3">P3.</param>
	/// <param name="t">T.</param>
	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{

		t = Mathf.Clamp01(t);

		var oneMinusT = 1f - t;

        return oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
		// 上面的公式由下面推导而出
		/*
        var point1 = Vector3.Lerp(p0, p1, t);
        var point2 = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(point1, point2, t);
        */
	}


    /// <summary>
    /// 对上面的公式取导 取导即某点的斜率k
    /// </summary>
    /// <returns>The first derivative.</returns>
    /// <param name="p0">P0.</param>
    /// <param name="p1">P1.</param>
    /// <param name="p2">P2.</param>
    /// <param name="t">T.</param>
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
    }

	/// <summary>
	/// 对上面的公式取导 取导即某点的斜率k
	/// </summary>
	/// <returns>The first derivative.</returns>
	/// <param name="p0">P0.</param>
	/// <param name="p1">P1.</param>
	/// <param name="p2">P2.</param>
	/// <param name="t">T.</param>
    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2,Vector3 p3, float t)
	{
		t = Mathf.Clamp01(t);
		float oneMinusT = 1f - t;
		return
			3f * oneMinusT * oneMinusT * (p1 - p0) +
			6f * oneMinusT * t * (p2 - p1) +
			3f * t * t * (p3 - p2);
	}
}


public enum BezierControlPointMode{

    Free,
    Aligned,
    Mirrored
}