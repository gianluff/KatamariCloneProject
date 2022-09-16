using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    public GameObject ball;
    public GameObject prefabCube;

    void Start()
    {
        //All'avvio dell'applicazione procedo allo spawn dei cubi in posizioni casuali all'interno del labirinto
        //La funzione CheckFreePosition si accerta che la posizione generata casualmente non sia occupata da un muro
        Vector3 spawnPos = new Vector3(0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            bool foundPosition = false;
            while (foundPosition == false)
            {
                spawnPos = new Vector3(Random.Range(-14.5f, 14.5f), 0.2f, Random.Range(-14.5f, 14.5f));
                foundPosition = CheckFreePosition(spawnPos);
            }
            Instantiate(prefabCube, spawnPos, Quaternion.identity);
        }
    }

    
    public void OnRestartButton()
    {
        //per comodità ho predisposto un bottone di restart che elimina i cubi presenti, ne spawna di nuovi e
        //riporta la sfera nella posizione di partenza
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject cube in cubes)
            GameObject.Destroy(cube);
        for (int i = 0; i < 20; i++)
        {
            Vector3 spawnPos = new Vector3(0, 0, 0);
            bool foundPosition = false;
            while (foundPosition == false)
            {
                spawnPos = new Vector3(Random.Range(-14.5f, 14.5f), 0.2f, Random.Range(-14.5f, 14.5f));
                foundPosition = CheckFreePosition(spawnPos);
            }
            Instantiate(prefabCube, spawnPos, Quaternion.identity);
        }
        ball.transform.position = new Vector3(0, 2, -17);
    }

    bool CheckFreePosition(Vector3 pos)
    {
        Collider[] intersecting = Physics.OverlapSphere(pos, 0.01f);
        if (intersecting.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
