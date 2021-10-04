using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speedKoef;
    private GameObject target;
    private GameObject player;

    private void Start()
    {
        player = Loader.Instance.Player;
        target = player;
        transform.position = player.transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = (Vector3)Vector2.Lerp(transform.position, target.transform.position, speedKoef * Time.fixedDeltaTime) + new Vector3(0, 0, -10);
    }
}
