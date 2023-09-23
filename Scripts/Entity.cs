using Godot;
using System;

public partial class Entity : Node2D
{
	public enum Polarity
	{
		Black,
		White
	}
	
	[Export]
	protected int _health = 10;
	
	[Export]
	protected Polarity _polarity = Polarity.Black;
	
	[Signal]
	public delegate void HealthChangedEventHandler( int oldValue, int newValue );
	[Signal]
	public delegate void HealthDepletedEventHandler( int oldValue, int newValue );
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthChanged += UpdateHealthLabel;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void TakeDamage( Polarity sourcePolarity, int damageAmount )
	{
		if ( sourcePolarity == _polarity )
			return;
		
		var oldHealth = _health;
		_health -= damageAmount;
		
		// EmitSignal == Invoke() in unity
		EmitSignal(SignalName.HealthChanged, oldHealth, _health ); 
		
		if ( _health <= 0 )
		{
			EmitSignal(SignalName.HealthDepleted, oldHealth, _health );
		}
	}
	
	//
	// TEST

	private void UpdateHealthLabel( int oldValue, int newValue )
	{
		var label = GetNode<Label>("../HealthLabel");
		
		if ( label == null )
			return;
			
		label.Text = "Health: " + newValue.ToString();
	}
	
	private void _on_button_pressed()
	{
		TakeDamage( Polarity.Black, 2 );
	}
}

