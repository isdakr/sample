using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float enemySpeed = -5;
    [SerializeField] private float enemyLifeTime;
    [SerializeField] private float horizontalRandomness;
    [SerializeField] private float directionChangeInterval;

    private Vector3 moveDirection;
    private float directionChangeTimer;

    private GameObject scoreCounterObject;
    private ScoreCounter scoreCounterScript;
    // Start is called before the first frame update
    void Start()
    {
        coreCounterObject = GameObject.Find("ScoreCounter");
        scoreCounterScript = scoreCounterObject.GetComponent<ScoreCounter>();

        float randomX = Random.Range(-horizontalRandomness, horizontalRandomness);
        moveDirection = new Vector3(randomX, 3f, 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        directionChangeTimer += Time.deltaTime;
        if (directionChangeTimer >= directionChangeInterval)
        {
            float randomX = Random.Range(-horizontalRandomness, horizontalRandomness);
            moveDirection = new Vector3(randomX, 3f, 0).normalized;
            directionChangeTimer = 0;
        }

        transform.Translate(moveDirection * enemySpeed * Time.deltaTime);

        enemyLifeTime -= Time.deltaTime;
        if (enemyLifeTime < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            scoreCounterScript.ScoreUp();
            Destroy(this.gameObject);
        }
    }
}
