/*using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public string apiKey;

    public GameObject panel;
    public GameObject galleryButton;
    public TMP_InputField input;
    public TextMeshProUGUI[] hintPanels;
    public TextMeshProUGUI victoryMessage;
    public TextMeshProUGUI defeatMessage;
    public TextMeshProUGUI extraMessage;

    public Color correctHint;
    public Color incorrectHint;

    public static GameManager Instance { get; private set; }

    List<string> _names;
    List<string> _properties;
    GameObject _holo;
    int _guesses = 3;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject holo = new GameObject("hologram");
        holo.transform.position = new Vector3(0, 0, 0);
        LoadGuess echo = holo.AddComponent<LoadGuess>();
        echo.APIKey = apiKey;
        echo.Tags = "guesswho";
    }

    public void Setup(List<string> names, List<string> properties, GameObject holo)
    {
        _names = names;
        _properties = properties;
        _holo = holo;

        panel.SetActive(true);
        input.gameObject.SetActive(true);
        galleryButton.SetActive(true);
    }

    public void Guess(string guess)
    {
        input.text = "";
        if (guess.Trim().Equals("")) return;
        if (_names.Contains(guess))
        {
            GameOver(true);
        }
        else
        {
            if (_guesses <= 0)
            {
                GameOver(false);
            }
            else if (_properties.Contains(guess))
            {
                HintReveal(true, guess);
            }
            else
            {
                HintReveal(false, guess);
            }

            _guesses--;
            EventSystem.current.SetSelectedGameObject(input.gameObject);
            input.ActivateInputField();
        }
    }

    public void HintReveal(bool correct, string guess)
    {
        TextMeshProUGUI hintPanel = hintPanels[hintPanels.Length - _guesses];
        hintPanel.color = correct ? correctHint : incorrectHint;
        hintPanel.SetText(guess);
    }

    public void GameOver(bool victory)
    {
        panel.SetActive(false);
        galleryButton.SetActive(false);
        input.gameObject.SetActive(false);

        _holo.GetComponent<GuessModel>().Reveal();

        if (victory)
        {
            victoryMessage.gameObject.SetActive(true);
            victoryMessage.text = "Congratulations! The answer was: " + _names[0];
        }
        else {
            defeatMessage.gameObject.SetActive(true);
            defeatMessage.text = "Sorry! The answer was: " + _names[0];
        }
        if (_names.Count > 1) {
            string msg = "AKA: ";
            for (int i = 1; i < _names.Count - 1; i++)
            {
                msg += _names[i] + ", ";
            }
            msg += _names[_names.Count - 1];
            extraMessage.gameObject.SetActive(true);
            extraMessage.text = msg;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
*/