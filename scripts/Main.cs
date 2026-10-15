using Godot;
using System;


public partial class Main : Node
{
	[Export] public string NomeEntrada = "Pedro";
	[Export] public int VidaEntrada = 100;
	[Export] public int AtaqueEntrada = 20;
	[Export] public int DefesaEntrada = 10;
	[Export] public int DanoEntrada = 30;
	[Export] public int Golpes = 5;

	public override void _Ready()
	{
		Personagem jogador = new Personagem(NomeEntrada, VidaEntrada, AtaqueEntrada, DefesaEntrada);

		for (int i = 1; i <= Golpes; i++)
		{
			GD.Print($"\n--- Golpe {i}: {DanoEntrada} de dano ---");

			jogador.ReceberDano(DanoEntrada);
			GD.Print(jogador.ObterStatus());

			if (!jogador.EstaVivo())
			{
				GD.Print("Unidade desativada!");
				break;
			}
		}
	}
}
