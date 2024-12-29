using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private AudioClip jumpSFX;
    public bool movable;
    private AudioSource audioSource;
    private Rigidbody2D body;
    private Animator anim;
    private Foot foot;
    private bool grounded => foot.grounded;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        foot = GetComponentInChildren<Foot>();
        audioSource = GetComponent<AudioSource>();
        movable = false;
    }

    private void Update()
    {
        if (!movable)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            anim.SetBool("Run", false);
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        body.velocity = new Vector2(horizontalInput * horizontalSpeed, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        anim.SetTrigger("Jump");
        audioSource.clip = jumpSFX;
        audioSource.Play();
    }

    public IEnumerator WaitForSec(float sec)
    {
        movable = false;
        yield return new WaitForSeconds(sec);
        movable = true;
    }
}
