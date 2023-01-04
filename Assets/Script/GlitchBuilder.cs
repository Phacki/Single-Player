using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchBuilder : MonoBehaviour
{
    [SerializeField]
    GlitchCreator glitchCreator;
    [SerializeField]
    GameObject glitchSectorObject;

    public float SectorSize = 1f;

    private void Start()
    {
        GlitchSector[,] glitch = glitchCreator.GetGlitch();

        for (int x = 0; x < glitchCreator.glitchWidth; x++)
        {
            for (int y = 0; y < glitchCreator.glitchHeight; y++)
            {
                GameObject newSector = Instantiate(glitchSectorObject, new Vector3((float)x * SectorSize, 0f, (float)y * SectorSize), Quaternion.identity, transform);

                GlitchWalls glitchWalls = newSector.GetComponent<GlitchWalls>();

                bool front = glitch[x, y].frontGW;
                bool leftSide = glitch[x, y].leftsideGW;
                bool rightSide = false;
                bool back = false;

                if (x == glitchCreator.glitchWidth - 1)
                    rightSide = true;
                if (y == 0)
                    back = true;

                glitchWalls.Inside(front, back, rightSide, leftSide);


            }
        }
    }

}