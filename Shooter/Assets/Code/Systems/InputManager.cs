using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TAMKShooter.Systems
{
    public partial class InputManager
    {
        private const int MaxKeyboard = 2;
        private const int MaxJoypad  =4;

        public enum ControllerType
        {
            Keyboard  = 0,
            Joypad    = 1
        }

        private static readonly List<InputManager> _registeredController = new List<InputManager>(6);

        /// <summary>
        /// Tries to find a free controller of given type.
        /// </summary>
        /// <param name="ofWhichType">Controller of which type to look for.</param>
        /// <param name="foundController">Number of controller of that type.</param>
        /// <returns></returns>
        private static bool GetFreeController(ControllerType ofWhichType, ref int foundController)
        {
            var ints = new[] {1, 2, 3, 4, 5};
            foundController = ints.Where(x => _registeredController.Count(y => y.ControllerSelected == ofWhichType && y.ControllerNumber == x) == 0).DefaultIfEmpty(0).First();
            return foundController > 0 && foundController <= (ofWhichType == ControllerType.Joypad ? 4 : 2);

        }

        /// <summary>
        /// Private constructor ensures only static part of the InputManager creates controller
        /// instances.
        /// </summary>
        private InputManager(ControllerType selectedController)
        {
            var controllerNumber = 0;
            if (GetFreeController(selectedController, ref controllerNumber))
            {
                ControllerSelected = selectedController;
                ControllerNumber = controllerNumber;
                _registeredController.Add(this);
            }
        }

        /// <summary>
        /// Public interface for InputManager constructor, will not give
        /// out instances if there is not enough supported controllers left.
        /// </summary>
        /// <param name="whatKindofController"></param>
        /// <returns></returns>
        public static InputManager GetController(ControllerType whatKindofController)
        {
            var manager = new InputManager(whatKindofController);
            return manager.ControllerNumber > 0 ? manager : null;
        }
    }
}