using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchWalls : MonoBehaviour
{
    [SerializeField]
    GameObject frontGlitch;
    [SerializeField]
    GameObject backGlitch;
    [SerializeField]
    GameObject fullGlitch;
    [SerializeField]
    GameObject sideGlitch;

    public void Inside(bool front, bool back, bool rightSide, bool leftSide)
    {
        frontGlitch.SetActive(front);
        backGlitch.SetActive(back);
        fullGlitch.SetActive(rightSide);
        sideGlitch.SetActive(leftSide);
    }
}



