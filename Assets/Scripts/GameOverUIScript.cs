using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour
{
    private GameManager gm;
    public TMP_Text finalScore;
    public Button restartButton;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        restartButton.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        finalScore.text = gm.points.ToString();
    }

    private void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Over");
    }
}