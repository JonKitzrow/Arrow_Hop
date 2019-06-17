using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    float moveCharge, actualScaleX, actualScaleY, spawnTimeNext;
    public Transform arrow, arrowNext, player, obstacle;
    int direction, directionNext, track;
    public float chargeRate, spawnDelay;
    float[] tracks;
    bool[] tracksSpawn;

    // Start is called before the first frame update
    void Start()
    {
        moveCharge = 1f;
        actualScaleX = arrow.localScale.x;
        actualScaleY = arrow.localScale.y;
        tracks = new float[4];
        tracks[0] = -7f;
        tracks[1] = -5f;
        tracks[2] = -3f;
        tracks[3] = -1f;
        track = 1;
        direction = 1;
        directionNext = -1;
        tracksSpawn = new bool[4];
    }

    // Update is called once per frame
    void Update()
    {
        // move step
        if (Input.GetKeyDown("space") && moveCharge >= 1f)
        {
          // update track value
          if (track + direction >= 0 && track + direction <= 3) // skip if the resulting value is out of bounds (should be impossible under normal operating parameters)
          {
            track += direction;
            moveCharge = 0;
          }
          direction = directionNext;

          // Generate the next direction with 50% chance of left or right
          if (Random.Range(1, 3) == 1)
          {
            if (track + direction - 1 >= 0 && track + direction - 1 <= 3) // make sure this direction won't send the player off the tracks
            {
              directionNext = -1;
            }
            else
            {
              directionNext = 1;
            }
          }
          else
          {
            if (track + direction + 1 >= 0 && track + direction + 1 <= 3) // make sure this direction won't send the player off the tracks
            {
              directionNext = 1;
            }
            else
            {
              directionNext = -1;
            }
          }
        }

        //update player position
        player.position = new Vector3(tracks[track], -4f, 0);

        // charge next move
        if (moveCharge < 1)
        {
          moveCharge += chargeRate * Time.deltaTime;
        }
        else
        {
          moveCharge = 1;
        }

        // update UI readout
        arrow.localScale = new Vector3(actualScaleX * moveCharge, actualScaleY * moveCharge * -direction, 1);
        arrowNext.localScale = new Vector3(arrowNext.localScale.y, Mathf.Abs(arrowNext.localScale.y) * -directionNext, 1);

        // Instantiate obstacles
        if (Time.time >= spawnTimeNext)
        {
          spawnTimeNext = Time.time + spawnDelay;
          for (int i = 0; i < 3; i++)
          {
            tracksSpawn[Random.Range(0, 4)] = true;
          }
          for (int i = 0; i < 4; i++)
          {
            if (tracksSpawn[i])
            {
              Instantiate(obstacle, new Vector3(tracks[i], 6f, -2f), Quaternion.Euler(0, 0, 0));
            }
          }
          for (int i = 0; i < 4; i++)
          {
            tracksSpawn[i] = false;
          }
        }
    }
}
