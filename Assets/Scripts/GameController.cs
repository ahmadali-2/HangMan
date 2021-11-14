using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    private string[] names = File.ReadAllLines(@"Assets/Words/words.txt");
    public Text timer;
    private float time;
    public Text wordToFind;
    private string randomName;
    private int charLength = 0;
    StringBuilder sb;
    public GameObject[] gameObjects;
    public GameObject winText;
    public GameObject loseText;
    private int bodyCheck = 0;
    private bool gameEnd = false;
    public GameObject replayButton;
    // Start is called before the first frame update
    void Start()
    {
        string testStr = "";
        randomName = names[Random.Range(0, names.Length)];
        for (int i = 0; i < randomName.Length; i++)
        {
            char letter = randomName[i];
            if (char.IsWhiteSpace(letter))
            {
                testStr += " ";
            } 
            else
            {
                testStr += "_";
            }
        }

        wordToFind.text = AddSpaces(testStr);
        sb = new StringBuilder(testStr);

    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnd!=true)
        time += Time.deltaTime;
        timer.text = ""+time;
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if(e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            if(charLength < randomName.Length)
            {
                char chr = randomName[charLength];
                if(char.Equals(' ', chr) && (charLength < randomName.Length))
                {
                    charLength++;
                }
                if(char.IsLetter((char)e.keyCode))
                {
                    
                    if (chr == ((char)e.keyCode))
                    {
                        sb[charLength] = ((char)e.keyCode);
                        wordToFind.text = AddSpaces(sb.ToString());
                        if (charLength == randomName.Length - 1)
                        {
                            gameEnd = true;
                            Debug.Log("Congrats, You guessed correctly!");
                            winText.SetActive(true);
                            replayButton.SetActive(true);
                        }
                        else
                        {
                            charLength++;
                        }
                    }
                    else
                    {
                        Debug.Log("InCorrect!" + e.keyCode.ToString());
                        gameObjects[bodyCheck].SetActive(true);
                        bodyCheck++;
                        if(bodyCheck >= gameObjects.Length)
                        {
                            gameEnd = true;
                            Debug.Log("Game Over, You Lost!");
                            loseText.SetActive(true);
                            replayButton.SetActive(true);
                        }
                    }
                }
                else
                {
                    Debug.Log("Please guess a letter only!");
                }

            }
        }
    }

    public string AddSpaces(string s)
    {
        string newStr = "";
        for(int i = 0; i < s.Length; i++)
        {
            if(i < s.Length)
            newStr += s[i]+" ";
        }

        return newStr;
    }
    public string RemoveSpaces(string s)
    {
        string newStr = "";
        for (int i = 0; i < s.Length; i++)
        {
            if (i < s.Length && s[i] != ' ')
                newStr += s[i];
        }

        return newStr;
    }
}
