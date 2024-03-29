using System;

namespace guessing_number;

public class GuessNumber
{
    //In this way we are passing the random number generator by dependency injection
    private IRandomGenerator random;
    public GuessNumber() : this(new DefaultRandom()){}
    public GuessNumber(IRandomGenerator obj)
    {
        this.random = obj;

        userValue = 0;
        randomValue = 0;
    }

    //user variables
    public int userValue;
    public int randomValue;

    public int maxAttempts = 5;
    public int currentAttempts = 0;

    public int difficultyLevel = 1;

    public bool gameOver;

    //1 - Imprima uma mensagem de saudação
    public string Greet()
    {
        string GreetMsg = "";
        GreetMsg += "---Bem-vindo ao Guessing Game--- /n";
        GreetMsg += " Para começar, tente adivinhar o número que eu pensei, entre -100 e 100!";
        return GreetMsg;
    }

    //2 - Receba a entrada da pessoa usuária e converta para Int
    //5 - Adicione um limite de tentativas
    public string ChooseNumber(string userEntry)
    {
        var convert = int.TryParse(userEntry, out userValue);
        currentAttempts++;
        if (currentAttempts > maxAttempts)
        {
            gameOver = true;
            return "Você excedeu o número máximo de tentativas! Tente novamente.";
        }
        if(!convert)
        {
            return "Entrada inválida! Não é um número.";
        }

        if(userValue > 100 || userValue < -100)
        {
            userValue = 0;
            return "Entrada inválida! Valor não está no range.";
        }
        return "Número escolhido!";
        
    }

    //3 - Gere um número aleatório
    public string RandomNumber()
    {
        randomValue = random.GetInt(-100, 100);
        return "A máquina escolheu um número de -100 à 100!";
    }

    //6 - Adicione níveis de dificuldade
    public string RandomNumberWithDifficult()
    {
        int maxRange = 100;
        if (difficultyLevel == 2)
        {
            maxRange = 500;
        }
        else if (difficultyLevel == 3)
        {
            maxRange = 1000;
        }
        randomValue = random.GetInt(-maxRange, maxRange);
        return $"A máquina escolheu um número de -{maxRange} à {maxRange}!";
    }

    //4 - Verifique a resposta da jogada
    public string AnalyzePlay()
    {
        if (userValue < randomValue)
        {
            return "Tente um número MAIOR";
        }
        else if (userValue > randomValue){
            return "Tente um número MENOR";
        }
        else
        {
            gameOver = true;
            return "ACERTOU!";
        }
    }

    //7 - Adicione uma opção para reiniciar o jogo
    public void RestartGame()
    {
        userValue = 0;
        randomValue = 0;
        currentAttempts = 0;
        gameOver = false;
        difficultyLevel = 1;
        maxAttempts = 5;
    }
}