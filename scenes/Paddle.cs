using Godot;
using System;
using System.Diagnostics;

public partial class Paddle : CharacterBody2D
{
    [Export]
    public Puck Puck { get; set; }

    [Export]
    public float Speed { get; set; } = 250f;

    [Export]
    public float MaxSpeed { get; set; } = 300f;

    private Area2D collisionEmitter;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionEmitter = GetNode<Area2D>("CollisionEmitter");
        collisionEmitter.AreaEntered += OnCollision;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        MoveAndSlide();
        //HandleCollisions();
    }

    private void HandleInput()
    {
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();
        Velocity = direction * Speed;

    }

    private void OnCollisionReceived(float velocity, Vector2 direction)
    {
        // Handle collision event
        GD.Print($"{this.Name} - Collision received with velocity: {velocity} and direction: {direction}");
    }

    private void OnCollision(Area2D area)
    {
        if (area is CollisionReceiver collisionReceiver)
        {
            var direction = (collisionReceiver.GlobalPosition - GlobalPosition).Normalized();
            var speed = Velocity.Length() + area.GetParent<CharacterBody2D>().Velocity.Length();
            collisionReceiver.EmitSignal("CollisionReceived", speed, direction);
        }
    }
}
