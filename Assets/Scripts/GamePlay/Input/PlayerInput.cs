using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Character player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        
        player.Move(direction);
    }

    private void FixedUpdate()
    {
        //Add Force Here???
    }
}
