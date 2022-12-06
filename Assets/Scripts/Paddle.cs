using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Private/Protected variables cannot be edited or seen by other scripts, or other instances of this script.
    //SerializeField lets us edit the variables in the inspector even if they are private.
    [SerializeField] protected float moveSpeed = 2.0f;   //how fast the player moves in units per second.
    [SerializeField] protected float maxDistance = 3.0f; //the maximum distance the player can travel up or down.

    //Movement Amount is how much the player will move this frame:
    //Holding W results in a positive value - that means "up"
    //Holding S results in a negative value - that means "down"
    protected float movementAmount = 0;

    //movementAmount is protected, which means other scripts can't read or change it.
    //We do want other scripts to know our movement, though.
    //A public "getter" function lets other scripts ask for, but not change, the value of movementAmount.
    public float GetMovementAmount()
    {
        return movementAmount;
    }

    protected void Move()
    {
        //get our y- (vertical) position
        //(transform.position is the position of whatever GameObject this script is attached to)
        //"float" is short for "floating-point number". It's how computers store numbers with fractional components, like 1.5 or -0.2
        float position = transform.position.y;

        //Mathf.Clamp() takes in three arguments:
        //The first is the value we're clamping.
        //The second is the minimum allowed value.
        //the third is the maximum allowed value.
        //it returns (gives us back) the clamped value, which we store in the variable "position".
        position = Mathf.Clamp(position + movementAmount * Time.deltaTime, -maxDistance, maxDistance);

        //A Vector3 is an X, Y, and Z. We tell the X and Z to stay as they were, but
        //We replace our Y position with the one we just calculated. 
        transform.position = new Vector3(transform.position.x, position, transform.position.z);
    }


    //Swaps the control method between AI-controlled and Player-controlled.
    public virtual Paddle SwapControlMethod()
    {
        //declare a new paddle. It doesn't have an actual paddle yet, we're just "promising"
        //that we will create one.
        Paddle newPaddle;
        
        if(GameManager.playerTwoIsHuman)
        {
            //If the player was human, make an AI paddle.
            newPaddle = gameObject.AddComponent<AIPaddle>();
        } else
        {
            //if the player was AI, make a player paddle.
            newPaddle = gameObject.AddComponent<PlayerPaddle>();
        }

        //exclamation (!) means "not". This statement swaps the value of playerTwoIsHuman.
        GameManager.playerTwoIsHuman = !GameManager.playerTwoIsHuman;

        //Removes this component from the object
        //BUT doesn't actually delete it until the end of the frame, so we can still return the new paddle component.
        Destroy(this);

        //Give the new paddle back to the game manager, or whoever else called the function.
        return newPaddle;
    }
}
