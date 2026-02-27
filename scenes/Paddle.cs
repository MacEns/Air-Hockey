using Godot;
using System;
using System.Diagnostics;

public partial class Paddle : CharacterBody2D
{
    [Export]
    public Puck Puck { get; set; }

    [Export]
    public float Speed { get; set; } = 5f;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        HandleInput();
        MoveAndSlide();
        HandleCollisions();
    }

    private void HandleInput()
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Position += direction * Speed;
        Velocity = Vector2.Zero;
    }

    private void HandleCollisions()
    {
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            if (collision.GetCollider() is Puck puck)
            {
                OnPuckCollision(puck);
            }
        }
    }

    private void OnPuckCollision(Puck puck)
    {
        var direction = (puck.GlobalPosition - GlobalPosition).Normalized();
        var puckSpeed = Math.Max(puck.Velocity.Length(), 1);
        var paddleSpeed = Velocity.Length();
        puckSpeed += paddleSpeed; // Increase puck speed based on paddle speed
        puck.Velocity = direction * puckSpeed;
        Debug.WriteLine($"Puck collided with speed {puckSpeed} and direction {direction}");
    }
}
