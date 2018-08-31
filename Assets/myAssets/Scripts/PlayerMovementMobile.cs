using UnityEngine;
//using UnitySampleAssets.CrossPlatformInput;
using GeekGame.Input;
namespace CompleteProject
{
    public class PlayerMovementMobile : MonoBehaviour
    {
        public float speed = 6f;            // The speed that the player will move at.


        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

        CharacterStats characterStats;

        public ParticleSystem effect;

        Vector3 EAV;
        void Awake ()
        {

            // Set up references.
            anim = GetComponent <Animator> ();
            playerRigidbody = GetComponent <Rigidbody> ();
            characterStats = GetComponent <CharacterStats>();
            //effect = GetComponent<ParticleSystem>();
        }

        void Start()
        {
            EAV = new Vector3(0, 100, 0);
            effect.Stop();
        }


        void FixedUpdate ()
        {
            // Store the input axes.
//            float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
//            float v = CrossPlatformInputManager.GetAxisRaw("Vertical");

			//the input axes from joystickMove

			float h=JoystickMove.instance.H;
			float v=JoystickMove.instance.V;

            // Move the player around the scene.
            Move (h, v);

            // Turn the player to face the mouse cursor.
            //Turning ();

            // Animate the player.
            Animating (h, v);
            
            if (Item.isActive)
            {
                effect.Play();
                Item.isActive = false;
                characterStats.armor.AddModifier(5);
            }
        }


        void Move (float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set (h, 0f, v);
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition (transform.position + movement);
            if (movement != Vector3.zero)
            playerRigidbody.MoveRotation(Quaternion.LookRotation(movement));
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            
        }


        void Turning ()
        {


            //Vector3 turnDir = new Vector3(CrossPlatformInputManager.GetAxisRaw("Mouse X") , 0f , CrossPlatformInputManager.GetAxisRaw("Mouse Y"));
			Vector3 turnDir = new Vector3(JoystickRotate.instance.H , 0f , JoystickRotate.instance.V);

            if (turnDir != Vector3.zero)
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = (transform.position + turnDir) - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotatation);
            }

        }


        void Animating (float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool ("IsWalking", walking);
        }
    }
}