using Godot;
using System;
using System.Diagnostics;

public partial class Puck : CharacterBody2D
{
	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
		HandleCollisions();
	}

	private void HandleCollisions()
	{
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			var collider = collision.GetCollider();
			
			if (collider is CharacterBody2D paddle)
			{
				OnPaddleCollision(paddle, collision);
			}
			else if (collider is StaticBody2D boards)
			{
				var puckDirection = Velocity.Normalized();
				//Debug.WriteLine($"\nPuck collided with boards");
				//Debug.WriteLine($"Puck speed {Velocity.Length()}");
				//Debug.WriteLine($"Puck direction {puckDirection}");
				//Debug.WriteLine($"Collision Position {collision.GetPosition()}");
				//Debug.WriteLine($"Collision Normal {collision.GetNormal()}");
				
				//Debug.WriteLine($"Puck direction {Velocity.Normalized()}");
				Velocity = Velocity.Bounce(collision.GetNormal());
				//Debug.WriteLine($"Bounce direction {Velocity.Normalized()}");
				
				OnWallCollision(boards);
			}
		}
	}

	private void OnWallCollision(StaticBody2D boards)
	{
	}

	private void OnPaddleCollision(CharacterBody2D paddle, KinematicCollision2D collision)
	{
		Debug.WriteLine($"\nPuck collided with PADDLE");
		
		var collisionPosition = collision.GetPosition();		
		var paddleToCollision = collisionPosition - paddle.GlobalPosition;
		var puckToCollision = collisionPosition - GlobalPosition; 
		
		Debug.WriteLine($"paddleToCollision: {paddleToCollision}");
		Debug.WriteLine($"puckToCollision: {puckToCollision}");
		Debug.WriteLine($"Collision Position {collision.GetPosition()}");
		Debug.WriteLine($"Collision Normal {collision.GetNormal()}");
		
		var direction = (GlobalPosition - paddle.GlobalPosition).Normalized();
		Debug.WriteLine($"direction: {direction * 10}");
		Debug.WriteLine($"GetNormal: {collision.GetNormal() * 10}");
		Debug.WriteLine($"Bounce: {Velocity.Bounce(collision.GetNormal()) * 10}");
		var paddleSpeed = paddle.Velocity.Length();
		Debug.WriteLine($"paddleSpeed: {paddleSpeed}");
		var colliderSpeed = collision.GetColliderVelocity().Length();
		Debug.WriteLine($"colliderSpeed: {colliderSpeed}");
		Velocity = direction * paddleSpeed;
	}
}
