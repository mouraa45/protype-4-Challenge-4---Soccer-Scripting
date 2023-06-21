using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    private float speed;

    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");

        if (playerGoal == null)
        {
            Debug.LogError("Player Goal não atribuído ao EnemyX. Verifique se você tem um objeto de jogo chamado 'Player Goal' na sua cena.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGoal == null)
        {
            Debug.LogWarning("Player Goal não atribuído ao EnemyX.");
            return;
        }

        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - other.gameObject.transform.position;

            Debug.Log("Collided with " + other.gameObject.name + ", sending it away from the player.");

            enemyRigidbody.AddForce(awayFromPlayer * speed, ForceMode.Impulse);
        }
    }

    // Set the speed of the enemy
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}