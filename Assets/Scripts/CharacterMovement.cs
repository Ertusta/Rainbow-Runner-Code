using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{


    public float Speed;
    public float Jump;

    private bool _IsMove = true;
    private bool _IsJump = true;
    private bool _IsShield = true;


    int _health = 3;
    int _point = 0;
    int _level = 1;
    int _lastPoint;
    int _bestPoint;


    Animator _animator;
    Rigidbody _rb;

    public CapsuleCollider StandCollider;
    public CapsuleCollider SlideCollider;

    public TMPro.TextMeshProUGUI HealthText;
    public TMPro.TextMeshProUGUI PointText;
    public TMPro.TextMeshProUGUI LastPoint;
    public TMPro.TextMeshProUGUI BestPoint;



    public GameObject Shield;




    void Awake()
    {
        _lastPoint = PlayerPrefs.GetInt("_lastPoint");
        _bestPoint = PlayerPrefs.GetInt("_bestPoint");

        //�nceki  oyunlardaki de�erleri yerine UI ye yazar
        LastPoint.text = "Last point:" + _lastPoint.ToString();
        BestPoint.text = "Best point:" + _bestPoint.ToString();


    }

    void Start()
    {   

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        InvokeRepeating("PointCounter", 1, 1);
    }



    //input tu�lar�ndan birine bas�nca hareket
    public void UpInput(InputAction.CallbackContext value)
    {
        if (value.started && _IsJump)
        {
            _rb.AddForce(new Vector3(0, Jump, 0), ForceMode.Impulse);
            _IsJump = false;
            _animator.SetBool("IsJump", true);

        }
    }
    public void DownInput(InputAction.CallbackContext value)
    {
        if (value.started && _IsJump)
        {
            _rb.AddForce(new Vector3(0, -Jump, 0), ForceMode.Impulse);
            _animator.SetBool("IsSlide", true);
            Invoke("Stand", 1);

            //alt�ndan ge�ilmesi gereken objeler i�in
            StandCollider.enabled = false;
            SlideCollider.enabled = true;

        }
    }
    public void RightInput(InputAction.CallbackContext value)
    {
        if (value.started && _IsMove)
        {
            _rb.AddForce(new Vector3(Speed, 0, 0), ForceMode.Impulse);
            _IsMove = false;


        }
    }
    public void LeftInput(InputAction.CallbackContext value)
    {
        if (value.started && _IsMove)
        {
            _rb.AddForce(new Vector3(-Speed, 0, 0), ForceMode.Impulse);
            _IsMove = false;
        }
    }


    private void OnTriggerEnter(Collider collision)
    { 
        //spesifik noktalarda durmas� i�in
        if (collision.gameObject.CompareTag("Stop"))
        {        
            Vector3 velocity = _rb.velocity;
            velocity.x = 0f;
            _rb.velocity = velocity;
            _IsMove = true;
        }

        //d��mana �arparsa can� azalt�r kalkan ekler
        if (collision.gameObject.CompareTag("Enemy") && _IsShield)
        {
            _health -= 1;
            HealthText.text = "Health=" + _health.ToString();

            _IsShield = false;
            Invoke("ShieldCooldown", 3);
            Shield.SetActive(true);

            Invoke("DeathControl", 0);



        }
        //paraya �arparsa puan ekler
        if (collision.gameObject.CompareTag("Coin"))
        {
            _level = GameMachine.Instance.Level;
            _point += 10 * _level;
            Invoke("PointCounter", 0);
        }
    }

    //yere bas�nca ve duvara �arp�nca
    private void OnCollisionEnter(Collision collision)
    {
        //haritadan ��kmamas� i�in geri f�rlat�r
        if (collision.gameObject.CompareTag("Wall_R"))
        {
            _rb.AddForce(new Vector3(-Speed, 0, 0), ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Wall_L"))
        {
            _rb.AddForce(new Vector3(Speed, 0, 0), ForceMode.Impulse);
        }

        //z�plama i�in
        if (collision.gameObject.CompareTag("Ground"))
        {
            _IsJump = true;
            _animator.SetBool("IsJump", false);
        }

    }
    //belli s�re sonra aya�a kalkar
    void Stand()
    {
        _animator.SetBool("IsSlide", false);
       
        StandCollider.enabled = true;
        SlideCollider.enabled = false;


    }

    //bekleme s�resi bitince kalkan� kapat�r
    void ShieldCooldown()
    {
        _IsShield = true;
        Shield.SetActive(false);

    }

    //puan sistemi �al��t���nda level kadar puan� art�r�r
    private void PointCounter()
    {
        //puan�n en iyi puan� ge�erse renk de�i�tirir
        if(_bestPoint<_point)
        {
            PointText.color = Color.yellow;
        }
        _level = GameMachine.Instance.Level;
        _point += 1 * _level;
        PointText.text = _point.ToString();
    }

    //karakter �l�nce puanlar� kaydeder
    void DeathControl()
    {
        if (_health == 0)
        {
            PlayerPrefs.SetInt("_lastPoint", _point);

            if (_point > PlayerPrefs.GetInt("_bestPoint"))
            {
                PlayerPrefs.SetInt("_bestPoint", _point);
            }

            SceneManager.LoadScene("GameScene");
        }
    }
}
