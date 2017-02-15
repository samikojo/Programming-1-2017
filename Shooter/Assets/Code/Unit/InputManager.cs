using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;

namespace TAMKShooter
{
    public class InputManager : MonoBehaviour
    {

        private Dictionary<PlayerUnit, string> _playerUnits = new Dictionary<PlayerUnit, string>();
        private int joyCount;
        private int keyCount;

        // Use this for initialization
        public void AddPlayer(PlayerUnit player, PlayerData.PlayerControllerType controllerType)
        {
            bool noController = false;
            bool tryToSwitch = false;

            if (Input.GetJoystickNames().Length <= joyCount)
            {
                noController = true;
            }

            switch (controllerType)
            {
                case PlayerData.PlayerControllerType.Controller:
                    if (noController)
                    {
                        Debug.Log("No joysticks free for " + player.Data.Id);
                        tryToSwitch = true;
                    }
                    else
                    {
                        _playerUnits.Add(player, "Joystick" + (joyCount + 1));
                        Debug.Log("Joystick" + (joyCount + 1) + " for " + player.Data.Id);
                        joyCount++;
                    }
                    break;

                case PlayerData.PlayerControllerType.Keyboard:
                    if (keyCount >= 2)
                    {
                        Debug.Log("No room in keyboard for " + player.Data.Id);
                        tryToSwitch = true;
                    }
                    else
                    {
                        _playerUnits.Add(player, "Keyboard" + (keyCount + 1));
                        Debug.Log("Keyboard" + (keyCount + 1) + " for " + player.Data.Id);
                        keyCount++;
                    }
                    break;
            }

            if (tryToSwitch)
            {
                if (noController && keyCount >= 2)
                {
                    Debug.Log("Cannot find any free input devices for " + player.Data.Id);
                }
                else if (!noController)
                {
                    _playerUnits.Add(player, "Joystick" + (joyCount + 1));
                    Debug.Log("Joystick" + (joyCount + 1) + " for " + player.Data.Id + " because no room on keyboard");
                    joyCount++;
                }
                else if (keyCount != 2 || keyCount > 2)
                {
                    _playerUnits.Add(player, "Keyboard" + (keyCount + 1));
                    Debug.Log("Keyboard" + (keyCount + 1) + " for " + player.Data.Id + " because no free joysticks");
                    keyCount++;
                }
            }
        }

        public Vector3 GetInputAxes(PlayerUnit player)
        {
            Vector3 input = Vector3.zero;
            float horizontal;
            float vertical;

            string controller;
            _playerUnits.TryGetValue(player, out controller);
            if (controller != null)
            {

                horizontal = Input.GetAxis(controller + "Horiz");
                vertical = Input.GetAxis(controller + "Vert");

                input = new Vector3(horizontal, 0, vertical);
            }

            return input;
        }

        public bool GetShoot(PlayerUnit player)
        {

            string controller;
            _playerUnits.TryGetValue(player, out controller);
            if (controller != null)
            {
                return Input.GetAxis(controller + "Shoot") > 0.1;
            }
            else return false;
        }
    }
}
