using UnityEngine;
using Unity.Behavior;
using System.Runtime.CompilerServices;

public class PlayerAction : MonoBehaviour
{
    public float shoutingRange = 20f; // Range within which the bot can hear the player shouting
    public float engageRange = 5f; // Range within which the bot will engage with the player

    [Range(0, 100)] public int playerCostumeRating = 0; // the rating of the player's costume, from 0 to 100
    public LayerMask botLayer;

    [SerializeField] private BehaviorGraphAgent behaviorGraphAgent; // Reference to the BehaviorGraph component

    [SerializeField] private AudioClip insultSound; // Sound effect for the insult action
    [SerializeField] private AudioClip takePictureSound; // Sound effect for the camera action
    [SerializeField] private AudioClip askDirectionSound; // Sound effect for the camera action
    [SerializeField] private AudioClip askCharacterSound; // Sound effect for the ask character action
    [SerializeField] private AudioClip askAnimeSound; // Sound effect for the ask anime recommendation action
    [SerializeField] private AudioClip askRateCostumeSound; // Sound effect for the ask rate costume action

    private AudioSource audioSource; // Reference to the AudioSource component

    private bool canEngage; // Variable to check if the bot can engage with the player
    private bool competitionStart; // Variable to check if the competition has started


    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the player
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // I = insult
        {
            Insult();
        }
        if (Input.GetKeyDown(KeyCode.C))    // C = camera
        {
            TakePicture();
        }
        if (Input.GetKeyDown(KeyCode.G))     // G = ask for direction
        {
            AskDirection();
        }
        if (Input.GetKeyDown(KeyCode.Q))      // Q = ask about character
        {
            AskCharacter();
        }
        if (Input.GetKeyDown(KeyCode.E))      // E = ask anime recommendation
        {
            AskAnimeRecommendation();
        }
        if (Input.GetKeyDown(KeyCode.R))      // R = ask to rate player's costume
        {
            AskRateCostume();
        }
        if (Input.GetKeyDown(KeyCode.J))   // J = ask to act
        {
            AskToAct();
        }
        if (Input.GetKeyDown(KeyCode.K))    // K = ask to sing
        {
            AskToSing();
        }
        if (Input.GetKeyDown(KeyCode.L))    // L = ask to dance
        {
            AskToDance();
        }
    }

    public void Insult()
    {   
        audioSource.clip = insultSound; // Set the insult sound effect to the audio source
        audioSource.Play(); // Play the insult sound effect

        Collider[] bots = Physics.OverlapSphere(transform.position, shoutingRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot

        foreach (Collider bot in bots)
        {
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {
                bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                bgAgent.BlackboardReference.SetVariableValue<bool>("harasserDetected", true); // Set the harasserDetected variable in the bot's blackboard to true
                bgAgent.BlackboardReference.SetVariableValue<bool>("greeted", true); // there is no reason for the bot to greet the player if the bot is going to respond the player's insult
                
            }
        }
    }

    public void TakePicture()
    {   
        audioSource.clip = takePictureSound; // Set the take picture sound effect to the audio source
        audioSource.Play(); // Play the take picture sound effect
        Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot
        foreach (Collider bot in bots)
        {
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {   
                bgAgent.BlackboardReference.GetVariableValue<bool>("canEngage", out bool canEngage); // Get the canEngage variable from the bot's blackboard
                if (canEngage == true) // Check if the bot can engage
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<bool>("takePicture", true); // Set takePicture in the bot's blackboard to true
                }
            }
        }
    }

    public void AskDirection()
    {   
        audioSource.clip = askDirectionSound; // Set the ask direction sound effect to the audio source
        audioSource.Play(); // Play the ask direction sound effect
        Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot
        foreach (Collider bot in bots)
        {
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {
                bgAgent.BlackboardReference.GetVariableValue<bool>("canEngage", out bool canEngage); // Get the canEngage variable from the bot's blackboard
                if (canEngage == true) // Check if the bot can engage
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askDirection", true); // Set askDirection in the bot's blackboard to true
                }              
            }
        }
    }

    public void AskCharacter()
    {   
        audioSource.clip = askCharacterSound; // Set the ask character sound effect to the audio source
        audioSource.Play(); // Play the ask character sound effect
        Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot
        foreach (Collider bot in bots)
        {   
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {
                bgAgent.BlackboardReference.GetVariableValue<bool>("canEngage", out bool canEngage); // Get the canEngage variable from the bot's blackboard
                if (canEngage == true) // Check if the bot can engage
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askCharacter", true); // Set askCharacter in the bot's blackboard to true
                }  
            }
        }
    }

    public void AskAnimeRecommendation()
    {   
        audioSource.clip = askAnimeSound; // Set the ask anime recommendation sound effect to the audio source
        audioSource.Play(); // Play the ask anime recommendation sound effect
        Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot
        foreach (Collider bot in bots)
        {
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {
                bgAgent.BlackboardReference.GetVariableValue<bool>("canEngage", out bool canEngage); // Get the canEngage variable from the bot's blackboard
                if (canEngage == true)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<bool>("recommendAnime", true); // Set askAnimeRecommendation in the bot's blackboard to true
                }
            }
        }
    }

    public void AskRateCostume()
    {   
        audioSource.clip = askRateCostumeSound; // Set the ask rate costume sound effect to the audio source
        audioSource.Play(); // Play the ask rate costume sound effect
        Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); // Find all colliders in range that are on the bot layer, in this case there is only one bot
        foreach (Collider bot in bots)
        {
            var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider
            if (bgAgent != null)
            {
                bgAgent.BlackboardReference.GetVariableValue<bool>("canEngage", out bool canEngage); // Get the canEngage variable from the bot's blackboard
                if (canEngage == true)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<GameObject>("Target", this.gameObject); // Set the Target gamebject variable from the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<int>("costumeRating", playerCostumeRating); // Give player's costume rating to the costume rating variable in the blackboard
                    bgAgent.BlackboardReference.SetVariableValue<bool>("rateCostume", true); // Set askAnimeRecommendation in the bot's blackboard to true
                }
            }
        }
    }

    public void AskToAct()
    {   
        Debug.Log("You are asking the bot to act");
        behaviorGraphAgent.BlackboardReference.GetVariableValue<bool>("competitionStart", out competitionStart); // Get the competitionStart variable from the bot's blackboard
        if (competitionStart == true)
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, shoutingRange, botLayer); // if in competion, increase range for convienience
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToAct", true); // Set askAnimeRecommendation in the bot's blackboard to true
                }
            }
        }
        else
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); 
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToAct", true); // Set askAnimeRecommendation in the bot's blackboard to true
                }
            }
        }
    }

    public void AskToSing()
    {
        Debug.Log("You are asking the bot to sing");
        behaviorGraphAgent.BlackboardReference.GetVariableValue<bool>("competitionStart", out competitionStart); // Get the competitionStart variable from the bot's blackboard
        if (competitionStart == true)
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, shoutingRange, botLayer); // if in competion, increase range for convienience
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToSing", true); 
                }
            }
        }
        else
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); 
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToSing", true);
                }
            }
        }
    }

    public void AskToDance()
    {
        Debug.Log("You are asking the bot to dance");
        behaviorGraphAgent.BlackboardReference.GetVariableValue<bool>("competitionStart", out competitionStart); // Get the competitionStart variable from the bot's blackboard
        if (competitionStart == true)
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, shoutingRange, botLayer); // if in competion, increase range for convienience
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToDance", true); 
                }
            }
        }
        else
        {
            Collider[] bots = Physics.OverlapSphere(transform.position, engageRange, botLayer); 
            foreach (Collider bot in bots)
            {
                var bgAgent = bot.GetComponent<BehaviorGraphAgent>(); // Get the CosplayBot component from the collider in case there are multiple bots in the competition
                if (bgAgent != null)
                {
                    bgAgent.BlackboardReference.SetVariableValue<bool>("isEngage", true); // Set isEngage in the bot's blackboard to true
                    bgAgent.BlackboardReference.SetVariableValue<bool>("askedToDance", true);
                }
            }
        }
    }
}
