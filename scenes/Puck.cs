using Godot;
using System;
using System.Diagnostics;

public partial class Puck : CharacterBody2D
{
    public override void _Process(double delta)
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is StaticBody2D boards)
            {
                OnWallCollision(boards);
            }
        }

        MoveAndSlide();
    }

    private void OnWallCollision(StaticBody2D boards)
    {
        var puckDirection = Velocity.Normalized();
        Debug.WriteLine($"Puck collided with boards at speed {Velocity.Length()} and direction {puckDirection} at {boards.GlobalPosition}");
    }
}
