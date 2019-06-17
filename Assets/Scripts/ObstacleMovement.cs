using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour
{
    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -1f, 0) * speed * Time.deltaTime;

        if (transform.position.y <= -6f)
        {
          Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      Debug.Log("Hit!");
      SceneManager.LoadScene("GameOver");
    }
}
