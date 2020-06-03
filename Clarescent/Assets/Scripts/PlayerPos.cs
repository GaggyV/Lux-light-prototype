using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private ReSpawner rs;

    void Start()
    {
        rs = GameObject.FindGameObjectWithTag("ReSpawner").GetComponent<ReSpawner>();
        transform.position = rs.lastCheckpointPos;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
