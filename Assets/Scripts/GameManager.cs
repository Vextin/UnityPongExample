using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    //The GameManager keeps track of a bunch of information about the game state. 
    //It uses a lot of Static variables. Let's discuss those briefly. 
    //In our other scripts, variables are not static. That means each instance of the script
    //has its own version of each variable, and their values can be different. 
    //Static variables are *shared* by every instance of a script. 
    //Static functions can only use static variables. 
    //using Statics makes our life easier, though: 
    //instead of using FindObjectOfType<>() or GetComponent<>() to find an instance of the
    //GameManager, we can refer to the class as a whole - 
    //FindObjectOfType<GameManager>().ScoreGoal(0) 
    //becomes
    //GameManager.ScoreGoal


    //A reference to the currently-in-play ball, for the AI to use. 
    static GameObject ball;

    //how many points the team on the left has
    static int teamLeftPoints = 0;
    //how many points the team on the right has
    static int teamRightPoints = 0;
    //The paddles corresponding to Player 1 (left) and Player 2 (right)
    static Paddle player1, player2;
    static Vector3 player1StartPos, player2StartPos;

    //The prefabricated Ball GameObject. Note that the static variable "prefab" a few lines down
    //gets its value from this, non-static, inspector-assignable variable. 
    //A downside of using statics is that they can't be assigned in the inspector. 
    [SerializeField] GameObject ballPrefab;

    //A reference to the score text - notice we added "using TMPro" to the top of the script
    static TextMeshProUGUI scoreText;

    //A reference to the particle system on the 
    static ParticleSystem particleSystem;

    //The static version of that ballPrefab variable.
    static GameObject prefab;

    //if false, the right-hand paddle should use AI. If true, it should be player-controlled. 
    public static bool playerTwoIsHuman = false;

    //When a ball enters a goal, this function should be called.
    public static void ScoreGoal (int team)
    {
        switch(team)
        {
            //If the left team's goal was scored on, give the right team points
            case 0: teamRightPoints++; break;
            //and vice-versa
            case 1: teamLeftPoints++; break;
            //If the team was less than 0 or greater than 1... let's assume that was an accident and do nothing. 
            default: return;
        }

        //Set the score text. + indicates we are appending the thing on the right to the thing on the left.
        scoreText.text = teamLeftPoints + " - " + teamRightPoints;

        //Play a burst of particles from the particle system.
        particleSystem.Play();

        //The ball is out of play now, so spawn another one. 
        SpawnNewBall();
    }

    static void SpawnNewBall()
    {
        if(ball != null)
            Destroy(ball);
        //Instantiate creates a clone of a GameObject.
        //The thing we clone is prefab (the ball)
        //The new object's position is (0,0,0) or the center of the screen.
        //The new object's rotation is the Quaternion Identity Vector . Basically means "not rotated".
        //You can think of setting rotation to Quaternion.identity as setting scale to (1, 1, 1). It's not zero, it's just "normal".
        //We store a reference to the new ball in the ball variable.
        ball = Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    void Awake()
    {
        //set the static version of the prefab to the one we set in the inspector.
        prefab = ballPrefab;
        //Get a reference to the score text
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        //And look in the scoreText's parent object for a child with a particle system component. 
        particleSystem = scoreText.transform.parent.GetComponentInChildren<ParticleSystem>();

        //Get references to both of the paddles.
        player1 = GameObject.FindObjectOfType<PlayerPaddle>();
        player2 = GameObject.FindObjectOfType<AIPaddle>();

        //And keep note of where the players started. We could hard-code these numbers,
        //but that makes it harder to change the level design later.
        player1StartPos = player1.transform.position;
        player2StartPos = player2.transform.position;

        SpawnNewBall();
    }

    void Update()
    {
        //The following code handles switching between AI and Human for Player 2.
        //Note: ! (exclamation) means "not". So this if statement reads:
        //"If player two is NOT human"
        if(!playerTwoIsHuman)
        {
            //If player 2 is AI, and the player hits "Enter" on the keyboard,
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //tell the AI paddle to swap its control method.
                player2 = player2.SwapControlMethod();
                //Restart the game.
                RestartGame();
            }
        }else
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                player2 = player2.SwapControlMethod();
                RestartGame();
            }
        }    

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
    public static void RestartGame()
    {
        ResetScoreboard();
        SpawnNewBall();

        //Reset player locations.
        player1.transform.position = player1StartPos;
        player2.transform.position = player2StartPos;
    }

    //Just gives whoever called it a reference to the ball object.
    public static GameObject GetBall(){ return ball; }
    static void ResetScoreboard()
    {
        teamLeftPoints = 0;
        teamRightPoints = 0;
        scoreText.text = "0 - 0";
    }
}
