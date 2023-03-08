using CodiceApp;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveCustomEditor : Editor
{
    private BezierCurve curve;
    private Transform handleTrans;
    private Quaternion handleRot;
    private int handleSelected = -1;

    private void OnSceneGUI()
    {
        curve = target as BezierCurve;
        handleTrans = curve.transform;
        handleRot = Tools.pivotRotation == PivotRotation.Local ? handleTrans.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1, 2f);
        Handles.DrawLine(p2, p3, 2f);
        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
    }

    private Vector3 ShowPoint(int i)
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
                Undo.RecordObject(curve, "Move Point");
                EditorUtility.SetDirty(curve);
                curve.points[i] = handleTrans.InverseTransformPoint(point);
            }
        }
        return point;
    }
}