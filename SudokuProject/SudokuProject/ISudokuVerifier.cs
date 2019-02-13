using System;
namespace SudokuProject
{
    public interface ISudokuVerifier
    {
        bool IsValid(string fileName);
    }
}
