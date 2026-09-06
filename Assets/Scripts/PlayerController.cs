using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // arraste a câmera principal ou um pivot
    public Transform muzzle; // ponto de saída dos projéteis
    public Weapon weapon;
    public float moveSpeed = 6f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = cameraTransform.right;
        right.y = 0;

        Vector3 moveDir = (forward * v + right * h).normalized;

        if (moveDir.sqrMagnitude > 0.001f)
        {
            transform.forward = moveDir; // vira o jogador na direção do movimento
        }

        controller.Move(moveDir * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && weapon != null)
        {
            weapon.Fire(muzzle);
        }
    }
}
