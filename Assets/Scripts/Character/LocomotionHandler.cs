using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZY
{
    [RequireComponent(typeof(CharacterController))]
    public class LocomotionHandler : MonoBehaviour
    {

        #region Properties
        [HideInInspector] public InputHandler inputHandler;
        [HideInInspector] public Vector3 originPosition;
        public bool movable { get; set; }
        public float moveSpeed = 7.5f;
        public bool debugMode = false;

        private CharacterController characterController;
        private Vector3 gravity;
        private Vector3 movePoint;
        private Vector3 slopeForce;
        #endregion

        private void Awake()
        {
            characterController = this.GetComponent<CharacterController>();
            inputHandler = this.GetComponent<InputHandler>();
        }
        void Start()
        {
            originPosition = transform.position;
        }

        void Update()
        {
            if (Input.anyKeyDown)
            {
                movable = true;
            }
            if (movable != true)
            {
                return;
            }
            Locomotion();
            LimitMovement();
            Crouch();
            Jump();
        }
        private void FixedUpdate()
        {
            DetectSlope();
        }
        #region CharacterLocomotion
        private void Locomotion()
        {
            gravity += Physics.gravity * Time.deltaTime;

            if (characterController.isGrounded)
            {
                gravity = Vector3.zero;
            }

            characterController.Move((movePoint + gravity + slopeForce + Vector3.forward) * Time.deltaTime * moveSpeed);
        }

        private void LimitMovement()
        {
            float x = originPosition.x;
            if (inputHandler.rightBtnClick == true)
            {
                x = Vector3.right.x + originPosition.x;
            }
            else if (inputHandler.leftBtnClick == true)
            {
                x = Vector3.left.x + originPosition.x;
            }
            else 
            {
                x = originPosition.x;
            }
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        private void Crouch()
        {
            if (inputHandler.downBtnClick)
            {
                characterController.center = new Vector3(0, -.5f, 0);
                characterController.height = 1;
            }
            else
            {
                characterController.center = new Vector3(0, 0, 0);
                characterController.height = 2;
            }
        }
        private void Jump()
        {
            if (inputHandler.upBtnClick && characterController.isGrounded)
            {
                characterController.Move(Vector3.up);
            }
        }
        #endregion

        #region DectectSlope
        private void DetectSlope()
        {
            //DetectSlope
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -transform.up);

            if (Physics.Raycast(ray ,out hit, 2f))
            {
                float slopedot = Vector3.Dot(transform.up, hit.normal);
                Vector3 slopeforward = Vector3.Cross(hit.transform.right, hit.normal) * (1 - slopedot);

                if (Mathf.Abs(slopedot) != 1 )
                {
                    slopeForce = -slopeforward ;
                }
                else
                {
                    slopeForce = Vector3.zero;
                }
            }
        }
        #endregion

    }

}
