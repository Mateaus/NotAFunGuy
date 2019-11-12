using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        [SerializeField]
        private int xMin, xMax;
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public GameObject character;

        //public int xmin,xmax;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h == 0)
            {
                character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
               // character.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }


            // Pass all parameters to the character control script.
           
            

                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            if (m_Character.transform.position.x > xMax)
                transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
            if (m_Character.transform.position.x < xMin)
                transform.position = new Vector3(xMin, transform.position.y, transform.position.z);

        }
    }
}
