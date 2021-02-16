using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    public bool isColliding;
    public Contact touchContact;
    public CubeBehaviour steve;


    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        collideChecker();
        _scene();
        _Fire();
        _Move();
    }

    private void _scene()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            SceneManager.LoadScene("Open Screen");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void _Move()
    {
        if (isGrounded)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity += playerCam.transform.right * speed *Time.deltaTime;
            }

            else if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity += -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity += playerCam.transform.forward * speed *  Time.deltaTime;
            }

            else if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                // move Back
                body.velocity += -playerCam.transform.forward * speed *  Time.deltaTime;
            }
       

        // remove y
        

            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z);
            
            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity += body.transform.up * speed * Time.deltaTime;
                
            }
            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.95f);
        }

        if (isColliding)
        {
            if (touchContact.face.x < 0.0f)
            {
               if (body.velocity.x < 0.0f) { body.velocity.x = 0.0f; }

            }

            if (touchContact.face.x > 0.0f)
            {
                if (body.velocity.x > 0.0f) { body.velocity.x = 0.0f; }
            }

            if (touchContact.face.z < 0.0f)
            {
                if (body.velocity.z < 0.0f) { body.velocity.z = 0.0f; }
            }

            if (touchContact.face.z > 0.0f)
            {
                if (body.velocity.z > 0.0f) { body.velocity.z = 0.0f; }
                
            }
        }
    
        transform.position += body.velocity;

    }


    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

    private void collideChecker()
    {
        isColliding = cube.isColliding;
        if (isColliding)
        {
            touchContact = cube.touchContact;
        }
        else { touchContact = null; }
    }

}
