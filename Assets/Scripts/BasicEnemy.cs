using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _speed;

    private Transform _myTransform;
    private CharacterController _myCharacterController;
    // Start is called before the first frame update
    void Start()
    {
        _myCharacterController = GetComponent<CharacterController>();
        _myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direccion = _player.transform.position-transform.position;
        direccion.Normalize();
        _myCharacterController.SimpleMove(direccion * _speed);
    }
}
