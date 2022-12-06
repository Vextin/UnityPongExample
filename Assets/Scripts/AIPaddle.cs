using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : Paddle
{
    //how much the AI should lag behind in its decision making
    //Values over 1 will always intentionall miss

    [SerializeField][Range(-1f,1f)] float handicap = 0f;

    void Update ()
    {
        //Remember, the generic Paddle that this class is a child of contains movementAmount,
        //so we don't need to declare a new one, just set it to 0 before we use it. 
        movementAmount = 0;

        if(GameManager.GetBall().transform.position.y < transform.position.y - handicap)
        {
            movementAmount = -1f;
        }
        else if(GameManager.GetBall().transform.position.y > transform.position.y + handicap)
        {
            movementAmount = 1f;
        }

        //All paddles can move. This is Paddle.Move() not AIPaddle.Move();
        //See Paddle.cs for the code.
        Move();
    }
}
