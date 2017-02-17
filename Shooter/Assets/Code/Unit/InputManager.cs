using UnityEngine;

namespace TAMKShooter {

    public class InputManager : MonoBehaviour {

        public enum UsedController {
            Error = 0,
            ArrowKeys,
            WASD,
            Controller1,
            Controller2
        }

        string horizontalMovementName;
        string verticalMovementName;
        string ShootName;
        public UsedController usedController;

	    void Awake () {
		    switch (usedController) {
                case UsedController.Error:
                    Debug.Log("Error: No controller selected! Choose one from the unit.");
                    break;
                case UsedController.ArrowKeys:
                    horizontalMovementName = "P1_Horizontal";
                    verticalMovementName = "P1_Vertical";
                    ShootName = "P1_Shoot";
                    break;
                case UsedController.WASD:
                    horizontalMovementName = "P2_Horizontal";
                    verticalMovementName = "P2_Vertical";
                        ShootName = "P2_Shoot";
                        break;
                case UsedController.Controller1:
                    horizontalMovementName = "P3_Horizontal";
                    verticalMovementName = "P3_Vertical";
                        ShootName = "P3_Shoot";
                        break;
                case UsedController.Controller2:
                    horizontalMovementName = "P4_Horizontal";
                    verticalMovementName = "P4_Vertical";
                        ShootName = "P4_Shoot";
                        break;
            }
	    }

        public void MoveAndShoot(Mover mover, int projectileLayer, WeaponController w) {
            if (usedController != UsedController.Error) {
                float horizontal = Input.GetAxis(horizontalMovementName);
                float vertical = Input.GetAxis(verticalMovementName);

                Vector3 movement = new Vector3(horizontal, 0f, vertical);

                mover.MoveToDirection(movement);

                if (Input.GetButton(ShootName)) {
                    w.Shoot(projectileLayer);
                }
            }
        }
    }
}
