using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsButtonScript : MonoBehaviour
{
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
