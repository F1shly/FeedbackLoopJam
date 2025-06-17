using UnityEngine;

public class Inputs : MonoBehaviour
{
    InputSystem_Actions inputSystems;

    public Vector2 movementInput;

    public bool Jumping;
    public bool Interacting;

    public bool clicked;

    private void OnEnable()
    {
        if (inputSystems == null)
        {
            inputSystems = new InputSystem_Actions();
            inputSystems.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            inputSystems.Player.Jump.performed += i => Jumping = i.ReadValueAsButton();
            inputSystems.Player.Interact.performed += i => Interacting = i.ReadValueAsButton();

            inputSystems.UI.Click.performed += i => clicked = i.ReadValueAsButton();
        }
        inputSystems.Enable();
    }

    private void OnDisable()
    {
        inputSystems.Disable();
    }

    private bool canClick = true;
    private float clickCooldown = 0.2f;
    private float clickTimer = 0f;

    private void Update()
    {
        if (clicked && canClick)
        {
            canClick = false;
            clickTimer = clickCooldown;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<ToggleAudio>() != null)
                {
                    hit.collider.gameObject.GetComponent<ToggleAudio>().Switch();
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
