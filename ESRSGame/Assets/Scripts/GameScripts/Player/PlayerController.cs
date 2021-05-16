using System;
using System.Linq;
using Cinemachine;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        private float _horizontalAD;
        public float zoomSpeed;
        public Vector2 zoomLimits;
        private float _verticalWs;
        private PlayerStats _playerStats;
        private WeaponSO _currentWeapon;
        private Rigidbody _rigidbody;
        private Transform _firePoint;
        private float _fireCountDown;
        private bool _canFire = false;
        private bool isUIOpen;
        public CinemachineVirtualCamera vCam;


        private void OnDestroy()
        {
            GameEventManager.onUIWindowChanged -= OnUIChange;
            GameEventManager.OnWeaponPickup -= OnWeaponPickup;
        }
        
        private void OnUIChange(object send, GameEventManager.OnUIWindowChangeEventArgs args)
        {
            isUIOpen = args.isWindowOpen;
        }

        private void Awake()
        {
            isUIOpen = false;
            GameEventManager.onUIWindowChanged += OnUIChange;
            
            GameEventManager.OnWeaponPickup  += OnWeaponPickup;
            _playerStats = GetComponent<PlayerStats>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnWeaponPickup(object sender, GameEventManager.OnWeaponPickUpEventArgs e)
        {
            
                this._currentWeapon = e.weaponSo;
                this._firePoint = transform.GetComponentsInChildren<Transform>()?.ToList()
                    .Find(x => x.CompareTag("FirePoint"));
        }

        private void FixedUpdate()
        {
            if (isUIOpen) return;
            MovePos();
            RotateToDirection();
            
            if (Input.GetButton("Fire1"))
            {
                Shoot(transform.forward);
            }

            CameraControls();
        }

        private void CameraControls()
        {
            var scrollVal =  -Input.GetAxis("Mouse ScrollWheel");
            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView,Mathf.Clamp(vCam.m_Lens.FieldOfView +  scrollVal * zoomSpeed, zoomLimits.x, zoomLimits.y),0.1f);
        }

        private void Update()
        {            
            if (_fireCountDown <= 0f)
            {
                _canFire = true;
                _fireCountDown = 1f / _currentWeapon.fireRate;
            }
            _fireCountDown -= Time.deltaTime;

          
        }


        private void MovePos()
        {
            float horizontalAD = Input.GetAxis("Horizontal_AD");
            float verticalWS = Input.GetAxis("Vertical_WS");
            Vector3 move = new Vector3(horizontalAD, 0, verticalWS);
            _rigidbody.velocity = move * (speed * _playerStats.playerSpeed);
        }
        public void Shoot(Vector3 direction)
        {
            if (_canFire)
            {
                _canFire = false;
                GameObject bullet = (GameObject)Instantiate(_currentWeapon.bulletPrefab, _firePoint.transform.position, Quaternion.identity);
                
                
                bullet.GetComponent<Bullet>()?.GetGunStats(direction, _currentWeapon.damage, _currentWeapon.radius,
                    _currentWeapon.projectileSpeed, _currentWeapon.lifeSpan);
            }
        }



        private void RotateToDirection()
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            if (float.IsNegativeInfinity(mousePos.x)) return;
            mousePos.y = transform.position.y;
            transform.LookAt(mousePos);
            Debug.DrawRay(transform.position,transform.forward*100,Color.red);
            
        }
    }
}