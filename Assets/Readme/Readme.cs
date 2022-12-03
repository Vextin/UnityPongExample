using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Readme))]
[CreateAssetMenu()]
public class Readme : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Visit https://www.youtube.com/@vextin for a step-by-step educational series covering this project and others. It may take a while to record and edit the video, so... coming soon.", MessageType.Info);
        EditorGUILayout.Space();
        GUILayoutOption[] options = {};
        
        
        GUIStyle header = new GUIStyle();
        header.fontSize = 18;
        header.fontStyle = FontStyle.Bold;
        header.richText = true;
        header.normal.textColor = Color.white;
        
        GUIStyle header2 = new GUIStyle();
        header2.fontSize = 16;
        header2.fontStyle = FontStyle.Normal;
        header2.richText = true;
        header2.normal.textColor = Color.white;

        GUIStyle regular = new GUIStyle();
        regular.fontSize = 14;
        regular.fontStyle = FontStyle.Normal;
        regular.richText = true;
        regular.wordWrap = true;
        regular.normal.textColor = Color.white;

        EditorGUILayout.LabelField("<color=#ffffff>Pong Example</color>", header, options);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "<color=#ffffff>    This example project is intended to be an easy starting point for complete-beginner Unity programmers.</color>", 
            regular, options);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "<color=#ffffff>    It is recommended to start with Scripts/PlayerPaddle.cs. This is the most basic script, and is friendly to beginner programmers.</color>", 
            regular, options);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(
            "<color=#ffffff>    Lines of code preceded by double-slashes (//) are comments, and tell you how the code works. Read them closely!</color>", 
            regular, options);
        EditorGUILayout.Space();    
        EditorGUILayout.LabelField( 
            "<color=#ffffff>    General information is contained here, or on the GitHub readme. \n->https://github.com/Vextin/UnityPongExample</color>", 
            regular, options);
        EditorGUILayout.Space(30f);
        EditorGUILayout.LabelField("<color=#ffffff>Project Structure</color>", header, options);
        EditorGUILayout.Space(10f);
        
        EditorGUILayout.LabelField("GameManager", header2, options);   
        EditorGUILayout.LabelField(" Stores information about the game state, like scores and number of players. Contains functions that can be called to score a goal or spawn a new ball.", regular, options);   
        EditorGUILayout.Space(30f);

        EditorGUILayout.LabelField("Paddle", header2, options);   
        EditorGUILayout.LabelField(" The base class for a Paddle. Stores info about itself that is generic and applicable to all paddles.", regular, options);   
        EditorGUILayout.Space(30f);

        EditorGUILayout.LabelField("PlayerPaddle", header2, options);   
        EditorGUILayout.LabelField(" A paddle controlled by the player. Has functions for moving the paddle with keyboard input.", regular, options);   
        EditorGUILayout.Space(30f);

        EditorGUILayout.LabelField("AIPaddle", header2, options);   
        EditorGUILayout.LabelField(" A paddle controlled by AI. Has functions that allow the paddle to control itself.", regular, options);   
        EditorGUILayout.Space(30f);

        EditorGUILayout.LabelField("Ball", header2, options);   
        EditorGUILayout.LabelField(" The Ball class handles all collisions in the game, producing sound effects, and telling the GameManager that a goal has been scored.", regular, options);   
        EditorGUILayout.Space(30f);

        EditorGUILayout.LabelField("Goal", header2, options);   
        EditorGUILayout.LabelField(" Stores the team that owns it, and returns that information to whoever asks for it.", regular, options);   
        EditorGUILayout.Space(30f);

    }
}
