using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SmallCubeScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    private StreamWriter csvWriter;
    public Camera topCam;
    public Camera sideCam;
    void Start()
    {
        // Set the initial position randomly
        RandomizePosition();

        string filePath = Application.dataPath + "/SmallCubeData.csv";
        csvWriter = new StreamWriter(filePath);
       // csvWriter.WriteLine("Object Name,Position X,Position Y Position,3D Corner Position, Cube centre, Cube Scale X, Cube Scale Y, Cube Scale Z, Cube Orientation");
    }

    void Update()
    {
        // Move the cube continuously within bounds
        float horizontalMovement = Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        transform.Translate(movement);

        // Rotate the cube continuously on all axes
        float rotationX = rotationSpeed * Time.deltaTime;
        float rotationY = rotationSpeed * Time.deltaTime;
        float rotationZ = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationX, rotationY, rotationZ);
        string nextLine = "";

        nextLine += theVertices();
        // Print the current position
       // Debug.Log("Current Position: " + transform.position);
        Transform cube = this.transform;

        string cubeString = convertVectorToString(transform.position) + " , " + convertVectorToString(transform.rotation.eulerAngles) + " , " + convertVectorToString(transform.localScale);

        csvWriter.WriteLine(nextLine + " , " + cubeString);


    }

    private string theVertices()
    {
        
        string points3D = "";
        string points2D = "";
        string pointsTop2D = "";
        string pointsSide2D = "";

        for (int i = 0; i < transform.childCount; i++)
        {
            print(i);
            Transform corner = transform.GetChild(i);
            points3D += convertVectorToString(corner.transform.position);
            Vector2 mainPix = Camera.main.WorldToScreenPoint(corner.transform.position);
            Vector2 topPix = topCam.WorldToScreenPoint(corner.transform.position);
            Vector2 sidePix = sideCam.WorldToScreenPoint(corner.transform.position);
            points2D += convertVectorToString(mainPix);
            pointsTop2D += convertVectorToString(topPix);
            pointsSide2D += convertVectorToString(sidePix);
            if (i < transform.childCount - 1)
            {
                points3D += " , ";
                points2D += " , ";
                pointsTop2D += " , ";
                pointsSide2D += " , ";
            }
        }

            return points2D + " , " +pointsTop2D + " , " + pointsSide2D + " , " + points3D;
    }

    private string convertVectorToString(Vector3 v)
    {
        return v.x.ToString() + " , " + v.y.ToString() + " , " + v.z.ToString();
    }


    private string convertVectorToString(Vector2 v)
    {
        return ((int) v.x).ToString() + " , " +( (int)v.y).ToString();
    }

    void OnDestroy()
    {
        // Close the CSV file when the script is destroyed
        if (csvWriter != null)
        {
            csvWriter.Close();
        }
    }

    void RandomizePosition()
    {
        // Set the cube's position to a random point within a certain range
        float randomX = Random.Range(-0f, 10f);
        float randomY = Random.Range(-0f, 10f);
        float randomZ = Random.Range(-0f, 10f);
        transform.position = new Vector3(randomX, 0f, randomZ);

        // Print the initial position
        Debug.Log("Initial Position: " + transform.position);
    }
}
