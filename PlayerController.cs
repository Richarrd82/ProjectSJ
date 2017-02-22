using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject startMenu;

    private Rigidbody2D rigidBody;
    private Animator playerAnim;

    private float jumpForce = 2.2f;
    private float angleOfJump = 2.2f;
    private float jumpTime = 0.5f;
    private float jumpTimeCounter;
    private bool gameStarted = false;
    private bool canJump = true;

	//ground detection
    public LayerMask WhatIsGround;
    private bool grounded;
    private Transform topLeft;
    private Transform bottomRight;
	private Vector3 startPos;

    public Animator MyAnimator { get { return playerAnim; } }
    public bool GameStarted { get { return gameStarted; } }

    void Start()
    {
        topLeft = this.gameObject.transform.GetChild(0);
        bottomRight = this.gameObject.transform.GetChild(1);
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
		startPos = transform.position;

        jumpTimeCounter = jumpTime;
	}

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, WhatIsGround);
		playerAnim.SetBool("Grounded", grounded);

		Jump();

		if (this.transform.position.x != startPos.x)
		{
			gameStarted = true;
			startMenu.SetActive(false);
		}
		
		if (gameStarted == true)
		{
			startMenu.SetActive(false);
			HUD.SetActive(true);
		}
	}

	void Jump()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (rigidBody.velocity.y == 0 && canJump == true)
				{
					gameStarted = true;
					rigidBody.velocity = new Vector2(angleOfJump, jumpForce);
				}
			}
			else
			{
				Debug.Log("Clicked on UI");
			}
		}

		if (Input.GetMouseButton(0))
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (jumpTimeCounter > 0 && canJump == true)
				{
					rigidBody.velocity = new Vector2(angleOfJump, jumpForce);
					jumpTimeCounter -= Time.deltaTime;
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (!EventSystem.current.IsPointerOverGameObject())
			{
				if (canJump == true)
					jumpTimeCounter = 0;
			}
		}

		if (grounded && rigidBody.velocity.y == 0)
			jumpTimeCounter = jumpTime;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = false;
        Invoke("ResetJumping", 0.15f);
    }

    void ResetJumping()
    {
        canJump = true;
    }

    public void KillPlayer()
    {
        this.gameObject.SetActive(false);
        gameStarted = false;

        HUD.SetActive(false);
        deathMenu.SetActive(true);

        PointManager.Instance.DeathMenuPoints();
    }
}