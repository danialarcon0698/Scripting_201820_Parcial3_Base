using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private Text timerText;
    [SerializeField] GameController gameController;

    // Use this for initialization
    private void Start()
    {
        timerText = GetComponentInChildren<Text>();
        if (timerText == null)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (gameController.CurrentGameTime > 0)
            timerText.text = gameController.CurrentGameTime.ToString("00.000");
        else
            timerText.text = "00.000";
    }
}