using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Transform guessPosition;
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
    GameObject _wrapper;
    Entry _entry;
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

        _wrapper = new GameObject("Guess");
        _wrapper.transform.position = guessPosition.position;
        _wrapper.AddComponent<RendererGroup>();
    }

    public void PickGuess(Database database, string queryURL)
    {
        int index = Random.Range(0, database.getEntries().Count);
        int i = 0;
        foreach (Entry entry in database.getEntries())
        {
            if (i == index)
            {
                _entry = entry;
                Echo3DService.instance.DownloadAndInstantiate(_entry, queryURL, _wrapper);
                break;
            }
            i++;
        }

        _names = new List<string>();
        string allNames = ((ModelHologram)_entry.getHologram()).getFilename().Split('.')[0];
        Debug.Log(allNames);
        foreach (string name in allNames.Split(' ')) _names.Add(name.Replace('_', ' '));

        _properties = new List<string>();
        string value;
        if (_entry.getAdditionalData() != null && _entry.getAdditionalData().TryGetValue("guesswho", out value))
        {
            Debug.Log(value);
            foreach (string prop in value.Split(' ')) _properties.Add(prop.Replace('_', ' '));
        }

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

        _wrapper.GetComponent<RendererGroup>().Display();

        if (victory)
        {
            victoryMessage.gameObject.SetActive(true);
            victoryMessage.text = "Congratulations! The answer was: " + _names[0];
        }
        else
        {
            defeatMessage.gameObject.SetActive(true);
            defeatMessage.text = "Sorry! The answer was: " + _names[0];
        }
        if (_names.Count > 1)
        {
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
