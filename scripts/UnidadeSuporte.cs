using Godot;
using System;

public partial class UnidadeSuporte : Personagem
{
	public UnidadeSuporte() : base("Unidade de Suporte", 80, 15, 8)
	{	
	}
	public override string Atacar()
		{
			return  $"{Nome} executou um ataque tecnológico (dano {Ataque})";;
		}
}
