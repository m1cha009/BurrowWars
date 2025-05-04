using System;
using UnityEngine;

namespace BurrowWars
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private LayerMask _groundLayerMask;
		[SerializeField] private LayerMask _burrowLayerMask;
		[SerializeField] private float _checkDistance;
		[SerializeField] private float _checkBurrowEnterDistance;

		[SerializeField] private PlatformEffector2D _mainGroundPlatformEffector2D;

		private Vector2 _moveVector;
		private Vector2 _groundNormal;
		private Vector2 _velocity;
		private bool _isWantToEnterBurrow;
		
		private Rigidbody2D _rigidbody2D;
		private bool _isGrounded;

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			
			_rigidbody2D.linearVelocity = Vector2.zero;
			_rigidbody2D.angularVelocity = 0f;
		}

		private void Update()
		{
			if (_isWantToEnterBurrow)
			{
				var burrowHit = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, _checkBurrowEnterDistance, _burrowLayerMask);
				
				Debug.DrawRay(transform.position + Vector3.down, Vector2.down * _checkBurrowEnterDistance, Color.red);

				if (burrowHit.collider != null)
				{
					Debug.Log("Burrow is under us");

					_mainGroundPlatformEffector2D.surfaceArc = 0;
				}
				else
				{
					_mainGroundPlatformEffector2D.surfaceArc = 180;
				}
			}
			else
			{
				_mainGroundPlatformEffector2D.surfaceArc = 180;
			}
			
			GroundMovement();
		}
		
		public void SetMovement(Vector2 moveVector)
		{
			_moveVector = moveVector;
		}

		public void SetBurrowEnter(bool isWantToEnterBurrow)
		{
			_isWantToEnterBurrow = isWantToEnterBurrow;
		}

		private void GroundMovement()
		{
			var hit = Physics2D.Raycast(transform.position, Vector2.down, _checkDistance, _groundLayerMask);
			
			if (hit.collider != null)
			{
				_groundNormal = hit.normal;
			
				var slopeDirection = new Vector2(_groundNormal.y, -_groundNormal.x);
            
				if (_moveVector.x < 0)
					slopeDirection = -slopeDirection;
			
				_velocity = slopeDirection * (_speed * Mathf.Abs(_moveVector.x));
			}
			
			transform.position += (Vector3)_velocity * Time.deltaTime;
		}
	}
}
