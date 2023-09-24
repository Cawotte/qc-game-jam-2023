using Godot;
using System;

public partial class Player : Entity
{

	private RigidBody2D _rb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rb = GetNode<RigidBody2D>("RigidBody2D");
	}

	public override void _Input(InputEvent @event)
	{
		/*
		if (@event.IsActionPressed("jump"))
		{
			//Jump();
		}
		*/
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("ui_right"))
		{
			velocity += Vector2.Right;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			velocity += Vector2.Up;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			velocity += Vector2.Left;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			velocity += Vector2.Down;
		}

		_rb.Position += velocity * _speed;
	}
}
