using UnityEngine;

public class InputManagerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    // private������ �ν����� ǥ���� �ϰ� �;�!
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D�� ĳ���մϴ�.
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input Manager���� Vertical��� Horizontal���� �Է��� �޾ƿɴϴ�.
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // ������ normalized�� �ϸ� ������ ���̸� 1�� ����ϴ�.
        // ���� ���, vertical, horizontal ��� 1�� ���, direction�� ũ��� 1���� ũ�� �� �� �ִµ�, �̸� 1�� �����ݴϴ�.
        Vector2 direction = new Vector2(horizontal, vertical);
        direction = direction.normalized;

        // rigidbody.velocity�� �ش� ��ü�� 1�ʴ� �����̴� �Ÿ��� ���մϴ�.
        rigidbody.velocity = direction * speed;
    }
}
