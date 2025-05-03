using UnityEngine;
using UnityEngine.InputSystem;

namespace BurrowWars
{
	public class PlayerInputHandler : MonoBehaviour
	{
		[SerializeField] private PlayerController _playerController;

		public void OnMove(InputAction.CallbackContext context)
		{
			var moveVector = context.ReadValue<Vector2>();

			if (_playerController != null)
			{
				_playerController.SetMovement(moveVector);
			}
		}

		public void OnEnterBurrow(InputAction.CallbackContext context)
		{
			if (context.started)
			{
				_playerController.SetBurrowEnter(true);
			}

			if (context.canceled)
			{
				_playerController.SetBurrowEnter(false);
			}
		}
	}
}