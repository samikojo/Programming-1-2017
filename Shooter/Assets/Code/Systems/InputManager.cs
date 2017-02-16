using System;
using System.Collections;
using System.Collections.Generic;
using TAMKShooter;
using TAMKShooter.Data;
using UnityEngine;

namespace TAMKShooter
{
    public class InputManager : MonoBehaviour
    {
        private PlayerUnits PlayerUnits;

        private Dictionary<PlayerData.PlayerId, InputBase> _inputs = 
            new Dictionary<PlayerData.PlayerId, InputBase>();

        public void Start()
        {
            Init();
        }

        public void Init()
        {
            _inputs.Add(PlayerData.PlayerId.Player1, new Player1());
            _inputs.Add(PlayerData.PlayerId.Player2, new Player2());
            _inputs.Add(PlayerData.PlayerId.Player3, new Player3());
            _inputs.Add(PlayerData.PlayerId.Player4, new Player4());

            PlayerUnits = FindObjectOfType<PlayerUnits>();
        }
        
        void Update()
        {
            foreach (var player in PlayerUnits.Players)
            {
                float horizontal = Input.GetAxis(_inputs[player.Key].Horizontal);
                float vertical = Input.GetAxis(_inputs[player.Key].Vertical);

                Vector3 input = new Vector3(horizontal, 0, vertical);

                player.Value.Mover.MoveToDirection(input);

                bool shoot = Input.GetButton(_inputs[player.Key].Shoot);
                if (shoot)
                {
                    player.Value.Weapons.Shoot(player.Value.ProjectileLayer);
                }
            }
        }
    }

    public class InputBase
    {
        public string Horizontal;
        public string Vertical;
        public string Shoot;
    }

    public class Player1 : InputBase
    {
        public Player1()
        {
            Horizontal = "P1 Horizontal";
            Vertical = "P1 Vertical";
            Shoot = "P1 Shoot";
        }
    }

    public class Player2 : InputBase
    {
        public Player2()
        {
            Horizontal = "P2 Horizontal";
            Vertical = "P2 Vertical";
            Shoot = "P2 Shoot";
        }
    }

    public class Player3 : InputBase
    {
        public Player3()
        {
            Horizontal = "P3 Horizontal";
            Vertical = "P3 Vertical";
            Shoot = "P3 Shoot";
        }
    }

    public class Player4 : InputBase
    {
        public Player4()
        {
            Horizontal = "P4 Horizontal";
            Vertical = "P4 Vertical";
            Shoot = "P4 Shoot";
        }
    }

}


