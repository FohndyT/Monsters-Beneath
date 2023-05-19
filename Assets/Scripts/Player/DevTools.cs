//Jeremy Legault

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[ExecuteInEditMode]     // Permet a Curve d'acceder a RefreshCurveArray(), but also d'afficher splines dans éditeur. Laissez jusqu'à ce qu'on ship le jeu.
public class DevTools : MonoBehaviour
{
    InputsManager inputManager;
    Material mat;
    #region Attributes
    enum DebugFunctions { DebugFunctions, ShowHitboxes, ShowRaycasts, ShowTrajectories, GiveAllItemsToPlayer };
    KeyCode[] debugHotKeys = { KeyCode.F1, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8 };
    public bool[] debugStates { get; private set; }
    int nbDebugStates;
    int nbOfDrawLines = 10;
    Curve[] curves;
    public BoxCollider[] boxColles;
    #endregion

    private void Awake()
    {
        nbDebugStates = debugHotKeys.Length;
        //debugStates = Enumerable.Repeat(true, nbDebugStates).ToArray();
        debugStates = new bool[] { true, false, false, true, true };
        RefreshCurveArray();
        inputManager = GetComponent<InputsManager>();
        mat = new Material(Shader.Find("Hidden/Internal-Colored"));
        mat.hideFlags = HideFlags.HideAndDontSave;
    }
    public void RefreshCurveArray()
    {
        if (debugStates != null && debugStates[0] && debugStates[3])
            curves = FindObjectsOfType<Curve>().Where(x => x.isActiveAndEnabled).ToArray();
    }                                           // pour ne pas afficher les paths d'objets destroyed (génère Exception)

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
            if (debugStates[1])
            {
                boxColles = FindObjectsOfType<BoxCollider>();
                foreach (var box in boxColles)
                {
                    Vector3 a = box.bounds.min;
                    Vector3 b = new(box.bounds.max.x, box.bounds.min.y, box.bounds.min.z);
                    Vector3 c = new(box.bounds.max.x, box.bounds.max.y, box.bounds.min.z);  //            E
                    Vector3 d = new(box.bounds.min.x, box.bounds.max.y, box.bounds.min.z);  //       D        H(max)
                    Vector3 e = new(box.bounds.min.x, box.bounds.max.y, box.bounds.max.z);  //           CF
                    Vector3 f = new(box.bounds.min.x, box.bounds.min.y, box.bounds.max.z);  //  (min)A        G
                    Vector3 g = new(box.bounds.max.x, box.bounds.min.y, box.bounds.max.z);  //           B
                    Vector3 h = box.bounds.max;

                    GL.Begin(GL.LINE_STRIP);
                    GL.Color(Color.blue);
                    GL.Vertex(a); GL.Vertex(b); GL.Vertex(c); GL.Vertex(d); GL.Vertex(a);
                    GL.Vertex(f); GL.Vertex(g); GL.Vertex(h); GL.Vertex(e); GL.Vertex(f);
                    GL.End();
                    GL.Begin(GL.LINES);
                    GL.Color(Color.blue);
                    GL.Vertex(d); GL.Vertex(e);
                    GL.Vertex(c); GL.Vertex(h);
                    GL.Vertex(b); GL.Vertex(g);
                    GL.End();
                }
            }
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
            if (debugStates[4])     //GT Code
            {
                debugStates[4] = false;
                inputManager.acquiredSword = true;
                GameObject.Find("Player").GetComponent<Planage>().collectedGlider = true;
                Player player = GameObject.Find("Player").GetComponent<Player>();
                for (int i = 0; i < inputManager.Items.Length; i++)
                    player.AcquiredItem(i);
            }
        }
    }
}
