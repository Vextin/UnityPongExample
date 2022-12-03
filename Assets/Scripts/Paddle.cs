using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Private/Protected variables cannot be edited or seen by other scripts, or other instances of this script.
    //SerializeField lets us edit the variables in the inspector even if they are private.
    [SerializeField] protected float moveSpeed = 1.0f;   //how fast the player moves in units per second.
    [SerializeField] protected float maxDistance = 3.0f; //the maximum distance the player can travel up or down.

    //Movement Amount is how much the player will move this frame:
    //Holding W results in a positive value - that means "up"
    //Holding S results in a negative value - that means "down"
    protected float movementAmount = 0;

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
}
