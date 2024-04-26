using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    private StreamWriter csvWriter;
    public Camera topCam;
    public Camera sideCam;
    public float minScale = 0.2f;
    public float maxScale = 3.0f;
    
    void Start()
    {
        // Set the initial position randomly
        RandomizePosition();

       //string filePath = Application.dataPath + "/CubeData.csv";
        string filePath1 = Application.dataPath + "/testData.csv";
        csvWriter = new StreamWriter(filePath1);
        
        
    }

    void Update()
    {
        // Move the cube continuously within bounds
        float horizontalMovement = Mathf.Sin(Time.time) * moveSpeed * Time.deltaTime;
        float verticalMovement = Mathf.Cos(Time.time) * moveSpeed * Time.deltaTime;
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        float cubeMoveInScreen = Random.Range(-4f, 4f);
        transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), Random.Range(-4f, 4f));


        // Rotate the cube continuously on all axes
        float rotationX = rotationSpeed * Time.deltaTime;
        float rotationY = rotationSpeed * Time.deltaTime;
        float rotationZ = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationX, rotationY, rotationZ);
        string nextLine = "";


            // Generate random scale values
            float randomScaleX = Random.Range(minScale, maxScale);
            float randomScaleY = Random.Range(minScale, maxScale);
            float randomScaleZ = Random.Range(minScale, maxScale);

            // Apply random scale
            transform.localScale = new Vector3(randomScaleX, randomScaleY, randomScaleZ);

            nextLine += theVertices();
        // Print the current position
       // Debug.Log("Current Position: " + transform.position);
        Transform cube = this.transform;

        string cubeString = convertVectorToString(transform.position) + " , " + convertQuatToString(transform.rotation) + " , " + convertVectorToString(transform.localScale);

        csvWriter.WriteLine(nextLine + " , " + cubeString);


    }

    private string theVertices()
    {
        
        string points3D = "";
        string points2D = "";
        for (int i = 0; i < transform.childCount; i++)
        {
            print(i);
            Transform corner = transform.GetChild(i);
            points3D += convertVectorToString(corner.transform.position);
            Vector2 pixMain = Camera.main.WorldToViewportPoint(corner.transform.position);
            points2D += convertVectorToString(pixMain);
            Vector2 pixTop = topCam.WorldToViewportPoint(corner.transform.position);
            points2D += ","+ convertVectorToString(pixTop);
            Vector2 pixSide = sideCam.WorldToViewportPoint(corner.transform.position);
            points2D += ","+ convertVectorToString(pixSide);

            if (i < transform.childCount - 1)
            {
                points3D += " , ";
                points2D += " , ";
            }
        }

            return points2D +" , "+ points3D;
    }

    private string convertVectorToString(Vector3 v)
    {
        return v.x.ToString() + " , " + v.y.ToString() + " , " + v.z.ToString();
    }
    private string convertQuatToString(Quaternion v)
    {
        return v.x.ToString() + " , " + v.y.ToString() + " , " + v.z.ToString() + " , " + v.w.ToString();
    }

    private string convertVectorToString(Vector2 v)
    {
        return ( v.x.ToString() + " , " + v.y.ToString());
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
        float randomX = Random.Range(-0f, 15f);
        float randomY = Random.Range(-0f, 15f);
        float randomZ = Random.Range(-0f, 15f);
        transform.position = new Vector3(randomX, 0f, randomZ);

        // Print the initial position
        Debug.Log("Initial Position: " + transform.position);
    }
}
