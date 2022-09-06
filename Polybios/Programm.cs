namespace Polybios;

public class Programm
{
    public static void Main()
    {
        var poly = new Polybios(Alphabet.AlphaNumeric, "Hello");

        Console.WriteLine($"Alphabet used: \n{poly.ToMatrixString()}");

        Console.WriteLine($"Encryption: {poly.Encrypt("HELLOWORLD")}");
        
        Console.WriteLine($"Decryption: {poly.Decrypt("00 01 02 02 03 34 03 25 02 11")}");

    }
}