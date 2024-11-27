using UnityEngine;
using TMPro;

public class HighscoreEntry : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Score;

    public void UpdateValues(string name, int score)
    {
        Name.text = name;
        Score.text = score.ToString();
    }
}
