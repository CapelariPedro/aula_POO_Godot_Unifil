using Godot;
using System;


public partial class Main : Node
{
	[Export] public string NomeEntrada = "Pedro";
	[Export] public int VidaEntrada = 100;
	[Export] public int AtaqueEntrada = 20;
	[Export] public int DefesaEntrada = 10;
	[Export(PropertyHint.Enum, "Combate,Suporte")]
	public int TipoUnidade = 1;
	public override void _Ready()
	{
		//Personagem jogador = new Personagem(NomeEntrada, VidaEntrada, AtaqueEntrada, DefesaEntrada);
		Personagem jogador;
		if (TipoUnidade == 0)
			jogador = new UnidadeCombate();
		else
			jogador = new UnidadeSuporte();
		GD.Print(jogador.ObterStatus());
		GD.Print(jogador.Atacar());
	}
}
