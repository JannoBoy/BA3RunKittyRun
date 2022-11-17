using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCPrefab;
    //xz,xz
    private float[] Level1 = { -41.3f, 51.2f,48.6f, 72.3f };
    private float[] Level2 = { 51.13f, 48.6f, 71f, -38.7f };
    private float[] Level3 = { 48.4f, -41f, -39.3f, -72f };
    private float[] Level4 = { -41.2f,-37.5f, -72.5f, 23.9f };
    private float[] Level5 = { -37.1f, 26.3f, 24.2f, 46.2f };
    private float[] Level6 = { 26.5f, 24f, 45.4f, -17.7f };
    private float[] Level7 = { 23.6f, -20.5f, -21.2f, -36f };
    private float[] Level8 = { -23.4f, -17.7f, -37.1f, 5.6f };
    private int[] NumberOfNPCs = { 10, 15, 10, 10, 10, 10, 5, 5 };
    public int[] NumberOfChasers = { 0, 0, 0, 0, 1, 2, 3, 4 };
  

    void Start()
    {
        for(int i= 0; i< 8; i++)
        {
            int currentChasers = 0;
            float minX;
            float minY;
            float maxX;
            float maxY;
            switch (i)
            {
                case 0:
                    if (Level1[0] < Level1[2])
                    {
                        minX = Level1[0];
                        maxX = Level1[2];
                    }
                    else
                    {
                        minX = Level1[2];
                        maxX = Level1[0];
                    }
                    if (Level1[1] < Level1[3])
                    {
                        minY = Level1[1];
                        maxY = Level1[3];
                    }
                    else
                    {
                        minY = Level1[1];
                        maxY = Level1[3];
                    }
                    break;
                case 1:
                    if (Level2[0] < Level2[2])
                    {
                        minX = Level2[0];
                        maxX = Level2[2];
                    }
                    else
                    {
                        minX = Level2[2];
                        maxX = Level2[0];
                    }
                    if (Level2[1] < Level2[3])
                    {
                        minY = Level2[1];
                        maxY = Level2[3];
                    }
                    else
                    {
                        minY = Level2[1];
                        maxY = Level2[3];
                    }
                    break;
                case 2:
                    if (Level3[0] < Level3[2])
                    {
                        minX = Level3[0];
                        maxX = Level3[2];
                    }
                    else
                    {
                        minX = Level3[2];
                        maxX = Level3[0];
                    }
                    if (Level3[1] < Level3[3])
                    {
                        minY = Level3[1];
                        maxY = Level3[3];
                    }
                    else
                    {
                        minY = Level3[1];
                        maxY = Level3[3];
                    }
                    break;
                case 3:
                    if (Level4[0] < Level4[2])
                    {
                        minX = Level4[0];
                        maxX = Level4[2];
                    }
                    else
                    {
                        minX = Level4[2];
                        maxX = Level4[0];
                    }
                    if (Level4[1] < Level4[3])
                    {
                        minY = Level4[1];
                        maxY = Level4[3];
                    }
                    else
                    {
                        minY = Level4[1];
                        maxY = Level4[3];
                    }
                    break;
                case 4:
                    if (Level5[0] < Level5[2])
                    {
                        minX = Level5[0];
                        maxX = Level5[2];
                    }
                    else
                    {
                        minX = Level5[2];
                        maxX = Level5[0];
                    }
                    if (Level5[1] < Level5[3])
                    {
                        minY = Level5[1];
                        maxY = Level5[3];
                    }
                    else
                    {
                        minY = Level5[1];
                        maxY = Level5[3];
                    }
                    break;
                case 5:
                    if (Level6[0] < Level6[2])
                    {
                        minX = Level6[0];
                        maxX = Level6[2];
                    }
                    else
                    {
                        minX = Level6[2];
                        maxX = Level6[0];
                    }
                    if (Level6[1] < Level6[3])
                    {
                        minY = Level6[1];
                        maxY = Level6[3];
                    }
                    else
                    {
                        minY = Level6[1];
                        maxY = Level6[3];
                    }
                    break;
                case 6:
                    if (Level7[0] < Level7[2])
                    {
                        minX = Level7[0];
                        maxX = Level7[2];
                    }
                    else
                    {
                        minX = Level7[2];
                        maxX = Level7[0];
                    }
                    if (Level7[1] < Level7[3])
                    {
                        minY = Level7[1];
                        maxY = Level7[3];
                    }
                    else
                    {
                        minY = Level7[1];
                        maxY = Level7[3];
                    }
                    break;
                case 7:
                    if (Level8[0] < Level8[2])
                    {
                        minX = Level8[0];
                        maxX = Level8[2];
                    }
                    else
                    {
                        minX = Level8[2];
                        maxX = Level8[0];
                    }
                    if (Level8[1] < Level8[3])
                    {
                        minY = Level8[1];
                        maxY = Level8[3];
                    }
                    else
                    {
                        minY = Level8[1];
                        maxY = Level8[3];
                    }
                    break;
                default:
                    minX = 0f;
                    maxX = 0f;
                    minY = 0f;
                    maxY = 0f;
                    break;
            }
            
            for (int j=0; j<NumberOfNPCs[i]; j++)
            {
                GameObject GO = Instantiate(NPCPrefab);
                GO.transform.position = new Vector3(Random.Range(minX,maxX), 0, Random.Range(minY, maxY));
                GO.GetComponent<NPCScript>().Levelno = i + 1;
                Debug.Log("MinX: " + minX + "MaxX: " + maxX);
                GO.GetComponent<NPCScript>().setMinMaxValues(minX, maxX, minY, maxY);
            }
        }
    }

    void Update()
    {
        
    }
}
