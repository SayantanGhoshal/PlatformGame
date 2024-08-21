using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    // Starting position for the parallax game object
    Vector2 startingPosition;

    // Start z value of the parallax game object 
    float startingZ;

    // Distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // If object is in front of target ,use near clip plane . if behind target use far clip plane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //The further the object from the player , the faster the parallaxEffect object will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //when the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        
        // The x/y positions changes based on target travel speed times the parallax factor, but z stay consistent.  
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
