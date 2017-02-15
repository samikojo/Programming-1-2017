using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;

namespace TAMKShooter
{

    public class InputManager : MonoBehaviour
    {
        

        private PlayerUnit _playerUnit;

        

        // Use this for initialization
        void Awake()
        {
            _playerUnit = GetComponent<PlayerUnit>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerData.ControllerCodes code = _playerUnit.Data.ControllerCode;
            if (code != PlayerData.ControllerCodes.None)
            {
                float horizontal = Input.GetAxis("Horizontal_" + code.ToString());
                float vertical = Input.GetAxis("Vertical_" + code.ToString());

                Vector3 input = new Vector3(horizontal, 0, vertical);

                _playerUnit.Mover.MoveToDirection(input);

                bool shoot = Input.GetButton("Shoot_" + code.ToString());
                if (shoot)
                {
                     _playerUnit.Weapons.Shoot(_playerUnit.ProjectileLayer);
                }
            }
        }
    }
}
