using Godot;
using System;

public class Personagem
{
	public string Nome { get; private set; }
	public int Vida { get; private set; }
	public int VidaMaxima { get; private set; }
	public int Ataque { get; private set; }
	public int Defesa { get; private set; }

	public Personagem(string nome, int vida, int ataque, int defesa)
	{
		Nome       = nome;
		Vida       = vida;
		VidaMaxima = vida;
		Ataque     = ataque;
		Defesa     = defesa;
	}

	public void ReceberDano(int dano)
	{
		Vida -= dano;

		if (Vida < 0)
			Vida = 0;
	}

	public bool EstaVivo()
	{
		return Vida > 0;
	}

	public string ObterStatus()
	{
		return $"Nome: {Nome}\nVida: {Vida}/{VidaMaxima}\nAtaque: {Ataque}\nDefesa: {Defesa}";
	}
}
