# aula_POO_Godot_Unifil
Projeto e contêudo didático que apresentado no curso de Programação Orientada a Objeto com C# na engine GODOT

## Tema: Batalha Cibernética — Aula 1 no console do Godot, Aula 2 com cena

> **Referência temática:** universo de ficção científica e cyberpunk inspirado em obras como _Neuromancer_, de William Gibson. A proposta utiliza uma batalha cibernética como contexto didático, sem reproduzir personagens, locais ou elementos específicos da obra.

---

# AULA 1 — Fundamentos de POO no console do Godot

## Objetivo

Construir, passo a passo, as classes-base da Batalha Cibernética em C# dentro de um projeto Godot, usando **apenas o console (painel Output)** para mostrar os resultados, sem cena nem interface visual. A aula apresenta classes, objetos, atributos, métodos, construtores e os quatro pilares da POO (encapsulamento, herança, polimorfismo e abstração), sempre na ordem em que a necessidade aparece.

### Como funcionam entrada e saída na Aula 1

O console do Godot (painel **Output**) **só exibe texto**: ele não lê o teclado como o `Console.ReadLine()` de um aplicativo de terminal. Por isso:

|Papel|No terminal tradicional|No console do Godot (Aula 1)|
|---|---|---|
|**Saída**|`Console.WriteLine()`|`GD.Print()` (painel Output)|
|**Entrada**|`Console.ReadLine()`|Campos `[Export]` no **Inspector** e, na etapa final, teclas|

Os campos `[Export]` aparecem no Inspector do nó `Main`; o estudante altera os valores ali e executa o projeto (**F5**) para ver o resultado no Output.

## Evolução do código

```text
Etapa 0  Projeto Godot e console
   ↓  "Como represento UMA unidade?"
Etapa 1  Classe e objeto
   ↓  "O objeto está vazio. Onde ficam os dados?"
Etapa 2  Atributos
   ↓  "A unidade só tem dados. E as ações?"
Etapa 3  Métodos
   ↓  "Configurar campo a campo é trabalhoso e propenso a erro."
Etapa 4  Construtor
   ↓  "Qualquer parte do código pode negativar a vida."
Etapa 5  Encapsulamento
   ↓  "Preciso de vários tipos de unidade sem copiar código."
Etapa 6  Herança
   ↓  "Cada tipo deve atacar de um jeito, com o mesmo comando."
Etapa 7  Polimorfismo
   ↓  "Não faz sentido existir uma unidade genérica."
Etapa 8  Abstração
   ↓
Etapa 9  Fechamento e checkpoint para a Aula 2
```

## Cronograma

|Etapa|Conteúdo|
|---|---|
|0|Projeto Godot e primeiro programa no Output|
|1|Classes e objetos|
|2|Atributos|
|3|Métodos|
|4|Construtor|
|5|Encapsulamento|
|6|Herança|
|7|Polimorfismo|
|8|Abstração|
|9|Fechamento|

## Regras de condução

1. **Um projeto só:** o que é digitado na Etapa 1 continua existindo na Etapa 8 e na Aula 2. O mesmo projeto Godot será usado nas duas aulas.
2. **Problema antes do conceito:** provocar o erro ao vivo antes de apresentar a solução.
3. **Marcos de versão (`v0` a `v8`):** salvar/commitar ao fim de cada etapa para que quem se perdeu possa retomar.
4. **As classes não imprimem nada:** elas devolvem valores; o `Main.cs` exibe com `GD.Print`.
5. **Compilar antes de executar:** a cada alteração em C#, usar **Build** (ícone de martelo ou `Alt+B`) e só então executar (`F5`). Os erros de compilação aparecem no painel inferior (MSBuild/Build).

---

## Etapa 0 — Projeto Godot e console

### Problema

Queremos testar os conceitos de POO rapidamente, sem a complexidade de montar uma interface gráfica.

### Necessidade

Um projeto Godot com um script C# que **execute código ao rodar** e **escreva no painel Output**.

### Preparação

1. Criar o projeto `BatalhaCibernetica`.
2. Criar uma cena com **um único nó** `Node`, chamado `Main`, e salvá-la em `scenes/Main.tscn`.
3. Anexar a esse nó um script C# `scripts/Main.cs`.
4. Definir `Main.tscn` como cena principal (**Project Settings → Application → Run → Main Scene**).
5. Abrir o painel **Output** (parte inferior do editor).

### Código — `Main.cs`

```csharp
using Godot;

public partial class Main : Node
{
    public override void _Ready()
    {
        GD.Print("================================");
        GD.Print("       BATALHA CIBERNÉTICA      ");
        GD.Print("================================");
    }
}
```

### Pontos de atenção

- Apenas o script ligado a um nó leva `partial` e herda de `Node`. As classes de POO que vamos criar **não são nós** e serão classes C# comuns.
- Ao executar (`F5`), abre-se uma janela vazia: o foco da aula é o **painel Output**.
- Dica: para a janela fechar sozinha depois de imprimir, acrescentar `GetTree().Quit();` ao final de `_Ready()` (exceto na Etapa 9, que usa o teclado).

### Fluxo de entrada e saída que será usado nas duas aulas

```text
Entrada (Inspector / teclas / botão)
        ↓
Código C#
        ↓
Objetos e métodos
        ↓
Processamento da regra
        ↓
Saída (painel Output / interface)
```

**Marco: `v0`**

---

## Etapa 1 — Classes e objetos

### Problema

Precisamos representar **uma unidade de combate** dentro do sistema.

### Necessidade

Um "molde" que descreva o que é uma unidade.

### Código — novo arquivo `scripts/Personagem.cs`

```csharp
public class Personagem
{
}
```

### Uso — `Main.cs`

```csharp
public override void _Ready()
{
    Personagem jogador = new Personagem();
    GD.Print("Unidade criada (ainda vazia).");
}
```

### Conceito

```text
Personagem       → classe   (molde)
jogador          → objeto   (instância)
new Personagem() → criação da instância
```

### Aplicação no jogo

Tudo o que existe na batalha será objeto: personagem, inimigo, arma, item, habilidade, projétil, fase.

### Pergunta para a turma

> "Se eu criar dois objetos `Personagem`, eles são o mesmo objeto?" (instâncias independentes)

### Problema que sobra

O objeto existe, mas **não guarda nada**.

**Marco: `v1`**

---

## Etapa 2 — Atributos

### Problema

Uma unidade precisa ter **informações**: nome, vida, ataque e defesa.

### Necessidade

Variáveis que pertençam ao objeto.

### Código — `Personagem.cs`

```csharp
public class Personagem
{
    public string Nome;
    public int Vida;
    public int Ataque;
    public int Defesa;
}
```

### Uso — Inspector alimentando o objeto

No `Main.cs`, os campos `[Export]` fazem o papel da "entrada":

```csharp
using Godot;

public partial class Main : Node
{
    [Export] public string NomeEntrada = "Operador";
    [Export] public int VidaEntrada = 100;
    [Export] public int AtaqueEntrada = 20;
    [Export] public int DefesaEntrada = 10;

    public override void _Ready()
    {
        Personagem jogador = new Personagem();

        jogador.Nome   = NomeEntrada;
        jogador.Vida   = VidaEntrada;
        jogador.Ataque = AtaqueEntrada;
        jogador.Defesa = DefesaEntrada;

        GD.Print($"Nome: {jogador.Nome}");
        GD.Print($"Vida: {jogador.Vida}");
        GD.Print($"Ataque: {jogador.Ataque}");
        GD.Print($"Defesa: {jogador.Defesa}");
    }
}
```

> Depois de **Build**, os quatro campos aparecem no Inspector ao selecionar o nó `Main`. Alterar um valor e executar de novo mostra o novo resultado.
> 
> Observação: o Inspector já impede texto onde se espera número. Em compensação, ele aceita qualquer inteiro, inclusive `-500`, o que é um bom gancho para a Etapa 5.

### Conceito

**Atributos** são os dados que representam o **estado** de um objeto.

```text
Unidade
├── Nome
├── Vida
├── Ataque
└── Defesa
```

### Aplicação no jogo

Atributos representam o estado de tudo: vida do personagem, dano da arma, quantidade do item.

### Problema que sobra

A unidade só tem **dados**. Quem faz tudo é o `Main.cs`.

**Marco: `v2`**

---

## Etapa 3 — Métodos

### Problema

Uma unidade também precisa **realizar ações**.

### Necessidade

Funções que pertençam ao objeto e usem os dados dele.

### Código — `Personagem.cs`

```csharp
public class Personagem
{
    public string Nome;
    public int Vida;
    public int Ataque;
    public int Defesa;

    public string Atacar()
    {
        return $"{Nome} realizou um ataque!";
    }

    public string ObterStatus()
    {
        return $"Nome: {Nome}\nVida: {Vida}\nAtaque: {Ataque}\nDefesa: {Defesa}";
    }
}
```

> Os métodos **devolvem texto** em vez de chamar `GD.Print`. Assim, na Aula 2, a mesma classe alimentará um `Label` sem nenhuma alteração.

### Uso — `Main.cs`

```csharp
GD.Print(jogador.ObterStatus());
GD.Print(jogador.Atacar());
```

### Conceito

**Métodos** representam **comportamentos**. Objeto = **estado (atributos) + comportamento (métodos)**.

### Aplicação no jogo

```text
Atacar()   ReceberDano()   Curar()   UsarItem()   Defender()   Mover()
```

### Problema que sobra

Criar uma unidade exige várias linhas. Se esquecermos uma, o objeto fica incompleto.

**Marco: `v3`**

---

## Etapa 4 — Construtor

### Problema

Não queremos criar uma unidade vazia e configurar tudo manualmente.

### Necessidade

Garantir que o objeto **já nasça pronto**.

### Código — `Personagem.cs`

```csharp
public Personagem(string nome, int vida, int ataque, int defesa)
{
    Nome   = nome;
    Vida   = vida;
    Ataque = ataque;
    Defesa = defesa;
}
```

`new Personagem()` sem argumentos **deixa de compilar**. Mostrar o erro ao vivo no painel de Build.

### Uso — `Main.cs`

```csharp
public override void _Ready()
{
    Personagem jogador = new Personagem(
        NomeEntrada,
        VidaEntrada,
        AtaqueEntrada,
        DefesaEntrada
    );

    GD.Print(jogador.ObterStatus());
}
```

### Conceito

O **construtor** define o **estado inicial** do objeto no momento da criação.

### Aplicação no jogo

```csharp
new Personagem("Operador", 100, 20, 10);
new Personagem("Drone Hostil", 80, 15, 5);
```

### Problema que sobra

Os atributos são `public`. Qualquer trecho do programa pode escrever `jogador.Vida = -500;`.

**Marco: `v4`**

---

## Etapa 5 — Pilar 1: Encapsulamento 

### Definição

**Encapsulamento** é o princípio de **proteger o estado interno de um objeto**, permitindo que seus dados sejam lidos ou alterados apenas pelos caminhos que a própria classe define (métodos e propriedades). Dessa forma, o objeto garante as suas próprias regras.

- **Ideia-chave:** os dados ficam protegidos; quem quiser alterá-los precisa passar por métodos que validam a mudança.
- **Em C#:** modificadores de acesso (`public`, `private`, `protected`) e propriedades com `get` / `set`.
- **Analogia:** em um caixa eletrônico ninguém mexe no cofre; o cliente pede um saque e o sistema valida o saldo antes de liberar o dinheiro.
- **Pergunta orientadora:** "Quem pode alterar este dado e sob quais regras?"

### Problema (provocar ao vivo)

```csharp
jogador.Vida = -500;
GD.Print(jogador.ObterStatus());
```

O objeto aceita um estado inválido.

### Necessidade

- Impedir alteração direta da vida.
- Centralizar a regra: "a vida nunca fica abaixo de 0 nem acima do máximo".

### Código — `Personagem.cs`

```csharp
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

    public void Curar(int quantidade)
    {
        Vida += quantidade;

        if (Vida > VidaMaxima)
            Vida = VidaMaxima;
    }

    public bool EstaVivo()
    {
        return Vida > 0;
    }

    public string Atacar() { return $"{Nome} realizou um ataque!"; }

    public string ObterStatus()
    {
        return $"Nome: {Nome}\nVida: {Vida}/{VidaMaxima}\nAtaque: {Ataque}\nDefesa: {Defesa}";
    }
}
```

A linha `jogador.Vida = -500;` **deixa de compilar**.

### Uso — sequência de golpes no Output

Como o console não lê o teclado, os campos `[Export]` definem o dano e a quantidade de golpes:

```csharp
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
```

Executar: a vida chega a 0 e **para**, nunca fica negativa.

### Conceito aplicado

O código acima aplica a definição de encapsulamento: `Vida` só muda por meio de `ReceberDano()` e `Curar()`, que validam os limites.

```text
Vida (private set)
   ↓
ReceberDano() / Curar()
   ↓
Validação (limites 0 e VidaMaxima)
   ↓
Novo valor
```

### Aplicação no jogo

Protege as regras do gameplay: vida, energia, munição, inventário.

### Pergunta para a turma

> "Se eu colocar `VidaEntrada = -500` no Inspector, o construtor aceita?" (sim: o construtor ainda não valida. Ótimo gancho para um desafio: validar os valores iniciais dentro da própria classe.)

### Problema que sobra

Só temos **um tipo** de unidade. A batalha precisa de unidades diferentes.

**Marco: `v5`**

---

## Etapa 6 — Pilar 2: Herança

### Definição

**Herança** é o mecanismo pelo qual uma classe (**derivada** ou **filha**) **reaproveita** os atributos e métodos de outra classe (**base** ou **pai**), podendo acrescentar ou especializar comportamentos. Expressa uma relação do tipo **"é um tipo de"**.

- **Ideia-chave:** o que é comum fica na classe base; cada classe filha declara apenas o que a diferencia.
- **Em C#:** `class Filha : Pai`, chamada ao construtor do pai com `: base(...)` e o modificador `protected`.
- **Analogia:** uma ficha genérica de personagem de RPG: todo personagem tem nome e vida, e cada classe (guerreiro, mago) acrescenta suas particularidades.
- **Pergunta orientadora:** "O que é comum a todos e o que é específico de cada tipo?"

### Problema

Precisamos de tipos diferentes de unidade. Mostrar a tentativa ingênua: copiar `Personagem.cs` para dois outros arquivos.

> "Se eu corrigir um erro em `ReceberDano`, em quantos arquivos preciso mexer?"

### Necessidade

Reaproveitar o que é comum e especializar apenas o que é diferente.

### Código — ajuste na classe base

Os atributos passam a ser `protected set` para que as subclasses possam defini-los:

```csharp
public class Personagem
{
    public string Nome { get; protected set; }
    public int Vida { get; private set; }
    public int VidaMaxima { get; private set; }
    public int Ataque { get; protected set; }
    public int Defesa { get; protected set; }

    public Personagem(string nome, int vida, int ataque, int defesa)
    {
        Nome       = nome;
        Vida       = vida;
        VidaMaxima = vida;
        Ataque     = ataque;
        Defesa     = defesa;
    }

    // ReceberDano, Curar, EstaVivo, Atacar e ObterStatus permanecem iguais
}
```

### Subclasses

`UnidadeCombate.cs`

```csharp
public class UnidadeCombate : Personagem
{
    public UnidadeCombate()
        : base("Unidade de Combate", 120, 25, 15)
    {
    }
}
```

`UnidadeSuporte.cs`

```csharp
public class UnidadeSuporte : Personagem
{
    public UnidadeSuporte()
        : base("Unidade de Suporte", 80, 15, 8)
    {
    }
}
```

### Uso — escolha do tipo pelo Inspector

Uma lista suspensa no Inspector substitui o menu do terminal:

```csharp
[Export(PropertyHint.Enum, "Combate,Suporte")]
public int TipoUnidade = 0;

public override void _Ready()
{
    Personagem jogador;

    if (TipoUnidade == 0)
        jogador = new UnidadeCombate();
    else
        jogador = new UnidadeSuporte();

    GD.Print(jogador.ObterStatus());
}
```

Testar `ReceberDano` e `Curar` nas duas unidades: funcionam **sem terem sido reescritos**.

### Conceito aplicado

O código acima aplica a definição de herança: `UnidadeCombate` e `UnidadeSuporte` reaproveitam tudo de `Personagem` e declaram apenas o que as diferencia.

```text
             Personagem
             /        \
    UnidadeCombate  UnidadeSuporte
```

- `: Personagem` → "é um tipo de".
- `: base(...)` → chama o construtor da classe pai.
- `protected` → visível para a classe e suas filhas, mas não para o resto do programa.

### Aplicação no jogo

```text
Inimigo
├── Drone
├── Soldado
├── Máquina
└── Chefe
```

### Problema que sobra

As duas unidades herdaram o **mesmo** `Atacar()`, mas cada uma deveria atacar de um jeito.

**Marco: `v6`**

---

## Etapa 7 — Pilar 3: Polimorfismo 

### Definição

**Polimorfismo** ("muitas formas") é a capacidade de **objetos de classes diferentes responderem à mesma operação, cada um à sua maneira**. O código trabalha com o tipo base, e o comportamento executado depende do **objeto real** naquele momento.

- **Ideia-chave:** o mesmo comando, resultados diferentes, sem precisar testar o tipo do objeto.
- **Em C#:** `virtual` na classe base e `override` nas classes filhas.
- **Analogia:** o botão "ligar" de um controle remoto: o comando é o mesmo, mas a TV acende a tela e o ar-condicionado liga o ventilador.
- **Pergunta orientadora:** "O mesmo comando deve produzir resultados diferentes dependendo de quem o executa?"

### Problema

Unidade de combate deve fazer ataque físico; unidade de suporte deve fazer ataque tecnológico. O comando deve continuar sendo `Atacar()`.

Mostrar a alternativa ruim:

```csharp
if (jogador is UnidadeCombate) { ... }
else if (jogador is UnidadeSuporte) { ... }   // cresce a cada novo tipo
```

### Necessidade

**Mesmo comando**, **resultados diferentes** conforme o objeto.

### Código

Na classe base:

```csharp
public virtual string Atacar()
{
    return $"{Nome} realizou um ataque.";
}
```

Nas subclasses:

```csharp
// UnidadeCombate
public override string Atacar()
{
    return $"{Nome} executou um ataque físico! (dano {Ataque})";
}

// UnidadeSuporte
public override string Atacar()
{
    return $"{Nome} executou um ataque tecnológico! (dano {Ataque})";
}
```

### Uso — a linha do `Main.cs` não muda

```csharp
GD.Print(jogador.Atacar());
```

Executar duas vezes, trocando o tipo no Inspector: mesma linha, resultados diferentes.

Demonstração em uma única execução:

```csharp
Personagem[] unidades = { new UnidadeCombate(), new UnidadeSuporte() };

foreach (Personagem unidade in unidades)
{
    GD.Print(unidade.Atacar());
}
```

### Conceito aplicado

O código acima aplica a definição de polimorfismo: a mesma chamada `Atacar()` produz resultados diferentes conforme o objeto real.

```text
Personagem jogador;      ← variável do tipo base
jogador.Atacar();        ← qual Atacar() executa? Depende do objeto real
```

`virtual` permite substituir; `override` substitui.

### Aplicação no jogo

O sistema de combate trabalha com `Personagem` sem conhecer cada tipo. Novos inimigos e habilidades entram sem alterar o código principal.

### Problema que sobra

Ainda é possível criar um `Personagem` genérico, e nada obriga uma nova subclasse a sobrescrever `Atacar()`.

**Marco: `v7`**

---

## Etapa 8 — Pilar 4: Abstração

### Definição

**Abstração** é representar um conceito pelo que ele tem de **essencial**, escondendo os detalhes e deixando as particularidades para as classes especializadas. Define **o que** deve existir, sem dizer **como** será feito.

- **Ideia-chave:** a classe abstrata descreve a ideia geral e obriga as filhas a completarem o que falta.
- **Em C#:** `abstract class` (não pode ser instanciada) e métodos `abstract` (sem corpo, obrigatórios nas filhas). Na Aula 2, também as interfaces.
- **Analogia:** "veículo" é uma ideia útil, mas ninguém dirige um "veículo genérico": o que existe são carros, motos e bicicletas.
- **Pergunta orientadora:** "O que todo tipo de unidade precisa saber fazer, independentemente de como faz?"

### Problema

- Uma unidade "genérica" não faz sentido no jogo.
- Uma subclasse nova poderia herdar o ataque genérico sem o programador perceber.

### Necessidade

Definir uma **estrutura comum** e **obrigar** as classes especializadas a completá-la.

### Código — `Personagem.cs`

```csharp
public abstract class Personagem
{
    public string Nome { get; protected set; }
    public int Vida { get; private set; }
    public int VidaMaxima { get; private set; }
    public int Ataque { get; protected set; }
    public int Defesa { get; protected set; }

    protected Personagem(string nome, int vida, int ataque, int defesa)
    {
        Nome       = nome;
        Vida       = vida;
        VidaMaxima = vida;
        Ataque     = ataque;
        Defesa     = defesa;
    }

    public abstract string Atacar();      // sem corpo: cada filha implementa

    public void ReceberDano(int dano)  { /* igual */ }
    public void Curar(int quantidade)  { /* igual */ }
    public bool EstaVivo()             { /* igual */ }
    public string ObterStatus()        { /* igual */ }
}
```

Mostrar ao vivo (o erro aparece no painel de Build):

```csharp
new Personagem("X", 10, 1, 1);   // ERRO: classe abstrata não pode ser instanciada
```

E criar uma subclasse **sem** `Atacar()`: o compilador reclama.

### Conceito aplicado

O código acima aplica a definição de abstração: `Personagem` representa a ideia geral (o **quê**) e `Atacar()` fica sem corpo, deixando o **como** para `UnidadeCombate` e `UnidadeSuporte`.

### Aplicação no jogo

Trabalhar com conceitos gerais (`Personagem`, `Inimigo`, `Arma`, `Habilidade`) enquanto cada tipo concreto implementa seus detalhes.

**Marco: `v8`**

---

## Etapa 9 — Fechamento (10 min)

### Programa final da Aula 1 — `Main.cs`

Uma pequena simulação **controlada pelo teclado**, que funciona como "ensaio" da batalha. A saída continua sendo só o painel Output; as teclas pressionadas na janela do jogo fazem o papel do menu do terminal:

```csharp
using Godot;

public partial class Main : Node
{
    [Export(PropertyHint.Enum, "Combate,Suporte")]
    public int TipoUnidade = 0;

    private Personagem jogador;

    public override void _Ready()
    {
        jogador = CriarUnidade();

        GD.Print("=== BATALHA CIBERNÉTICA ===");
        GD.Print(jogador.ObterStatus());
        GD.Print("\n[1] Status  [2] Atacar  [3] Receber dano  [4] Curar  [ESC] Sair");
        GD.Print("(clique na janela do jogo para dar foco ao teclado)");
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event is not InputEventKey tecla || !tecla.Pressed || tecla.Echo)
            return;

        switch (tecla.Keycode)
        {
            case Key.Key1:
                GD.Print(jogador.ObterStatus());
                break;

            case Key.Key2:
                GD.Print(jogador.Atacar());
                break;

            case Key.Key3:
                jogador.ReceberDano(30);
                GD.Print(jogador.ObterStatus());
                if (!jogador.EstaVivo())
                    GD.Print("Unidade desativada!");
                break;

            case Key.Key4:
                jogador.Curar(20);
                GD.Print(jogador.ObterStatus());
                break;

            case Key.Escape:
                GetTree().Quit();
                break;
        }
    }

    private Personagem CriarUnidade()
    {
        if (TipoUnidade == 0)
            return new UnidadeCombate();

        return new UnidadeSuporte();
    }
}
```

> Se a turma ainda não conhece `_UnhandledKeyInput`, tratar como "o Godot nos avisa quando uma tecla é pressionada"; o foco da aula continua sendo as classes. Alternativa sem teclado: executar uma sequência fixa de chamadas em `_Ready()` e terminar com `GetTree().Quit()`.

### Integração dos quatro pilares

```text
                    Personagem
                 (classe abstrata)
                       │
             ┌─────────┴─────────┐
      UnidadeCombate      UnidadeSuporte
       ataque físico      ataque tecnológico
```

|Pilar|Onde apareceu|Problema que resolveu|
|---|---|---|
|**Encapsulamento**|`Vida { get; private set; }` + `ReceberDano()`|Vida com valores inválidos|
|**Herança**|`UnidadeCombate : Personagem`|Código duplicado entre tipos|
|**Polimorfismo**|`virtual` / `override` em `Atacar()`|`if/else` por tipo|
|**Abstração**|`abstract class Personagem`|Unidade genérica sem sentido|

### Perguntas de fechamento

1. Qual conceito resolveu o problema da vida negativa? Por quê?
2. Se eu criar uma `UnidadeHacker`, o que preciso escrever? O que **não** preciso escrever?
3. Por que a linha `jogador.Atacar()` do `Main.cs` não mudou entre as Etapas 6, 7 e 8?

### Estrutura do projeto ao fim da Aula 1

```text
BatalhaCibernetica/
├── scenes/
│   └── Main.tscn            (um único nó Node, sem interface)
├── scripts/
│   ├── Main.cs              (entrada: Inspector/teclas · saída: GD.Print)
│   ├── Personagem.cs        (abstract)
│   ├── UnidadeCombate.cs
│   └── UnidadeSuporte.cs
└── project.godot
```

### Ponte para a Aula 2

> "Hoje o jogo é texto no painel Output. Na próxima aula, **o mesmo projeto e as mesmas três classes** vão ganhar uma cena, botões e barras de vida."

---

# AULA 2 — Construção da Batalha Cibernética na Godot (cena)

**Duração:** 3 horas

## Objetivo

Evoluir o projeto da Aula 1, do console para uma **cena com interface**, e construir uma **batalha por turnos funcional**, com inimigos, itens, habilidades e progressão, aplicando de novo os quatro pilares e acrescentando **interfaces C#** e **composição**.

## Cronograma

|Etapa|Conteúdo|Tempo|
|---|---|---|
|0|Recuperação dos conceitos e preparação do projeto|15 min|
|1|Montando a cena da batalha|20 min|
|2|Jogador na interface|15 min|
|3|Inimigos: `Inimigo`, `Drone`, `Mecha`|20 min|
|4|Ataque e dano na interface|15 min|
|5|Turnos e contra-ataque|20 min|
|6|Defender|10 min|
|7|Itens e inventário|20 min|
|8|Vitória, derrota, progressão e XP|20 min|
|9|Habilidades com interface (complexidade extra)|20 min|
|10|Desafio final|5 min + atividade|
||**Total**|**180 min**|

---

## Etapa 0 — Recuperação e preparação do projeto (15 min)

### Revisão rápida (perguntas orais)

- O que é classe e o que é objeto?
- Por que `Vida` tem `private set`?
- Por que `Personagem` é abstrata?
- O que muda quando chamamos `Atacar()` em uma `UnidadeCombate` ou em uma `UnidadeSuporte`?

### Preparação

1. Abrir o projeto da Aula 1 (marco `v8`).
2. **Manter** `Personagem.cs`, `UnidadeCombate.cs` e `UnidadeSuporte.cs` **sem alterações**.
3. Preservar o ensaio da Aula 1: renomear `Main.cs` para `Aula1Console.cs` (e a classe para `Aula1Console`) e `Main.tscn` para `Aula1Console.tscn`.
4. Criar uma **nova** `scenes/Main.tscn` com raiz `Control` e um novo `scripts/Main.cs` (`partial class Main : Control`).
5. Definir a nova `Main.tscn` como cena principal.
6. Compilar (**Build**) e verificar que não há erros.

### Pontos de atenção

- As classes de POO **não são nós** e, portanto, **não** levam `partial` nem herdam de `Node`. Apenas o script ligado à cena (`Main.cs`) é `partial class Main : Control`.
- Como as classes devolvem `string` e não usam `GD.Print`, **nenhuma linha precisa mudar**: o que antes ia para o Output agora vai para um `Label`. Esse é o ponto da separação de responsabilidades feita na Aula 1.
- `GD.Print(...)` continua útil para depuração.

### Estrutura de pastas

```text
BatalhaCibernetica/
├── scenes/
│   ├── Main.tscn            (nova: batalha com interface)
│   └── Aula1Console.tscn    (ensaio da Aula 1)
├── scripts/
│   ├── Main.cs
│   ├── Aula1Console.cs
│   ├── Personagem.cs
│   ├── UnidadeCombate.cs
│   ├── UnidadeSuporte.cs
│   ├── Inimigo.cs          (Etapa 3)
│   ├── Drone.cs            (Etapa 3)
│   ├── Mecha.cs            (Etapa 3)
│   ├── Item.cs             (Etapa 7)
│   └── IHabilidade.cs      (Etapa 9)
└── project.godot
```

---

## Etapa 1 — Montando a cena (20 min)

### Problema

Na Aula 1, a entrada era o Inspector/teclado e a saída era o painel Output. Em um jogo, precisamos de uma **interface visual** para o jogador.

### Necessidade

Mapear cada elemento do console a um nó do Godot.

|Console do Godot (Aula 1)|Cena (Aula 2)|
|---|---|
|`[Export]` no Inspector / teclas|`Button` pressionado / `OptionButton`|
|`GD.Print()`|`Label` / `RichTextLabel`|
|Status em texto|`Label` + `ProgressBar`|

### Árvore da cena `Main.tscn`

```text
Main (Control)
└── Layout (VBoxContainer)
    ├── PainelJogador (VBoxContainer)
    │   ├── LabelJogador   (Label)        ← %LabelJogador
    │   └── BarraJogador   (ProgressBar)  ← %BarraJogador
    ├── PainelInimigo (VBoxContainer)
    │   ├── LabelInimigo   (Label)        ← %LabelInimigo
    │   └── BarraInimigo   (ProgressBar)  ← %BarraInimigo
    ├── Log (RichTextLabel)               ← %Log
    ├── LabelXp (Label)                   ← %LabelXp
    ├── SeletorTipo (OptionButton)        ← %SeletorTipo
    ├── BotaoIniciar (Button)             ← %BotaoIniciar
    └── Acoes (HBoxContainer)
        ├── BotaoAtacar     (Button)      ← %BotaoAtacar
        ├── BotaoDefender   (Button)      ← %BotaoDefender
        ├── BotaoItem       (Button)      ← %BotaoItem
        └── BotaoHabilidade (Button)      ← %BotaoHabilidade
```

> Marcar cada nó como **Unique Name in Owner** (`%Nome`).

### Esboço da interface

```text
================================
       BATALHA CIBERNÉTICA
================================
Tipo: [ Unidade de Combate ▼ ]  [ INICIAR ]

JOGADOR   Unidade de Combate
[██████████░░] 120/120

INIMIGO   Drone Hostil
[██████████░░] 100/100

> Log da batalha...

XP: 0
[ATACAR] [DEFENDER] [ITEM] [HABILIDADE]
================================
```

### Código — esqueleto do `Main.cs`

```csharp
using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Control
{
    private Label labelJogador, labelInimigo, labelXp;
    private ProgressBar barraJogador, barraInimigo;
    private RichTextLabel log;
    private OptionButton seletorTipo;
    private Button botaoIniciar, botaoAtacar, botaoDefender, botaoItem, botaoHabilidade;

    public override void _Ready()
    {
        labelJogador    = GetNode<Label>("%LabelJogador");
        labelInimigo    = GetNode<Label>("%LabelInimigo");
        labelXp         = GetNode<Label>("%LabelXp");
        barraJogador    = GetNode<ProgressBar>("%BarraJogador");
        barraInimigo    = GetNode<ProgressBar>("%BarraInimigo");
        log             = GetNode<RichTextLabel>("%Log");
        seletorTipo     = GetNode<OptionButton>("%SeletorTipo");
        botaoIniciar    = GetNode<Button>("%BotaoIniciar");
        botaoAtacar     = GetNode<Button>("%BotaoAtacar");
        botaoDefender   = GetNode<Button>("%BotaoDefender");
        botaoItem       = GetNode<Button>("%BotaoItem");
        botaoHabilidade = GetNode<Button>("%BotaoHabilidade");

        seletorTipo.AddItem("Unidade de Combate");   // índice 0
        seletorTipo.AddItem("Unidade de Suporte");   // índice 1

        botaoIniciar.Pressed    += OnIniciar;
        botaoAtacar.Pressed     += OnAtacar;
        botaoDefender.Pressed   += OnDefender;
        botaoItem.Pressed       += OnItem;
        botaoHabilidade.Pressed += OnHabilidade;
    }

    private void OnIniciar() { }
    private void OnAtacar() { }
    private void OnDefender() { }
    private void OnItem() { }
    private void OnHabilidade() { }
}
```

**Marco: `g1`**

---

## Etapa 2 — Jogador na interface (15 min)

### Problema

O botão **Iniciar** precisa criar o jogador escolhido e mostrar seu estado, como o `CriarUnidade()` da Aula 1 fazia com o Inspector.

### Código — `Main.cs`

```csharp
private Personagem jogador;

private void OnIniciar()
{
    if (seletorTipo.Selected == 0)
        jogador = new UnidadeCombate();
    else
        jogador = new UnidadeSuporte();

    Escrever($"{jogador.Nome} entrou na batalha!");
    AtualizarInterface();
}

private void AtualizarInterface()
{
    if (jogador != null)
    {
        labelJogador.Text   = $"JOGADOR   {jogador.Nome}   ATK {jogador.Ataque}   DEF {jogador.Defesa}";
        barraJogador.MaxValue = jogador.VidaMaxima;
        barraJogador.Value    = jogador.Vida;
    }
}

private void Escrever(string mensagem)
{
    log.AppendText(mensagem + "\n");
}
```

### Conceito reforçado

**Polimorfismo:** a variável é `Personagem`, mas o objeto real depende do que foi escolhido no `OptionButton`. O `Main.cs` nunca precisa saber qual é.

**Marco: `g2`**

---

## Etapa 3 — Inimigos: `Inimigo`, `Drone`, `Mecha` (20 min)

### Problema

O jogador precisa de adversários, e cada um deve atacar de um jeito. Todos devem poder ser tratados como `Personagem` no combate.

### Necessidade

Uma classe intermediária para as características comuns de todo inimigo (por exemplo, a recompensa de XP) e classes concretas para cada tipo.

### Código

`Inimigo.cs`

```csharp
public abstract class Inimigo : Personagem
{
    public int RecompensaXp { get; protected set; }

    protected Inimigo(string nome, int vida, int ataque, int defesa, int recompensaXp)
        : base(nome, vida, ataque, defesa)
    {
        RecompensaXp = recompensaXp;
    }
}
```

`Drone.cs`

```csharp
public class Drone : Inimigo
{
    public Drone()
        : base("Drone Hostil", 100, 15, 5, 100)
    {
    }

    public override string Atacar()
    {
        return $"{Nome} dispara um pulso de energia!";
    }
}
```

`Mecha.cs`

```csharp
public class Mecha : Inimigo
{
    public Mecha()
        : base("Mecha", 160, 22, 12, 200)
    {
    }

    public override string Atacar()
    {
        return $"{Nome} realiza um ataque pesado!";
    }
}
```

### Hierarquia

```text
                Personagem (abstract)
               /          |          \
   UnidadeCombate  UnidadeSuporte   Inimigo (abstract)
                                      /     \
                                   Drone   Mecha
```

### Uso — `Main.cs`

```csharp
private Inimigo inimigo;

private void OnIniciar()
{
    // ... criação do jogador (Etapa 2)

    inimigo = new Drone();
    Escrever($"{inimigo.Nome} apareceu!");
    AtualizarInterface();
}
```

E em `AtualizarInterface()`:

```csharp
if (inimigo != null)
{
    labelInimigo.Text   = $"INIMIGO   {inimigo.Nome}   ATK {inimigo.Ataque}   DEF {inimigo.Defesa}";
    barraInimigo.MaxValue = inimigo.VidaMaxima;
    barraInimigo.Value    = inimigo.Vida;
}
```

### Conceito reforçado

- **Herança em vários níveis** (`Personagem` → `Inimigo` → `Drone`).
- **Abstração:** `Inimigo` continua abstrata, pois ainda não implementa `Atacar()`.

**Marco: `g3`**

---

## Etapa 4 — Ataque e dano na interface (15 min)

### Problema

O botão **Atacar** deve reduzir a vida do inimigo e mostrar o resultado.

### Ajuste em `Personagem.cs`: `ReceberDano` passa a devolver o dano efetivo

```csharp
public int ReceberDano(int dano)
{
    int vidaAntes = Vida;

    Vida -= dano;

    if (Vida < 0)
        Vida = 0;

    return vidaAntes - Vida;      // dano realmente sofrido
}
```

> O encapsulamento continua intacto: apenas a própria classe altera `Vida`. A chamada da Aula 1 (`jogador.ReceberDano(30);`) continua válida, pois o valor devolvido pode ser ignorado.

### Código — `Main.cs`

```csharp
private void OnAtacar()
{
    if (jogador == null || !jogador.EstaVivo() || !inimigo.EstaVivo())
        return;

    Escrever(jogador.Atacar());

    int dano = inimigo.ReceberDano(jogador.Ataque);
    Escrever($"{inimigo.Nome} sofreu {dano} de dano.");

    AtualizarInterface();
}
```

### Aplicação

```text
Drone Hostil   Vida 100
   ↓ ataque de 25
Drone Hostil   Vida 75
```

**Marco: `g4`**

---

## Etapa 5 — Turnos e contra-ataque (20 min)

### Problema

Hoje o jogador pode atacar quantas vezes quiser, e o inimigo nunca reage.

### Necessidade

Alternar turnos e impedir ações fora da vez.

### Código — `Main.cs`

```csharp
private bool turnoJogador = true;

private void OnAtacar()
{
    if (!turnoJogador || jogador == null || !jogador.EstaVivo() || !inimigo.EstaVivo())
        return;

    Escrever(jogador.Atacar());
    int dano = inimigo.ReceberDano(jogador.Ataque);
    Escrever($"{inimigo.Nome} sofreu {dano} de dano.");

    FinalizarTurnoJogador();
}

private void FinalizarTurnoJogador()
{
    turnoJogador = false;
    AtualizarInterface();

    if (inimigo.EstaVivo())
        TurnoInimigo();
    else
        VerificarFimDeBatalha();
}

private void TurnoInimigo()
{
    Escrever(inimigo.Atacar());

    int dano = jogador.ReceberDano(inimigo.Ataque);
    Escrever($"{jogador.Nome} sofreu {dano} de dano.");

    turnoJogador = true;
    AtualizarInterface();
    VerificarFimDeBatalha();
}
```

### Fluxo

```text
Jogador ataca
     ↓
Inimigo está vivo?
   sim → inimigo contra-ataca → volta o turno ao jogador
   não → fim da batalha
```

### Observação

Para o contra-ataque não ser instantâneo e ficar legível, pode-se usar um pequeno atraso com `GetTree().CreateTimer(0.8).Timeout` e só então chamar `TurnoInimigo()`. É um bom momento para mostrar o `await ToSignal(...)` como curiosidade, sem exigir que a turma domine o assunto.

**Marco: `g5`**

---

## Etapa 6 — Defender (10 min)

### Problema

O atributo `Defesa`, criado na Aula 1, ainda **não tem efeito**. Queremos que **Defender** reduza o próximo dano recebido.

### Necessidade

Um estado temporário no personagem, controlado pela própria classe.

### Ajuste em `Personagem.cs`

```csharp
public bool Defendendo { get; private set; }

public void Defender()
{
    Defendendo = true;
}

public int ReceberDano(int dano)
{
    if (Defendendo)
    {
        dano = Math.Max(1, dano - Defesa);   // precisa de "using System;"
        Defendendo = false;
    }

    int vidaAntes = Vida;

    Vida -= dano;

    if (Vida < 0)
        Vida = 0;

    return vidaAntes - Vida;
}
```

### Código — `Main.cs`

```csharp
private void OnDefender()
{
    if (!turnoJogador || !jogador.EstaVivo() || !inimigo.EstaVivo())
        return;

    jogador.Defender();
    Escrever($"{jogador.Nome} ativou a defesa!");

    FinalizarTurnoJogador();
}
```

### Conceito reforçado

**Encapsulamento:** `Defendendo` só é alterado por métodos da própria classe. O `Main.cs` apenas pede `Defender()`.

**Marco: `g6`**

---

## Etapa 7 — Itens e inventário (20 min)

### Problema

O jogador precisa recuperar vida durante a batalha, e queremos vários tipos de item com efeitos diferentes.

### Necessidade

Reaproveitar a ideia de herança e polimorfismo, agora para itens.

### Código

`Item.cs`

```csharp
public abstract class Item
{
    public string Nome { get; protected set; }

    protected Item(string nome)
    {
        Nome = nome;
    }

    public abstract string Usar(Personagem alvo);
}

public class Nanorreparador : Item
{
    private readonly int cura;

    public Nanorreparador() : base("Nanorreparador")
    {
        cura = 30;
    }

    public override string Usar(Personagem alvo)
    {
        alvo.Curar(cura);
        return $"{alvo.Nome} recuperou até {cura} de vida com {Nome}.";
    }
}

public class Sobrecarga : Item
{
    public Sobrecarga() : base("Sobrecarga")
    {
    }

    public override string Usar(Personagem alvo)
    {
        alvo.AumentarAtaque(5);
        return $"{alvo.Nome} aumentou o ataque em 5 com {Nome}!";
    }
}
```

Em `Personagem.cs` (mantendo o encapsulamento):

```csharp
public void AumentarAtaque(int valor)
{
    Ataque += valor;
}
```

### Inventário no `Main.cs`

```csharp
private readonly List<Item> inventario = new List<Item>();

// em OnIniciar():
inventario.Clear();
inventario.Add(new Nanorreparador());
inventario.Add(new Nanorreparador());
inventario.Add(new Sobrecarga());

private void OnItem()
{
    if (!turnoJogador || !jogador.EstaVivo() || !inimigo.EstaVivo())
        return;

    if (inventario.Count == 0)
    {
        Escrever("Inventário vazio!");
        return;
    }

    Item item = inventario[0];
    inventario.RemoveAt(0);

    Escrever(item.Usar(jogador));

    FinalizarTurnoJogador();
}
```

### Fluxo

```text
Item no inventário
        ↓
   Usar item
        ↓
Efeito (cura ou ataque)
        ↓
Fim do turno do jogador
```

### Conceito reforçado

- **Abstração + polimorfismo:** `inventario` é uma `List<Item>`, e cada item sabe o que fazer ao ser usado.
- **Lista de objetos:** coleção de instâncias de tipos diferentes tratadas de forma uniforme.

### Pergunta para a turma

> "Para criar um item novo (por exemplo, `EscudoDeEnergia`), quais arquivos preciso modificar?" (resposta esperada: nenhum, só criar a nova classe e adicioná-la ao inventário)

**Marco: `g7`**

---

## Etapa 8 — Vitória, derrota, progressão e XP (20 min)

### Problema

Quando alguém chega a 0 de vida, a batalha precisa acabar. Se o jogador vencer, deve haver um próximo adversário.

### Necessidade

Verificar o fim da batalha e controlar uma **fila de inimigos**.

### Código — `Main.cs`

```csharp
private int experiencia = 0;
private int indiceInimigo = 0;

private readonly Func<Inimigo>[] fila =
{
    () => new Drone(),
    () => new Mecha()
};

private void VerificarFimDeBatalha()
{
    if (!jogador.EstaVivo())
    {
        Escrever("Unidade do jogador desativada! FIM DE JOGO.");
        DesativarAcoes();
        return;
    }

    if (!inimigo.EstaVivo())
    {
        experiencia += inimigo.RecompensaXp;
        Escrever($"{inimigo.Nome} neutralizado! +{inimigo.RecompensaXp} XP");

        indiceInimigo++;

        if (indiceInimigo >= fila.Length)
        {
            Escrever("Todos os adversários foram derrotados. VITÓRIA!");
            DesativarAcoes();
        }
        else
        {
            inimigo = fila[indiceInimigo]();
            Escrever($"{inimigo.Nome} apareceu!");
            turnoJogador = true;
        }
    }

    AtualizarInterface();
}

private void DesativarAcoes()
{
    botaoAtacar.Disabled     = true;
    botaoDefender.Disabled   = true;
    botaoItem.Disabled       = true;
    botaoHabilidade.Disabled = true;
}
```

Em `AtualizarInterface()`:

```csharp
labelXp.Text = $"Experiência: {experiencia} XP";
```

Em `OnIniciar()` (reiniciar a partida):

```csharp
experiencia = 0;
indiceInimigo = 0;
inimigo = fila[0]();
```

> O código usa `Func<Inimigo>` para criar o inimigo certo a cada rodada. Se a turma ainda não conhece, explicar como "uma lista de receitas para fabricar inimigos". Uma alternativa mais simples para turmas iniciantes é um `if (indiceInimigo == 0) new Drone() else new Mecha()`.

### Progressão

```text
Batalha 1
   ↓
Drone neutralizado → +100 XP
   ↓
Próximo adversário
   ↓
Mecha
   ↓
Nova batalha → +200 XP
```

### Conceito reforçado

**Polimorfismo:** o sistema de combate só enxerga `Inimigo`. Quem é o inimigo da vez é decidido na fila, e o restante do código não muda.

**Marco: `g8`**

---

## Etapa 9 — Habilidades com interface (complexidade extra, 20 min)

### Problema

Queremos **habilidades especiais** que custem energia. Elas não pertencem a uma só hierarquia de classes: qualquer personagem poderia usá-las.

### Necessidade

Um **contrato** que defina o que toda habilidade deve ter, sem impor uma classe-pai.

### Conceito novo

**Interface** em C#: define o que uma classe deve oferecer, sem dizer como. Diferente da classe abstrata, uma classe pode implementar várias interfaces e não precisa estar na mesma hierarquia.

### Código

Em `Personagem.cs`:

```csharp
public int Energia { get; private set; } = 50;
public int EnergiaMaxima { get; private set; } = 50;

public bool GastarEnergia(int custo)
{
    if (Energia < custo)
        return false;

    Energia -= custo;
    return true;
}

public void RecuperarEnergia(int quantidade)
{
    Energia = Math.Min(EnergiaMaxima, Energia + quantidade);
}
```

`IHabilidade.cs`

```csharp
public interface IHabilidade
{
    string Nome { get; }
    int Custo { get; }
    string Usar(Personagem usuario, Personagem alvo);
}

public class Hackeamento : IHabilidade
{
    public string Nome => "Hackeamento";
    public int Custo => 30;

    public string Usar(Personagem usuario, Personagem alvo)
    {
        int dano = alvo.ReceberDano(usuario.Ataque * 2);
        return $"{usuario.Nome} usou {Nome}! {alvo.Nome} sofreu {dano} de dano.";
    }
}

public class ProtocoloReparo : IHabilidade
{
    public string Nome => "Protocolo de Reparo";
    public int Custo => 20;

    public string Usar(Personagem usuario, Personagem alvo)
    {
        usuario.Curar(25);
        return $"{usuario.Nome} usou {Nome} e recuperou vida!";
    }
}
```

### Composição no `Main.cs` — o jogador "tem" habilidades

```csharp
private readonly List<IHabilidade> habilidades = new List<IHabilidade>
{
    new Hackeamento(),
    new ProtocoloReparo()
};

private int habilidadeAtual = 0;

private void OnHabilidade()
{
    if (!turnoJogador || !jogador.EstaVivo() || !inimigo.EstaVivo())
        return;

    IHabilidade habilidade = habilidades[habilidadeAtual];

    if (!jogador.GastarEnergia(habilidade.Custo))
    {
        Escrever($"Energia insuficiente para {habilidade.Nome}!");
        return;
    }

    Escrever(habilidade.Usar(jogador, inimigo));

    // alterna a habilidade selecionada a cada uso (ou use um OptionButton)
    habilidadeAtual = (habilidadeAtual + 1) % habilidades.Count;

    FinalizarTurnoJogador();
}
```

Mostrar a energia na interface (`Label` ou segunda `ProgressBar`) em `AtualizarInterface()`.

### Herança × interface × composição

```text
Herança     → "é um tipo de"      Drone é um Inimigo
Interface   → "sabe fazer"        Hackeamento sabe Usar()
Composição  → "tem"               o jogador tem uma lista de habilidades
```

### Pergunta para a turma

> "Por que `Hackeamento` não deveria ser uma subclasse de `Personagem`?"

**Marco: `g9`**

---

## Etapa 10 — Desafio final (5 min + atividade)

Adicionar **pelo menos uma** nova mecânica à batalha. Sugestões, em ordem crescente de dificuldade:

**Básico**

- novo tipo de unidade (`UnidadeHacker`);
- novo inimigo (`Sentinela`);
- novo item (`EscudoDeEnergia`);
- nova mensagem de vitória/derrota.

**Intermediário**

- ataque crítico (usar `Random` em `UnidadeCombate.Atacar()`);
- recuperação de energia a cada turno;
- sistema de níveis a partir da experiência;
- seleção de habilidade com `OptionButton`.

**Avançado**

- nova interface `IEfeito` (veneno, lentidão) aplicada a `Personagem`;
- inimigo chefe com duas fases;
- nova cena (menu inicial, tela de vitória) com troca de cena;
- salvamento simples de XP em arquivo.

---

# Avaliação prática

O estudante deverá entregar um projeto Godot com uma batalha funcional demonstrando:

- criação de classes e objetos;
- atributos e métodos;
- construtores;
- encapsulamento (estado protegido);
- herança (pelo menos dois níveis);
- polimorfismo (`override` usado pelo sistema de combate);
- abstração (classe abstrata);
- uso de interface **ou** coleção de objetos polimórficos;
- interação entre objetos (jogador × inimigo × itens);
- interface mínima de jogo com cena, botões e feedback visual;
- **uma** mecânica nova, do desafio final.

---

# Resultado esperado

Ao final das duas aulas, o estudante deverá compreender que a Programação Orientada a Objetos serve para estruturar sistemas de jogos, e que a **mesma lógica** pode funcionar em interfaces diferentes (console do Godot e cena).

```text
Objeto
   ↓
Estado + comportamento
   ↓
Interação entre objetos
   ↓
Regras do jogo
   ↓
Gameplay
```

---

# Mensagem central

> **Programação Orientada a Objetos é uma forma de organizar o código para representar objetos, comportamentos e relações. Uma boa organização separa a lógica do jogo da interface: as mesmas classes funcionam no console do Godot e na cena, e podem crescer com novos personagens, inimigos, itens e habilidades sem reescrever o sistema.**

---

# Observações sobre os ajustes feitos no material original

1. **Aula 1 no console do Godot:** a saída usa `GD.Print` (painel Output). Como o Output não lê o teclado, a entrada é simulada pelo Inspector (campos `[Export]`) e, na etapa final, por teclas. As classes devolvem valores e nunca imprimem, o que permite usá-las sem alteração na cena da Aula 2.
2. **Um único projeto Godot nas duas aulas:** a Aula 2 evolui o projeto da Aula 1 (o ensaio em console é preservado como `Aula1Console`), em vez de migrar classes de outro projeto.
3. **`Atacar()` acessando `labelResultado` dentro da classe:** removido. Classe de domínio não conhece interface.
4. **`Inimigo` concreta e depois `Drone : Inimigo` vazio:** o original tinha as duas ideias misturadas. Agora `Inimigo` é abstrata (com `RecompensaXp`), e `Drone` e `Mecha` implementam o próprio ataque.
5. **`Curar()` com limite fixo de 100:** substituído por `VidaMaxima`, criada na Etapa 5 da Aula 1 (a unidade de combate tem 120 de vida).
6. **`Defesa` sem efeito:** passa a ter função na Aula 2, no botão **Defender**.
7. **Complexidade extra na Aula 2:** `Item` abstrato, `IHabilidade` (interface), energia e composição (lista de habilidades), seguindo a intenção de aprofundar a segunda aula.
8. **`Func<Inimigo>` na fila de inimigos** é opcional. Para uma turma iniciante, trocar por `if/else` mantém o foco em POO.