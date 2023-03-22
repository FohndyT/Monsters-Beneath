using System;
using System.Linq;
using UnityEngine;

/*public class DebugAndGodMode : MonoBehaviour
{
    BezierCurve[] curves;
    InputsManager inputManager;
    //JumpingInput jumpScript;
    #region Attributes
    enum DebugFunctions { SwitchDebugOnOff, ShowHitboxes, ShowRaycasts, ShowTrajectories, UnlockPuzzle };
    KeyCode[] debugHotKeys = { KeyCode.F1, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8 };
    bool[] debugStates;
    int nbDebugStates;
    int nbOfDrawLines = 10;
    Material mat;
    #endregion
    private void Start()
    {
        nbDebugStates = debugHotKeys.Length;
        debugStates = Enumerable.Repeat(true, nbDebugStates).ToArray();
        curves = FindObjectsOfType<BezierCurve>();
        inputManager = GetComponent<InputsManager>();
        //jumpScript = GetComponent<JumpingInput>();
        mat = new Material(Shader.Find("Hidden/Internal-Colored"));
        mat.hideFlags = HideFlags.HideAndDontSave;
    }
    void SwitchOnOff(bool condition, ref bool state, string debugLog)
    {
        if (condition)
        {
            state = !state;
            Debug.Log(debugLog + state);
        }
    }
    void Update()
    {
        for (int i = 0; i < nbDebugStates; i++)
            SwitchOnOff(Input.GetKeyDown(debugHotKeys[i]), ref debugStates[i], Enum.GetName(typeof(DebugFunctions), i) + " : ");
    }
    private void OnRenderObject()
    {
        if (debugStates[0])
        {
            mat.SetPass(0);
            GL.PushMatrix();
            if (debugStates[2] && inputManager.isGrounded)    // jumpScript.estATerre
            {
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex(transform.position);
                GL.Vertex(inputManager.rayHit.point);    // transform.position + Vector3.down
                GL.End();
            }
            if (debugStates[3])
            {
                foreach (var curve in curves)
                {
                    nbOfDrawLines = curve.points.Length * 10;
                    GL.Begin(GL.LINE_STRIP);
                    GL.Color(Color.white);
                    for (int i = 0; i < nbOfDrawLines; i++)
                    { GL.Vertex(curve.GetPoint((float)i / nbOfDrawLines)); }
                    GL.End();
                }
            }
            GL.PopMatrix();
        }
    }
}
*/