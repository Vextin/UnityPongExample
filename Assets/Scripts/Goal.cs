using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //0 for left, 1 for right
    [SerializeField] private int team;
    //team is private, so other scripts can't see or change it.
    //We do want Ball.cs to be able to ask what team this goal is for, though, so we can
    //have a GetTeam() function return the team. 
    public int GetTeam() {return team;}


}
