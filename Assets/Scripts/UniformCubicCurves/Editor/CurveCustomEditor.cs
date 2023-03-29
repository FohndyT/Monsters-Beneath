using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Curve))]
public class CurveCustomEditor : Editor
{
    private Curve curve;
    #region Attributes
    Vector3 lastPt;
    int nbPtsPrior;
    //Vector3[] drawPoints = new Vector3[40];
    //private int nbOfDrawLines = 40;
    private Transform handleTrans;
    private Quaternion handleRot;
    private int handleSelected = -1;
    #endregion

    private void OnValidate()
    {
        if (nbPtsPrior == curve.points.Length)
            lastPt = curve.points[curve.points.Length - 1];
        nbPtsPrior = curve.points.Length;
        //nbOfDrawLines = curve.points.Length * 10;
        // drawPoints = new Vector3[nbOfDrawLines];
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        curve = target as Curve;
        if (curve.points.Length < curve.nbPtsMin)
            curve.AddPoint(lastPt);
        if (curve.curveType == Curve.CurveType.BezierCurve && curve.points.Length > curve.nbPtsMin)
            Array.Resize(ref curve.points, 4);
    }
    private void OnSceneGUI()
    {
        curve = target as Curve;
        handleTrans = curve.transform;
        handleRot = Tools.pivotRotation == PivotRotation.Local ? handleTrans.rotation : Quaternion.identity;

        Handles.color = Color.gray;
        switch (curve.curveType)
        {
            case Curve.CurveType.BezierCurve:
                Handles.DrawLine(handleTrans.TransformPoint(curve.points[0]), handleTrans.TransformPoint(curve.points[1]), 2f);
                Handles.DrawLine(handleTrans.TransformPoint(curve.points[2]), handleTrans.TransformPoint(curve.points[3]), 2f);
                //Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
                break;

            case Curve.CurveType.LinearSpline: break;

            default:
                Handles.DrawPolyLine(curve.points.Select(x => handleTrans.TransformPoint(x)).ToArray());
                //for (int k = 0; k < nbOfDrawLines; k++)
                //    drawPoints[k] = curve.GetPoint((float)j / nbOfDrawLines);
                //Handles.color = Color.white;
                //Handles.DrawPolyLine(drawPoints);
                break;
        }
        for (int i = 0; i < curve.points.Length; i++)
            DisplayControlPts(i);
    }

    private void DisplayControlPts(int i)
    {
        Vector3 point = handleTrans.TransformPoint(curve.points[i]);
        Handles.color = Color.red;
        if (i == 0) { Handles.color = Color.magenta; }
        if (Handles.Button(point, handleRot, HandleUtility.GetHandleSize(point) * 0.03f, 0.06f, Handles.DotHandleCap))
        {
            handleSelected = i;
            Repaint();
        }
        if (handleSelected == i)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRot);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(curve, "Move Curve Control Point");
                EditorUtility.SetDirty(curve);
                curve.points[i] = handleTrans.InverseTransformPoint(point);
            }
        }
    }
}
