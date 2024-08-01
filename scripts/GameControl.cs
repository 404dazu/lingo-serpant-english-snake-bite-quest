using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour {
    public GameObject Quiz;
    public GameState gameState = GameState.Quiz;
    private GameObject whoWinsTextShadow, player1MoveText, player2MoveText;
    private string sceneToLoad;

    public string SceneToLoad { get => sceneToLoad; }

    private GameObject player1, player2;

    public int diceSideThrown = 0;
    public int player1StartWaypoint = 0;
    public int player2StartWaypoint = 0;

    public bool gameOver = false;

    // ini buat buka quis
    [SerializeField] GameObject gameLevelPanel;


    /*// Use for load scene
    public static void Load(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }*/
    // Use this for initialization
    void Start () {
        Quiz.SetActive(true);
        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<FollowThePath>().moveAllowed = false;
        player2.GetComponent<FollowThePath>().moveAllowed = false;

        whoWinsTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);

        //buat buka level menu
        gameLevelPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<FollowThePath>().waypointIndex > 
            player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(true);
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;

        }

        if (player2.GetComponent<FollowThePath>().waypointIndex >
            player2StartWaypoint + diceSideThrown)
        {
            player2.GetComponent<FollowThePath>().moveAllowed = false;
            player2MoveText.gameObject.SetActive(false);
            player1MoveText.gameObject.SetActive(true);
            player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex - 1;

        }

        if (player1.GetComponent<FollowThePath>().waypointIndex == 
            player1.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 1 Wins";
            gameLevelPanel.SetActive(true);
            gameOver = true;

            changeState(GameState.Win);
        }

        if (player2.GetComponent<FollowThePath>().waypointIndex ==
            player2.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(false);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 2 Wins";
            gameLevelPanel.SetActive(true);
            gameOver = true;

            changeState(GameState.Win);
        }
    }

    public void MovePlayer(int playerToMove)
    {
        switch (playerToMove) { 
            case 1:
                player1.GetComponent<FollowThePath>().moveAllowed = true;
                break;

            case 2:
                player2.GetComponent<FollowThePath>().moveAllowed = true;
                break;
        }
    }

    public void changeState(GameState newState)
    {
        gameState = newState;
        switch (gameState)
        {
            case GameState.Quiz:
                Quiz.SetActive(true);
                break;
            case GameState.BoardGame:
                Quiz.SetActive(false);
                break;
            case GameState.Win:
                Quiz.SetActive(false);
                break;
            default:
                break;
        }
    }
}
public enum GameState
{
    Quiz, BoardGame, Win
}

