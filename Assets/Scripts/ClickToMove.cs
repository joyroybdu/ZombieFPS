using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Make sure this is included!

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent; // Fixed typo here

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); // Fixed type name
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
            RaycastHit hit; // Variable to store information about what the ray hit

            if (Physics.Raycast(ray, out hit)) // Perform the raycast
            {
                navAgent.SetDestination(hit.point); // Move the character to the point where the ray hit
            }
        }
    }
}
