using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerController : MonoBehaviour, IPlayer
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private Camera _camera;
    [SerializeField] TextMeshProUGUI healthPoints;
    
    private float horizontalDir;
    private float verticalDir;

    private bool isLadder;
    private bool isClimbing;

    private float _jumpForce = 250.0f;
    private float _moveSpeed = 5f;
    private float calculatedSpeed = 0f;
    private int countLife =3;

    private int movementId = Animator.StringToHash("MoveSpeed"); 
    private int jumpId = Animator.StringToHash("Jump");
    private int hurtId = Animator.StringToHash("Hurt");
    private int climbId = Animator.StringToHash("Climb");

    public void Hurt()
    {
        _sprite.flipX = !_sprite.flipX;
        int direction = _sprite.flipX ? -1: 1;
        _animator.SetTrigger(hurtId);
        transform.position = Vector3.Lerp(transform.position,
                                        transform.position + 
                                        new Vector3(15*direction, 5, 0), 
                                        Time.deltaTime*10);
        countLife -=1;
        healthPoints.text = $"HP - {countLife}/3";
    }

    public void Win()
    {
        LevelManager.instance.Win();
        
    }

    private void CharacterCheckDie()
    {
        if(countLife == 0 || transform.position.y<-5)
        {
            LevelManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    public void Ladder(bool on)
    {
        isLadder = on;
        if (isLadder==false)
        {
            isClimbing=false;
        }
        _animator.SetBool(climbId, isLadder);
    }

    void Start()
    {
        healthPoints.text = $"HP - {countLife}/3";
    }

    void OnEnable()
    {
        LoadData();
    }

    private void CharacterMovement()
    {
        horizontalDir = Input.GetAxis("Horizontal");
        verticalDir = Input.GetAxis("Vertical");
        if(isLadder && verticalDir!=0)
        {
            isClimbing = true;
        }
        else{isClimbing = false;}

        if(_animator.GetBool(climbId)!= isClimbing)
        {
            _animator.SetBool(climbId, isClimbing);
        }
        if(isLadder)
        {
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(_rb.velocity.x, verticalDir * _moveSpeed);
        }
        else{_rb.gravityScale = 1f;}
        
        float newCalculatedSpeed = 0f;
        if(horizontalDir!=0){newCalculatedSpeed = 1f; _sprite.flipX = horizontalDir < 0;}
        else {calculatedSpeed = (calculatedSpeed < 0.01) ? 0: calculatedSpeed;}

        calculatedSpeed = Mathf.Lerp(calculatedSpeed, newCalculatedSpeed, Time.deltaTime*_moveSpeed);
        transform.position = Vector3.Lerp(transform.position,
                                        transform.position + new Vector3(horizontalDir, 0, 0), 
                                        Time.deltaTime*calculatedSpeed*_moveSpeed);

        

        _animator.SetFloat(movementId, calculatedSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up*_jumpForce);
            _animator.SetTrigger(jumpId);
        }

    }

    public void SaveData()
    {
        var data = new PlayerData(SceneManager.GetActiveScene().buildIndex, transform.position, countLife);
        var str = JsonUtility.ToJson(data);
        string path = Application.dataPath + "/Data.json";
        File.WriteAllText(path, str);
        Debug.Log($"{path} - path");
    }

    public void LoadData()
    {
        string path = Application.dataPath + "/Data.json";
        if (File.Exists(path))
        {
            var dataStr = File.ReadAllText(path);
            var data = JsonUtility.FromJson<PlayerData>(dataStr);

            if (SceneManager.GetActiveScene().buildIndex == data.IndexScene)
            {
                transform.position = data.Position;
                countLife = data.HealthPoints;
            }
        }
    }

    void Update()
    {
        CharacterMovement();
        CharacterCheckDie();
    }

    private void FixedUpdate()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, 
                                                transform.position + new Vector3(5, 2, -10), Time.deltaTime*_moveSpeed);
    }
}

[Serializable]
public class PlayerData
{
    public int IndexScene;
    public Vector3 Position;
    public int HealthPoints;

    public PlayerData(){}
    
    public PlayerData(int indexScene, Vector3 position, int healthPoints)
    {
        IndexScene = indexScene;
        Position = position;
        HealthPoints = healthPoints;
    }

}
