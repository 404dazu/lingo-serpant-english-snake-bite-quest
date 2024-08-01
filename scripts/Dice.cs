using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{

    private Sprite[] diceSides;
    private SpriteRenderer rend;
    private int whosTurn = 1;
    private bool coroutineAllowed;
    public GameObject player1bgColor, player2bgColor;

    // for turn player
    public void changeTurn()
    {

        whosTurn *= -1;
        if (whosTurn == 1)
        {
            player1bgColor.gameObject.SetActive(true);
            player2bgColor.gameObject.SetActive(false);
        }
        if (whosTurn == -1)
        {
            player1bgColor.gameObject.SetActive(false);
            player2bgColor.gameObject.SetActive(true);
        }
    }

    // Use this for initialization
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
        rend.sprite = diceSides[5];

        coroutineAllowed = true;
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<GameControl>().gameState == GameState.BoardGame)
        {
            if (!FindObjectOfType<GameControl>().gameOver && coroutineAllowed)
                StartCoroutine("RollTheDice");
            else
            {
                StartCoroutine("RollTheDice");
            }
        }
    }

    private IEnumerator RollTheDice()
    {
        coroutineAllowed = false;
        int randomDiceSide = 0;
        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 6);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        FindObjectOfType<GameControl>().diceSideThrown = randomDiceSide + 1;
        if (whosTurn == 1)
        {
            FindObjectOfType<GameControl>().MovePlayer(1);
        }
        else if (whosTurn == -1)
        {
            FindObjectOfType<GameControl>().MovePlayer(2);
        }
        whosTurn *= -1; 
        yield return new WaitForSeconds(2f);
        coroutineAllowed = true;
        FindObjectOfType<GameControl>().changeState(GameState.Quiz);
    }
}