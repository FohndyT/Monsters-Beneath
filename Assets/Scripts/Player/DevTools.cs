using System;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]     // Permet a Curve d'acceder a RefreshCurveArray(), but also d'afficher splines dans éditeur. Laissez jusqu'à ce qu'on ship le jeu.
public class DevTools : MonoBehaviour
{
    Curve[] curves;
    InputsManager inputManager;
    Material mat;
    #region Attributes
    enum DebugFunctions { DebugDisplay, ShowHitboxes, ShowRaycasts, ShowTrajectories, UnlockNearPuzzle };
    KeyCode[] debugHotKeys = { KeyCode.F1, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8 };
    public bool[] debugStates { get; private set; }
    int nbDebugStates;
    int nbOfDrawLines = 10;
    #endregion

    private void Awake()
    {
        nbDebugStates = debugHotKeys.Length;
        debugStates = Enumerable.Repeat(true, nbDebugStates).ToArray();

        inputManager = GetComponent<InputsManager>();
        mat = new Material(Shader.Find("Hidden/Internal-Colored"));
        mat.hideFlags = HideFlags.HideAndDontSave;
    }
    private void Start()
    {
        RefreshCurveArray();
    }
    public void RefreshCurveArray()
    { if (debugStates[3]) { curves = FindObjectsOfType<Curve>(); } }

    void Update()
    {
        for (int i = 0; i < nbDebugStates; i++)
        {
            if (Input.GetKeyDown(debugHotKeys[i]))
            {
                debugStates[i] = !debugStates[i];
                Debug.Log(Enum.GetName(typeof(DebugFunctions), i) + " : " + debugStates[i]);
            }
        }
    }
    private void OnRenderObject()
    {
        if (debugStates[0])
        {
            mat.SetPass(0);
            GL.PushMatrix();
            if (debugStates[2] && inputManager.estATerre)
            {
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex(transform.position);
                GL.Vertex(inputManager.rayHit.point);
                GL.End();
            }
            if (debugStates[3] && curves.Length > 0)
            {
                foreach (var curve in curves)
                {
                    if (curve.isActiveAndEnabled)
                    {
                        GL.Begin(GL.LINE_STRIP);
                        GL.Color(Color.white);
                        for (int currentCurve = 0; currentCurve < curve.nbCurves; currentCurve++)
                        {
                            curve.CalculateCoefficients(currentCurve);
                            for (int i = 0; i <= nbOfDrawLines; i++)
                            {
                                Vector3 tempV = curve.curveType == Curve.CurveType.LinearSpline ? curve.GetPoint((float)i / nbOfDrawLines) + curve.points[currentCurve] :
                                                                                                  curve.GetPoint((float)i / nbOfDrawLines);
                                GL.Vertex(tempV);
                            }
                        }
                        GL.End();
                    }
                }
            }
            GL.PopMatrix();
        }
    }
}
