using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class DebugGodMode : MonoBehaviour
{
    InputsManager inputManager;
    #region Attributes
    enum DebugFunctions { SwitchDebugOnOff, ShowHitboxes, ShowRaycasts, ShowTrajectories, UnlockPuzzle };
    KeyCode[] debugHotKeys = { KeyCode.F1, KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8 };
    bool[] debugStates;
    int nbDebugStates;
    Material mat;
    #endregion
    private void Start()
    {
        nbDebugStates = debugHotKeys.Length;
        debugStates = new bool[nbDebugStates];
        inputManager = GetComponent<InputsManager>();
        mat = new Material(Shader.Find("Hidden/Internal-Colored"));
        mat.hideFlags = HideFlags.HideAndDontSave;
    }
    void SwitchOnOff(bool condition, bool state, string debugLog)
    {
        if (condition)
        {
            state = !state;
            //switch (state)
            //{
            //    case true:
            //        state = false; break;
            //    case false:
            //        state = true; break;
            //}
            Debug.Log(debugLog + state);
        }
    }
    void Update()
    {
        for (int i = 0; i < nbDebugStates; i++)
            SwitchOnOff(Input.GetKeyDown(debugHotKeys[i]), debugStates[i], Enum.GetName(typeof(DebugFunctions), i) + " : ");
        if (debugStates[0])
        {
            //if (debugStates[1]) { }
            //if (debugStates[3]) { }
            //if (debugStates[4]) { }
        }
    }
    private void OnRenderObject()
    {
        if (inputManager.isGrounded)    //  debugStates[0] && debugStates[2] && inputManager.isGrounded
        {
            mat.SetPass(0);
            GL.PushMatrix(); GL.Begin(GL.LINES);
            GL.Color(Color.red);
            GL.Vertex(transform.position);
            GL.Vertex(inputManager.rayHit.point);
            GL.End(); GL.PopMatrix();
        }
    }
}
