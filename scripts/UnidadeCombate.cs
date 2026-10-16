using Godot;
using System;

public partial class UnidadeCombate : Personagem
{
	public UnidadeCombate() : base("Unidade de Combate", 120, 25, 15){
	}
	public override string Atacar()
		{
			return  $"{Nome} executou um ataque físico (dano {Ataque})";
		}
}
