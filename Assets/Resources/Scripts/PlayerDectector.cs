using Unity.Behavior;
using UnityEngine;

public class PlayerDectector : MonoBehaviour
{
    [SerializeField] private GameObject player; // Reference to the player GameObject
    [SerializeField] private float detectionRange = 20f; // Range within which the player can be detected
    [SerializeField] private float fieldOfView = 120f; // Field of view angle in degrees

    private BehaviorGraphAgent btAgent; // Reference to the BehaviorGraph component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btAgent = GetComponent<BehaviorGraphAgent>(); // Get the BehaviorGraph component attached to this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - transform.position; // Calculate the direction from the bot to the player
        float distance = dir.magnitude; // Get the distance to the player
        dir.Normalize(); // Normalize the direction vector

        if (distance <= detectionRange) // Check if the player is within the detection range
        {   
            float angle = Vector3.Angle(transform.forward, dir); // Calculate the angle between the bot's forward direction and the direction to the player
            if (angle <= fieldOfView / 2f) // Check if the player is within the field of view
            {
                // Set the harasserDetected variable in the bot's blackboard to true
                btAgent.BlackboardReference.SetVariableValue<bool>("seeTarget", true);

                // Set the Target game object variable from the blackboard
                btAgent.BlackboardReference.SetVariableValue<GameObject>("Target", player);
            }
            else
            {
                btAgent.BlackboardReference.SetVariableValue<bool>("seeTarget", false);
            }
        }
        else
        {
            btAgent.BlackboardReference.SetVariableValue<bool>("seeTarget", false);
            btAgent.BlackboardReference.SetVariableValue<bool>("greeted", false);          //to make the bot greet when the player is in view again
            btAgent.BlackboardReference.SetVariableValue<bool>("canEngage", true); // Reset the canEngage variable when the player is out of range
        }
    }
}
