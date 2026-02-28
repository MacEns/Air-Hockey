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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

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
}
