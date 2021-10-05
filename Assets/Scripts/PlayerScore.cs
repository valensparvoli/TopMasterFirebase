using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using UnityEngine.UI;
using System.Linq;


[Serializable]
public class User
{

    public string userName;
    public int userScore;

    public User()
    {
        userName = PlayerScore.playerName;
        userScore = PlayerScore.playerScore;
    }
}

public class PlayerScore : MonoBehaviour
{
    public User[] userList;
    public GameObject scorePanel;
    public GameObject linePrefab;
    public GameObject[] lines;
    public GameObject currentUserLine;

    public Transform newLinePosition;
    public int lineOffset;

    public Text scoreText;
    public InputField nameText;

    private System.Random random = new System.Random();

    User user = new User();

    public static int playerScore;
    public static string playerName;

    void Start()
    {
        //playerScore = random.Next(0, 101);
        scoreText.text = "Puntos: " + playerScore;
        CreateGrilla();
    }

    void CreateGrilla()
    {
        for(int i = 0; i < lines.Length; i++)
        {
            CreateNewLines(i);
            newLinePosition.Translate(0f, -lineOffset, 0f);
        }
    }

    public void CreateNewLines(int i)
    {
        GameObject newLine = Instantiate(linePrefab, newLinePosition.position, Quaternion.identity);
        newLine.transform.SetParent(scorePanel.transform);
        newLine.name = "User_" + i.ToString();
        lines[i] = newLine;
    }

    private void WriteAllLines()
    {
        for(int i=0;i < lines.Length; i++)
        {
            WriteUserLine(lines[i], userList[i]);
        }
    }

    private void WriteUserLine(GameObject userLine, User user)
    {
        userLine.transform.GetChild(0).GetComponent<Text>().text = user.userName.ToString();
        userLine.transform.GetChild(1).GetComponent<Text>().text = user.userScore.ToString();
    }

    private void UpdateScore()
    {
        scoreText.text = "Puntos: " + user.userScore;
    }

    private void PosToDataBase()
    {
        User user = new User();
        RestClient.Put("https://scrollingshooterfranvalen-default-rtdb.firebaseio.com/" + playerName + ".json", user);
    }

    public void OnSumbit()
    {
        playerName = nameText.text;
        PosToDataBase();
    }

    private void RetrieveFromDataBase()
    {
        RestClient.GetArray<User>("https://scrollingshooterfranvalen-default-rtdb.firebaseio.com/" + nameText.text + ".json").Then(response =>
        {
            userList = response;
            WriteAllLines();
        });
    }

    public void OnGetScore()
    {
        RetrieveFromDataBase();
        
    }

    public void OrderArray()
    {
        userList = userList.OrderByDescending(User => User.userScore).ToArray();
        WriteAllLines(); 
    }

    public void CreateDataBase()
    {
        for(int i=0;i < lines.Length; i++)
        {
            User user = new User
            {
                userName = "User" + i.ToString(),
                userScore = random.Next(0, 101)
            };
        }
        PostToDataBase(user);
    }
    private void PostToDataBase(User user)
    {
        RestClient.Put("https://scrollingshooterfranvalen-default-rtdb.firebaseio.com/" + user.userScore.ToString() + ".json", user);
    }
}
