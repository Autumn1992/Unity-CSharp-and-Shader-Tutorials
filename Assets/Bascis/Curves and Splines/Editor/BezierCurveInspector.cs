﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

    private BezierCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    public int lineSteps = 20;

    private void OnSceneGUI(){

        curve = target as BezierCurve;

        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                              handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p2, p3);


        //Handles.color = Color.white;
        //Vector3 lineStart = curve.GetPoint(0f);
        //Handles.color = Color.green;
        //Handles.DrawLine(lineStart, lineStart +curve.GetDirection(0f));

        //for (var i = 1; i <= lineSteps; i++)
        //{
        //    Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);

        //    Handles.color = Color.white;
        //    Handles.DrawLine(lineStart, lineEnd);

        //    //draw tangent
        //    Handles.color = Color.green;
        //    Handles.DrawLine(lineEnd, lineEnd+curve.GetDirection(i/(float)lineSteps));

        //    lineStart = lineEnd;
        //}

        ShowDirections();
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
    }

    float directionScale = 1f;
    void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = curve.GetPoint(0f);
        Handles.DrawLine(point, point+curve.GetDirection(0f) * directionScale);

        for (var i = 1; i < lineSteps; i++)
        {

            point = curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + curve.GetDirection(i / (float)lineSteps)*directionScale);
        }

    }

    private Vector3 ShowPoint(int index){

        Vector3 point = handleTransform.TransformPoint(curve.points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if(EditorGUI.EndChangeCheck()){

            Undo.RecordObject(curve, "Move Point");
			EditorUtility.SetDirty(curve);
            curve.points[index] = handleTransform.InverseTransformPoint(point);
        }

        return point;
    }
}
