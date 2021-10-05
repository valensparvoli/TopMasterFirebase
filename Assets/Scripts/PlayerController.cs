using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float Speed;
    Vector2 MoveInput;
    Animator Animations;
    public static int ScoreValue = 0;
    public Text Score;

    // Start is called before the first frame update
    void Start()
    {
        Animations = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput.x = Input.GetAxis("Horizontal");
        MoveInput.y = Input.GetAxis("Vertical");

        transform.Translate(MoveInput * Time.deltaTime * Speed);

        Animations.SetBool("Moving", (Mathf.Abs (MoveInput.x) > 0 || Mathf.Abs(MoveInput.y) > 0));

        /*
        if (ScoreValue >= 8)
        {
            SceneManager.LoadScene("Winner");
            ScoreValue = 0;
        }
        */
        Score.text = ScoreValue.ToString();
    }
   
}
