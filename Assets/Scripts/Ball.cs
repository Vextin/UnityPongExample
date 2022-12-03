using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Our velocity. Not using Unity's rigidbody velocity because it overcomplicates things. Plus, this method is helpful to learn. 
    Vector2 velocity;
    //Attached to this object - use GetComponent in Start() to get a reference to it
    AudioSource audioSource;
    //In Pong, the ball inherits some of your paddle's velocity when they collide. This is the fraction of your velocity the ball should gain. 
    [SerializeField] float velocityInheritanceRatio = 1f;
    //How fast the ball should move.
    [SerializeField] private float speed = 4;

    //2 audio clips, one for hitting a wall and the other for hitting a paddle. Set these in the inspector. 
    [SerializeField] private AudioClip wallBounceAudio, paddleBounceAudio;



    //An enum lets us assign numbers to a set of things. Since we didn't specify, 
    //0 means left and 1 means right. We'll use this in Start();
    //Note: an enum for this is a little bit overkill, but it's a convinient place to teach you about them. 
    enum direction
    {
        left, right
    }
    
    //Start is called on the first frame.
    void Start()
    {
        //Find the RigidBody2D component attached to this object.
        //The angled brackets (< and >) mean we are giving GetComponent a "type," or the name
        //of a class (like how this class is called Ball). GameObject, Transform, Vector2, and Collision are also types/classes.
        //rigidBody = GetComponent<Rigidbody2D>();

        //Set the direction to 0 or 1 randomly.
        //the (direction) in parentheses means "turn this integer into its corresponding direction"
        //Random.Range() generates a number between 0.0 and 1.0, 
        //then Mathf.RoundToInt() rounds to the nearest whole number.
        direction dir = (direction) Mathf.RoundToInt(Random.Range(0f, 1f));

        //Generate a random vertical component to our starting velocity
        float startingVertical = Random.Range(0f, 1f);
        //startingHorizontal is 0 for now, we will set it in a moment.
        float startingHorizontal = 0;

        //If the ball is going to start by moving left, 
        if(dir == direction.left)
        {
            //its horizontal speed should be -1
            startingHorizontal = -1f;
        } else 
        if(dir == direction.right)
        {
            //Otherwise, its horizontal speed should be +1
            startingHorizontal = 1f;
        }

        //Create a velocity from our X and Y components
        Vector2 startingVelocity = new Vector2(startingHorizontal, startingVertical);

        //Normalize it - that is, the sum of all of its components should be 1
        //Another way to think about it: the ball should have the same starting speed
        //no matter what X and Y starting values we generated.
        startingVelocity = startingVelocity.normalized;

        //Multiply our direction by speed to get a velocity.
        velocity = startingVelocity * speed;

        //Lastly, get a reference to the AudioSource attached to the ball. This will let us play sound effects. 
        audioSource = GetComponent<AudioSource>();

    }


    //OnTriggerEnter is called whenever the ball bumps into something a collider.
    //See the docs: https://docs.unity3d.com/ScriptReference/Collider2D.OnTriggerEnter2D.html
    void OnTriggerEnter2D(Collider2D col)
    {
        //There are 3 things the ball COULD bump into: A paddle, a goal, or a wall.
        //All Paddle and Goal GameObjects should have their Tag set to "Paddle" or "Goal" respectively.
        //Look in the top left of the Inspector on those objects for the Tag.
        //If the ball hits a paddle,
        if(col.gameObject.tag == "Paddle")
        {
            //Play a sound effect. 
            audioSource.PlayOneShot(paddleBounceAudio);

            //The ball should change directions on the horizontal axis - that is, bounce off of the paddle.
            velocity.x = -velocity.x;

            //Get the Paddle component attached to the paddle object.
            Paddle paddle = (Paddle) col.gameObject.GetComponent<Paddle>();

            //To our velocity's vertical component, add some percentage of that paddle's current movement amount.
            //See: the PlayerPaddle script.
            velocity.y += paddle.GetMovementAmount() * velocityInheritanceRatio;

            //Normalize our velocity and multiply by speed - this makes sure the ball never starts going faster
            //by bouncing off of too many paddles and inheriting their velocity.
            velocity = velocity.normalized * speed;

        } else if(col.gameObject.tag == "Goal")
        {
            //Get a reference to the goal we hit. 
            Goal goal = col.gameObject.GetComponent<Goal>();

            //Tell the GameManager we scored a goal - ScoreGoal() requires that you give it 
            //the team that owns the goal, so we pass it the team of the goal we hit.
            //See: Goal script.
            GameManager.ScoreGoal(goal.GetTeam());

            //The ball deletes itself to avoid confusion.
            Destroy(gameObject);
        } else //it must be a top or bottom wall
        {
            //Play a sound effect.
            audioSource.PlayOneShot(wallBounceAudio);
            //Negate our vertical velocity to "bounce" off of the wall. 
            velocity.y = -velocity.y;
        }
    }

    //Update is called every frame. 
    void Update ()
    {
        //Translate means "change our position"
        //Translate takes in 3 parameters: X, Y, and Z
        //Along the X axis, we move x-velocity units per second.
        //For the "per second" part, we multiply by Time.deltaTime, which is the amount of time that has 
        //passed since the last frame.
        //Same for the Y axis. 
        //This is a 2D game, so we don't want to move on the Z axis. That stays 0.

        transform.Translate(velocity.x * Time.deltaTime, velocity.y * Time.deltaTime, 0);
    }
}
