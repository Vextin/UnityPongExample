using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : Paddle
{
    //Look at the line above - the class declaration.
    //it reads "public class Controls is-a Paddle.
    //That means all of the variables and functions 
    //in Paddle are also part of Controls. 
    //Read Paddle before we get started for extra context.
    //Note: the AIPaddle script is also a Paddle.
    
    //[SerializeField] tells unity to show this variable in the Inspector window.
    //bool - or "boolean" - means "true or false." 
    //if isLeftPlayer is false, that means this is the right-hand-side player.
    //If it's true, that means this is the left-hand-side player.
    [SerializeField] bool isLeftPlayer;

    // Update is called once per frame
    void Update()
    {
        //movementAmount needs to be reset every frame, or else we couldn't stop.
        movementAmount = 0;

        //If this is the left-hand-side player,
        if(isLeftPlayer)
            //Go to the HandleInput function, and tell it that "W" means Up, and "S" means Down.
            HandleInput(KeyCode.W, KeyCode.S);
        else
            //Otherwise, call HandleInput with "Up Arrow" means Up, and "Down Arrow" means Down.
            HandleInput(KeyCode.UpArrow, KeyCode.DownArrow);
        
        //All paddles can move. This is Paddle.Move() not PlayerPaddle.Move();
        //See Paddle.cs for the code.
        Move();
        
    }

    void HandleInput(KeyCode up, KeyCode down)
    {
        //Let's break this apart.
        //Input.GetKey() checks if the key is currently pressed.
        //MovementAmount is how much we should move this frame.
        //+=, or "plus-equals", is shorthand for "add these together and save the result."
        //that is to say, line 37 is the same as writing "movementAmount = movementAmount + moveSpeed * Time.deltaTime"
        //Time.deltaTime is the number of seconds that have passed since the last frame - usually a very small number, like 0.001
        //Multiplying our moveSpeed by the # of seconds since the last frame means the player
        //won't move faster if they have a faster computer.

        if(Input.GetKey(up))
        {
            movementAmount += moveSpeed;
        }
        
        if(Input.GetKey(down))
        {
            movementAmount -= moveSpeed;
        }
    }
}
