using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBalanceParser : MonoBehaviour
{
    private const int LANGUAGE_CHAR_COUNT = 26;
    private const char FIRST_COLUMN_CHAR = 'A';

    [SerializeField] private TextAsset _gameBalanceSourceTable;

    private Dictionary<string, string> _gameBalance;

    private void Start()
    {
		ParseTable();
		GetText();

	}

    private void ParseTable()
    {
		_gameBalance = new Dictionary<string, string>();

		string[] tableLines = SplitByLines(_gameBalanceSourceTable.text);

		for (int i = 1; tableLines.Length > i; ++i)
        {
			string[] allColumnInCurrentLine = tableLines[i].Split(',');

			_gameBalance.Add(allColumnInCurrentLine[0], allColumnInCurrentLine[1]);
        }

	}

	private void GetText()
    {
		var tryGetText = _gameBalance.TryGetValue("id_armor_agility_multiplier", out var text);
    }

    private string[] SplitByLines(string text)
    {
        List<string> resultStrings = new List<string>();

        int startLineIndex = 0;
        string currentLine = "";

        for (int i = 0; i < text.Length - 1; i++)
        {
            if (text[i] == '\x0D' && text[i + 1] == '\x0A')
            {
                currentLine = text.Substring(startLineIndex, i - startLineIndex);
                resultStrings.Add(currentLine);
                i++;
                startLineIndex = i + 1;
            }
        }

        if (startLineIndex < text.Length)
        {
            resultStrings.Add(text.Substring(startLineIndex, text.Length - startLineIndex));
        }

        return resultStrings.ToArray();
    }


	private string GenerationColumnName(int columnNumber)
    {
        var sqrCharCount = LANGUAGE_CHAR_COUNT * LANGUAGE_CHAR_COUNT;
        var firstColumnChar = FIRST_COLUMN_CHAR - 1;

        int intFirstLetter = (columnNumber / sqrCharCount) + firstColumnChar;
        int intSecondLetter = ((columnNumber % sqrCharCount) / LANGUAGE_CHAR_COUNT) + firstColumnChar;
        int intThirdLetter = (columnNumber % LANGUAGE_CHAR_COUNT) + FIRST_COLUMN_CHAR;

        char FirstLetter = (intFirstLetter > firstColumnChar) ? (char)intFirstLetter : ' ';
        char SecondLetter = (intSecondLetter > firstColumnChar) ? (char)intSecondLetter : ' ';
        char ThirdLetter = (char)intThirdLetter;

        return string.Concat(FirstLetter, SecondLetter, ThirdLetter).Trim();
    }
}
