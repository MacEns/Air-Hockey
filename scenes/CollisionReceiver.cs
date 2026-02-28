using Godot;
using System;

public partial class CollisionReceiver : Area2D
{
    [Signal]
    public delegate void CollisionReceivedEventHandler(float velocity, Vector2 direction);
}
