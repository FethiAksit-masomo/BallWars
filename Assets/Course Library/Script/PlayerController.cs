using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5.0f;
    public bool hasPowerup = false;
    private float powerupStreght = 150.0f;
    public GameObject powerInducator;

    public TextMeshProUGUI enemyCount;
    SpawnManager spawnManager;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        Movement();

        enemyCount.text = "Enemies:" + spawnManager.enemyCount.ToString();
    }

    private void Movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0, 0);
        playerRb.AddForce(movement * speed);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerInducator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -10)
        {
            RestartGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
            powerInducator.gameObject.SetActive(true);
        }
    }


    public void HomePage()
    {
        SceneManager.LoadScene("HomePage");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerInducator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody EnemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            EnemyRigidbody.AddForce(awayFromPlayer * powerupStreght, ForceMode.Impulse);
        }
    }
}
