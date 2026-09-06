using UnityEngine;

public class VehicleBase : MonoBehaviour
{
    [System.Serializable]
    public class VehicleStats
    {
        public string vehicleName = "Vehicle";
        public float maxSpeed = 50f;
        public float acceleration = 5f;
        public float brakeForce = 10f;
        public float turnSpeed = 5f;
        public float maxHealth = 200f;
        public float weight = 1f;
    }

    public VehicleStats stats = new VehicleStats();
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float currentHealth;
    
    protected float currentSpeed = 0f;
    protected bool isPlayerInside = false;
    protected Transform playerSeat;
    protected GameObject player;

    protected virtual void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        currentHealth = stats.maxHealth;
    }

    protected virtual void FixedUpdate()
    {
        if (!isPlayerInside) return;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Movimento
        Vector3 moveDirection = transform.forward * vertical * stats.acceleration;
        rb.velocity = new Vector3(rb.velocity.x + moveDirection.x, rb.velocity.y, rb.velocity.z + moveDirection.z);

        // Limitar velocidade máxima
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        if (speed > stats.maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z) * (stats.maxSpeed / speed);
        }

        // Rotação
        transform.Rotate(0, horizontal * stats.turnSpeed, 0);

        // Freio
        if (Input.GetKey(KeyCode.Space))
        {
            rb.velocity *= 0.9f;
        }
    }

    public virtual void EnterVehicle(GameObject player, Transform seat)
    {
        this.player = player;
        this.playerSeat = seat;
        isPlayerInside = true;
        player.SetActive(false);
        Debug.Log($"Entrou no veículo: {stats.vehicleName}");
    }

    public virtual void ExitVehicle()
    {
        if (player != null)
        {
            player.SetActive(true);
            player.transform.position = transform.position + transform.forward * 3f;
            isPlayerInside = false;
            player = null;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

    public float GetHealthPercent() => currentHealth / stats.maxHealth;
    public bool IsPlayerInside() => isPlayerInside;
}