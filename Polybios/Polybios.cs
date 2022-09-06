using System.Text;

namespace Polybios;

public enum Alphabet
{
    Alpha,
    AlphaNumeric,
}

public class Polybios
{
    private const string Alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
    private const string AlphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
    private readonly char[,] _mat;

    public Polybios(Alphabet type, string input)
    {
        var key = input.ToUpper().Replace(" ", "");
        
        var charset = type switch
        {
            Alphabet.Alpha => Alpha.ToList(),
            Alphabet.AlphaNumeric => AlphaNum.ToList(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        var alphabet = key.Distinct().ToArray().ToList();

        charset.RemoveAll(c => alphabet.Contains(c));
        alphabet.AddRange(charset);
        
        var i = 0;
        switch (type)
        {
            case Alphabet.Alpha:
                _mat = new char[5, 5];
                for (var row = 0; row < 5; row++)
                {
                    for (var col = 0; col < 5; col++)
                    {
                        _mat[row, col] = alphabet[i++];
                    }
                }
                break;
            case Alphabet.AlphaNumeric:
                _mat = new char[6, 6];
                for (var row = 0; row < 6; row++)
                {
                    for (var col = 0; col < 6; col++)
                    {
                        _mat[row, col] = alphabet[i++];
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public string Encrypt(string msg)
    {
        var array = msg.ToCharArray();

        var sb = new StringBuilder();
        foreach (var c in array)
        {
            for (var row = 0; row < _mat.GetLength(0); row++)
            {
                for (var col = 0; col < _mat.GetLength(1); col++)
                {
                    if (_mat[row, col] == c)
                    {
                        sb.Append($"{row}{col} ");
                    }
                }
            }
        }

        return sb.ToString();
    }

    public string Decrypt(string msg)
    {
       var crypt = msg.Replace(" ", "");
       
        var res = new StringBuilder();
        
        for (var i = 0; i < crypt.Length - 1 ; i+=2)
        {
            var row = int.Parse(crypt[i].ToString());
            var col = int.Parse(crypt[i + 1].ToString());
            
            res.Append(_mat[row,col]);
        }

        return res.ToString();
    }

    public string ToMatrixString()
    {
        var res = "";

        for (var i = 0; i < _mat.GetLength(0); i++)
        {
            for (var j = 0; j < _mat.GetLength(1); j++)
            {
                res += $"{_mat[i, j]}";
            }

            res += Environment.NewLine;
        }

        return res;
    }
}