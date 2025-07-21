using TMPro;
using UnityEngine;

public class ScoreUnity : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerTxt;

    private void Awake()
    {
        Score.OnWinnerChanged += UpdateWinnerText;
        winnerTxt.text = "Winner is " + Score.Winner; // set initial value
    }

    private void UpdateWinnerText(string newWinner)
    {
        winnerTxt.text = "Winner is " + newWinner;
        Debug.Log("{winner.txt} updated");
    }

    private void OnDestroy()
    {
        Score.OnWinnerChanged -= UpdateWinnerText; // cleanup to avoid memory leaks
    }
}
