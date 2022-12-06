using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : Paddle
{
    //how much the AI should lag behind in its decision making
    //Values over 1 will always intentionally miss
    [SerializeField][Range(0,1f)] float handicap = 0f;

    void Update ()
    {
        //Remember, the generic Paddle that this class is a child of contains movementAmount,
        //so we don't need to declare a new one, just set it to 0 before we use it. 
        movementAmount = 0;

        //This looks complicated, but it's not. 
        //GameManager.GetBall() just gets the ball object.
        //Then, we compare its position to the paddle's position.
        //If the ball is above the paddle, go up. If the paddle is above the ball, go down.
        //Handicap means the AI won't have perfect reaction time, and will wait until
        //the ball is significantly higher/lower than the AI before it reacts.
        if(GameManager.GetBall().transform.position.y < transform.position.y - handicap)
        {
            //Since AIPaddle is a Paddle, we use the paddle class' movementAmount to go up or down.
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
