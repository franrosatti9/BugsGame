using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float shootRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float dashCooldown;

    [Header("Input")]
    [SerializeField] KeyCode dashKey;
    [SerializeField] KeyCode shootKey;
    
    [Header("References")]
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform aimDot;
    [SerializeField] GameObject bullet;
    [SerializeField] Camera cam;
    float _nextShootTime;
    Vector2 _shootDir;
    Vector2 _dir;
    Rigidbody2D _rb;
    bool _grounded;

    [Header("Bugs")]
    public bool inverseShootingBug;
    public bool inverseMovementBug;
    public bool shootingJumpingBug;
    public bool randomTeleportBug;

    [SerializeField] float randomTpRate;
    [SerializeField] LayerMask tpCheckLayer;
    float _randomTpCooldown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _randomTpCooldown = 0f;
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        _dir.x = !inverseMovementBug ? h : -h;
        _dir.y = !inverseMovementBug ? v : -v;

        
        // if (Input.GetKeyDown(dashKey)) { Dash(); }
        

        _shootDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        gunPivot.transform.right = !inverseShootingBug ? _shootDir.normalized : -_shootDir.normalized;

        if (Input.GetKeyDown(shootKey))
        {
            if (CanShoot())
            {
                _nextShootTime = Time.time + shootRate;
                Shoot();
            }
        }
        aimDot.position = cam.ScreenToWorldPoint(Input.mousePosition);

        if (randomTeleportBug) HandleRandomTeleport();
    }

    private void FixedUpdate()
    {
        //_grounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);

        Move(_dir);
        //Jump();
    }

    void Move(Vector2 dir)
    {
        _rb.velocity = new Vector2(_dir.x, _dir.y).normalized * speed;
    }

    // TODO: IMPLEMENTAR DASH
    void Dash()
    {
       
    }

    void Shoot()
    {
        var newBullet = GameObject.Instantiate(bullet, shootPoint.position, Quaternion.identity);
        newBullet.transform.up = gunPivot.transform.right;
        newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.up * bulletSpeed;
    }

    bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }

    public void InvertShootAndDashButtons(bool enabled)
    {
        var aux = dashKey;
        dashKey = shootKey;
        shootKey = aux;
        shootingJumpingBug = enabled;
    }

    void HandleRandomTeleport()
    {
        _randomTpCooldown += Time.deltaTime;
        if(_randomTpCooldown >= randomTpRate)
        {
            _randomTpCooldown = 0f;
            Teleport();
        }
    }

    void Teleport()
    {
        // Busqueda de punto random para teletransportarse, mientras no colisione con paredes o enemigos
        Vector2 newPosition;
        newPosition.x = Random.Range(-13.5f, 13.5f);
        newPosition.y = Random.Range(-6.3f, 3f);

        while (Physics2D.OverlapCircle(newPosition, 0.2f, tpCheckLayer)) 
        {
            newPosition.x = Random.Range(-13.5f, 13.5f);
            newPosition.y = Random.Range(-6.3f, 3f);
        }

        transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.15f);
    }
}
