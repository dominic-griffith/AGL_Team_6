using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject[] torches;
    [SerializeField] private GameObject closedDoor;
    [SerializeField] private GameObject openDoor;
    [HideInInspector] public int torchesLeft;

    public static GameManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        torchesLeft = torches.Length;
    }


    public void FindTorchesLeft() //recursive method
    {
        torchesLeft--;
        if (torchesLeft == 0) // Win Condition
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1);
        //open door sound
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }
}
