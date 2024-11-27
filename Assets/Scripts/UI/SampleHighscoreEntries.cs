using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SampleHighscoreEntries : MonoBehaviour
{
    public GameObject HighScoreEntryPrefab;
    public RectTransform HighscoresHolder;

    private void Start()
    {
        // add stuff when pressing enter
        var add = InputSystem.actions.FindAction("Jump");

        add.started += (_) => 
        { 
            GameObject entry = Instantiate(HighScoreEntryPrefab, HighscoresHolder);
            entry.GetComponent<HighscoreEntry>().UpdateValues("ssss", 5);

            // if some ui layout or content size fitter isnt working, use this
            //LayoutRebuilder.ForceRebuildLayoutImmediate(HighscoresHolder);
        };
    }
}
