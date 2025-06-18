using UnityEngine;

public class MeuInputs : MonoBehaviour
{
    InputSystem_Menu inputSystems;

    public bool clicked;
    public bool canClick;
    public float clickTimer;
    private float clickCooldown = 0.3f;
    public bool spacePressed;

    public GameObject playButton;

    private void OnEnable()
    {
        if (inputSystems == null)
        {
            inputSystems = new InputSystem_Menu();
            inputSystems.UI.Click.performed += i => clicked = i.ReadValueAsButton();
            inputSystems.Player.Jump.performed += i => spacePressed = i.ReadValueAsButton();
        }
        inputSystems.Enable();
    }

    private void OnDisable()
    {
        inputSystems.Disable();
    }

    private void Update()
    {
        if(spacePressed && canClick)
        {
            canClick = false;
            clickTimer = clickCooldown;
            playButton.GetComponent<PlayPressed>().Clicked();
            playButton.GetComponent<PlayPressed>().spacePressed = true;
            playButton.GetComponent<PlayPressed>().Hovered();
        }
        if(clicked && canClick)
        {
            canClick = false;
            clickTimer = clickCooldown;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<PlayPressed>() != null)
                {
                    hit.collider.gameObject.GetComponent<PlayPressed>().Clicked();
                }
                if (hit.collider.gameObject.GetComponent<ExitPressed>() != null)
                {
                    hit.collider.gameObject.GetComponent<ExitPressed>().Clicked();
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<PlayPressed>() != null)
                {
                    hit.collider.gameObject.GetComponent<PlayPressed>().Hovered();
                }
                if (hit.collider.gameObject.GetComponent<ExitPressed>() != null)
                {
                    hit.collider.gameObject.GetComponent<ExitPressed>().Hovered();
                }
            }
        }

        if (!canClick)
        {
            clickTimer -= Time.deltaTime;
            if (clickTimer <= 0f)
            {
                canClick = true;
            }
        }
    }
}
