using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreGameSettingsManager : MonoBehaviour
{
    [SerializeField] TMP_InputField _colsInput;
    [SerializeField] TMP_InputField _rowsInput;

    public void StartGame()
    {
        if (ValideInput())
        {
            MatchSettings boardSettings = Resources.Load<MatchSettings>("ScriptableObjects/BoardSettings");
            boardSettings.cols = int.Parse(_colsInput.text);
            boardSettings.rows = int.Parse(_rowsInput.text);
        }

        SceneManager.LoadScene(1);
    }

    private bool ValideInput()
    {
        string colls = _colsInput.text;
        string rows = _rowsInput.text;

        if (string.IsNullOrEmpty(colls) || string.IsNullOrEmpty(rows))
        {
            return false;
        }

        if(int.Parse(colls) <= 0 || int.Parse(rows) <= 0) 
        { 
            return false;
        }

        return true;
    }
}
