using Godot;
using System;
using System.Diagnostics;

public partial class Puck : CharacterBody2D
{
    [Export]
    public Line2D blueLine;

    [Export]
    public Line2D greenLine;

    [Export]
    public Line2D redLine;

    private float currentSpeed = 0f;
    private CollisionReceiver collisionReceiver;

    public override void _Ready()
    {
        collisionReceiver = GetNode<CollisionReceiver>("CollisionReceiver");
        collisionReceiver.CollisionReceived += OnCollisionReceived;
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity *= 0.999f;
        var collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            HandleCollisions(collision);
        }
    }

    private void HandleCollisions(KinematicCollision2D collision)
    {
        var collider = collision.GetCollider();

        if (collider is StaticBody2D boards)
        {
            OnWallCollision(collision);
        }
    }

    private void OnWallCollision(KinematicCollision2D collision)
    {
        var normal = collision.GetNormal();
        var remainder = collision.GetRemainder();
        var bounce = remainder.Bounce(normal);

        redLine.Points = new Vector2[] { collision.GetPosition() - Velocity.Normalized() * 120, collision.GetPosition() };
        blueLine.Points = new Vector2[] { collision.GetPosition(), collision.GetPosition() + normal * 100 };
        greenLine.Points = new Vector2[] { collision.GetPosition(), collision.GetPosition() + bounce.Normalized() * 140 };

        Velocity = bounce.Normalized() * currentSpeed * 0.9f;
    }

    private void OnCollisionReceived(float velocity, Vector2 direction)
    {
        // Handle collision event
        GD.Print($"{this.Name} - OnCollisionReceived with velocity: {velocity} and direction: {direction}");
        currentSpeed = velocity * 1.05f;
        Velocity = direction * currentSpeed;
    }

}
