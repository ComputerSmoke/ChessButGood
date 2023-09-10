using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controls
{
     public enum Key {
        LMB,
        RMB
    }
    public static string[] keyBindings;

    public static void BindKeys ()
    {
        keyBindings = new string[2];
        keyBindings[(int)Key.LMB] = "mouse 0";
        keyBindings[(int)Key.RMB] = "mouse 1";
    }
    public static bool KeyPressed(Key key) {
        return Input.GetKey(keyBindings[(int)key]);
    }
    public static bool KeyDown(Key key) {
        return Input.GetKeyDown(keyBindings[(int)key]);
    }
    public static Vector3 MousePos() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(pos.x, pos.y, 0);
    }
    public static float MouseScrollDelta() { return Input.mouseScrollDelta.y;}
}
