using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchCreator : MonoBehaviour

{

    [Range(5, 500)]
    public int glitchWidth = 5, glitchHeight = 5;
    public int defaultX, defaultY;
    GlitchSector[,] glitch;

    Vector2Int presentSector;

    public GlitchSector[,] GetGlitch()
    {
        glitch = new GlitchSector[glitchWidth, glitchHeight];

        for (int x = 0; x < glitchWidth; x++)
        {
            for (int y = 0; y < glitchHeight; y++)
            {
                glitch[x, y] = new GlitchSector(x, y);
            }
        }

        CreatePath(defaultX, defaultY);

        return glitch;
    }

    List<Direction> pathways = new List<Direction>
    { Direction.yUp, Direction.yDown, Direction.xUp, Direction.xDown };

    List<Direction> GetRNGPathways()
    {
        //Creates a copy of "pathways" list to alter
        List<Direction> pathwaysTemp = new List<Direction>(pathways);
        //Creates a copy of "pathways" list to insert RNG pathways into
        List<Direction> pathwaysRNG = new List<Direction>();
        //Creates a loop until RNG is fully utilized
        while (pathwaysTemp.Count > 0)
        {
            //Fetches a RNG index inside the list, adds random path to it
            //then removes it to perfect randomness and not allow it to be
            //selected twice.
            int RNG = Random.Range(0, pathwaysTemp.Count);
            pathwaysRNG.Add(pathwaysTemp[RNG]);
            pathwaysTemp.RemoveAt(RNG);
        }
        //When all paths are RNG, return list
        return pathwaysRNG;
    }

    bool CheckSectorCogent(int x, int y)
    {
        //Set Sectors deemed outside of radius or discovered as not cogent
        if (x < 0 || y < 0 || x > glitchWidth - 1 || y > glitchHeight - 1 || glitch[x, y].discovered) return false;
        else return true;
    }
    //Set adjacent sectors position to present sector temporarily
    Vector2Int CoordinateAdjacent()
    {
        List<Direction> pathRNG = GetRNGPathways();

        for (int i = 0; i < pathRNG.Count; i++)
        {
            Vector2Int adjacentSector = presentSector;

            switch (pathRNG[i])
            {
                case Direction.yUp:
                    adjacentSector.y++;
                    break;
                case Direction.yDown:
                    adjacentSector.y--;
                    break;
                case Direction.xUp:
                    adjacentSector.x++;
                    break;
                case Direction.xDown:
                    adjacentSector.x--;
                    break;

            }

            //If the recently checked adjacent is cogent. Return value
            //otherwise recheck.
            if (CheckSectorCogent(adjacentSector.x, adjacentSector.y)) return adjacentSector;
        }
        //Return present sector if all "paths" lack a cogent adjacent
        return presentSector;
    }

    //Issues itself a couple of "paths" and sets sectors relative
    void DeleteGlitch(Vector2Int mainSector, Vector2Int sideSector)
    {
        if (mainSector.x > sideSector.x)
        {
            glitch[mainSector.x, mainSector.y].leftsideGW = false;
        }
        else if (mainSector.x < sideSector.x)
        {
            glitch[sideSector.x, sideSector.y].leftsideGW = false;
        }
        else if (mainSector.y < sideSector.y)
        {
            glitch[mainSector.x, mainSector.y].frontGW = false;
        }
        else if (mainSector.x > sideSector.x)
        {
            glitch[sideSector.x, sideSector.y].frontGW = false;
        }
    }

    void CreatePath(int x, int y)
    {
        if (x < 0 || y < 0 || x > glitchWidth - 1 || y > glitchHeight - 1)
        {
            x = y = 0;

        }

        presentSector = new Vector2Int(x, y);

        List<Vector2Int> path = new List<Vector2Int>();

        bool close = false;
        while (!close)
        {
            Vector2Int nextSector = CoordinateAdjacent();

            if (nextSector == presentSector)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    presentSector = path[i];
                    path.RemoveAt(i);
                    nextSector = CoordinateAdjacent();

                    if (nextSector != presentSector) break;
                }

                if (nextSector == presentSector)
                    close = true;

            }
            else
            {
                DeleteGlitch(presentSector, nextSector);
                glitch[presentSector.x, presentSector.y].discovered = true;
                presentSector = nextSector;
                path.Add(presentSector);

            }
        }
    }
}

public class GlitchSector
{
    public bool discovered;
    public int x, y;
    public bool frontGW;
    public bool leftsideGW;

    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }

    }

    public GlitchSector(int x, int y)
    {
        this.x = x;
        this.y = y;

        discovered = false;

        frontGW = leftsideGW = true;
    }


}

public enum Direction
{
    yUp, yDown, xUp, xDown
}

