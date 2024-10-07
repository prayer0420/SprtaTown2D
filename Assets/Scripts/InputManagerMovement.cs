using UnityEngine;

public class InputManagerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    // private이지만 인스펙터 표출은 하고 싶어!
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D를 캐싱합니다.
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input Manager에서 Vertical축과 Horizontal축의 입력을 받아옵니다.
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // 벡터의 normalized를 하면 벡터의 길이를 1로 만듭니다.
        // 예를 들어, vertical, horizontal 모두 1인 경우, direction의 크기는 1보다 크게 될 수 있는데, 이를 1로 맞춰줍니다.
        Vector2 direction = new Vector2(horizontal, vertical);
        direction = direction.normalized;

        // rigidbody.velocity는 해당 물체가 1초당 움직이는 거리를 말합니다.
        rigidbody.velocity = direction * speed;
    }
}
