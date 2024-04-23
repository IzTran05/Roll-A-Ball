using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;


public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    private int pickUpCount;
    private Timer timer;
    private bool gameOver = false;

    [Header("UI")]
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
    public GameObject gameOverScreen;
    public GameObject inGamePanel;





    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the Check Pickups Function
        CheckPickUps();
        gameOverScreen.SetActive(false);
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
    }
    private void Update()
    {
        timerText.text = "Timer" + timer.GetTime().ToString("F2");
    }

    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pick Up")
        {
            //Destroy the collided object
            Destroy(other.gameObject);
            //Decrement the pick up count
            pickUpCount--;
            //Run the Check Pick Ups function
            CheckPickUps();
        }
    }

    void CheckPickUps()
    {
        pickUpText.text = "Pick Ups Left: " + pickUpCount;
        if (pickUpCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //Set our game over to true
        gameOver = true;
        //Turn off our in game panel
        inGamePanel.SetActive(false);
        //Turn on our win panel
        gameOverScreen.SetActive(true);
        //Stop the timer
        timer.StopTimer();
        //Display our time to the win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");
        Debug.Log(timer.GetTime().ToString("F2"));
        pickUpText.color = Color.green;

        //Stop the ball from moving
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        timer.StopTimer();
        print("Yay! You Won. Your time was: " + timer.GetTime());
    }

    //Temporary - Remove when doing A2 modules 

    public void RestartGame()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}
