using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Linq;

using TMPro;
using System.IO;
using System.Text.RegularExpressions;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private Text warning;

    [SerializeField]
    private TextAsset dictionaryFile;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private CanvasGroup gameOverScreen;

    [SerializeField]
    private string gameSceneName;

    private List<string> words;

    public string CurrentWord {
        get; private set;
    }

    void Start() {
        var textFile = Regex.Replace(dictionaryFile.text, @"\t|\n|\r", string.Empty).ToUpper().Trim();
        words = textFile.Split(',').ToList();

        GenerateNewWord();
    }

    public void FlashNotFullyFilled() {
        warning.text = "No blanks allowed";
        FlashWarning();
    }

    public void FlashInvalidWordWarn() {
        warning.text = "Not in dictionary";
        FlashWarning();
    }

    private void FlashWarning() {
        LeanTween.alphaText(warning.rectTransform, 1f, 0.5f).setOnComplete(HideWordWarn);

        #region Local_Function

        void HideWordWarn() {
            LeanTween.alphaText(warning.rectTransform, 0f, 2f);
        }

        #endregion
    }

    private void GenerateNewWord() {
        CurrentWord = words[Random.Range(0, words.Count)];
    }

    public bool IsValidWord(string word) {
        return words.Contains(word);
    }

    public void ShowGameOver(string message) {
        gameOverScreen.gameObject.SetActive(true);
        gameOverText.text = message;

        LeanTween.alphaCanvas(gameOverScreen, 1f, 1f).setOnComplete(() => {
            gameOverScreen.interactable = true;
        });
    }

    public void Restart() {
        SceneManager.LoadScene(gameSceneName);
    }
}
