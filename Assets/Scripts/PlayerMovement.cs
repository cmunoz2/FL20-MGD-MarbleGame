using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public bool phoneIsConnected = false;

    [Header("Movement")]
    public float jumpForce = 5f;
    public float dashForce = 10f;
    public float moveSpeed = 20f;

    [Header("Audio Clips")]
    public AudioClip calibrateClip;
    public AudioClip jumpClip;
    public AudioClip coinClip;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    
    [Header("Other Stuff")]
    public Button jumpButton;
    public Button dashButton;
    [Tooltip("Assign the Main Camera to This")]
    public GameObject mainCam;

    [HideInInspector]
    public Vector3 dir, startPosition, calibratedDir;

    // Private Variables
    Rigidbody rb;
    AudioSource aud;
    bool isGrounded = true;
    bool canJump = false;
    bool canDash = false;

    int score = 0;
    int coinScore = 250;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        aud = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        jumpButton.interactable = false;
        dashButton.interactable = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) ResetPlayer();
        if(this.transform.position.y < -2) ResetPlayer();
        if(!phoneIsConnected && Input.GetKeyDown(KeyCode.Space)) Jump();
        if(!phoneIsConnected && Input.GetKeyDown(KeyCode.LeftShift)) Dash();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dir = Vector3.zero;
        if(phoneIsConnected){
            dir.x = Input.acceleration.x - calibratedDir.x;
            dir.z = Input.acceleration.y - calibratedDir.z;
        } else {
            dir.x = Input.GetAxis("Horizontal");
            dir.z = Input.GetAxis("Vertical");
        }

        rb.AddForce(dir * moveSpeed);
    }

    public void ResetPlayer(){
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = startPosition;
    }

    public void CalibrateTilt() {
        Debug.Log("Calibrating Tilt");
        calibratedDir.x = Input.acceleration.x;
        calibratedDir.z = Input.acceleration.y;

        aud.PlayOneShot(calibrateClip);
    }

    public void Jump(){
        if(isGrounded && canJump){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            aud.PlayOneShot(jumpClip);
        }
    }

    public void Dash(){
        if(canDash){
            rb.AddForce(dir * dashForce, ForceMode.Impulse);
            aud.PlayOneShot(jumpClip);
        }
    }
    // DONT FORGET TO ADD PICKUP FOR DASH POWER,, 
    // DONT FORGET TO ADD COOLDOWN FOR DASH

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGrounded = true; 
            if(canJump) jumpButton.interactable = true;
            }
    }
    void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
            jumpButton.interactable = false;
            }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Coin")){
            aud.PlayOneShot(coinClip);
            score += coinScore;
            scoreText.text = "Score = " + score;
            Destroy(other.gameObject);
        }
        if(other.gameObject.name == "Jump Pickup"){
            canJump = true;
            aud.PlayOneShot(jumpClip);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("CustomCam")){
            mainCam.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(other.gameObject.name == "Dash Pickup"){
            canDash = true;
            dashButton.interactable = true;
            aud.PlayOneShot(jumpClip);
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit (Collider other){
        if(other.gameObject.CompareTag("CustomCam")){
            mainCam.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    /*
    
    */
}
