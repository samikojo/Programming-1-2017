using System;
using UnityEngine;

namespace TAMKShooter.Systems
{
    public partial class InputManager
    {
        private enum Alignment
        {
            Horizontal    = 0,
            Vertical      = 1
        }

        public string FireButton = "0"; // User bindable fire button. Joypad button number or keyboard key.

        private string JoypadKeycode
        {
            get { return string.Format("joystick {0} button {1}", ControllerNumber, FireButton); }
        }

        private readonly ControllerType ControllerSelected;
        private readonly int ControllerNumber = 0; // 0 - uninitialize, 1-2 for keyboard and 1-4 for joypads

        public void ReleaseController()
        {
            InputManager._registeredController.Remove(this);
        }

        private string GetAxisName(Alignment whichAxis)
        {
            return string.Format("{0}_{1}_{2}", whichAxis == Alignment.Horizontal ? "Hor" : "Ver",
                ControllerSelected == ControllerType.Keyboard ? "key" : "joy", ControllerNumber);
        }

        public Vector3 PollStickAxis()
        {
            var hor = Input.GetAxis(GetAxisName(Alignment.Horizontal));
            var ver = Input.GetAxis(GetAxisName(Alignment.Vertical));

            return new Vector3(hor, 0, ver);
        }

        public bool PollFireButtonDown()
        {
            return Input.GetKeyDown(ControllerSelected == ControllerType.Keyboard ? FireButton : JoypadKeycode);
        }

        public bool PollFireButtonUp()
        {
            return Input.GetKeyUp(ControllerSelected == ControllerType.Keyboard ? FireButton : JoypadKeycode);
        }

        public bool PollFireButton()
        {
            return Input.GetKey(ControllerSelected == ControllerType.Keyboard ? FireButton : JoypadKeycode);
        }
    }
}