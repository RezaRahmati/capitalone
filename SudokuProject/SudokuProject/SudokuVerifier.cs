using System;
using System.IO.Abstractions;
using System.Linq;

namespace SudokuProject
{
    public class SudokuVerifier : ISudokuVerifier
    {
        private readonly IFileSystem fileSystem;
        public SudokuVerifier(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public bool IsValid(string fileName)
        {
            var grid = this.ReadFileContentAsGrid(fileName);

            return IsValidGrid(grid);
        }

        private int[][] ReadFileContentAsGrid(string fileName)
        {
            if (this.fileSystem.File.Exists(fileName))
            {
                var result = this.fileSystem.File.ReadAllLines(fileName).Where(i => i.Length != 0).ToArray();

                result.ToList().ForEach(i => i = i.Trim());

                if (result.Length != 9)
                {
                    throw new ApplicationException("Soduko file should have 9 lines of data");
                }

                if (result.Any(i => i.Length != 9))
                {
                    throw new ApplicationException("Each line in Soduko file should have 9 chars");
                }

                if (result.Any(i => i.Any(c => !char.IsDigit(c))))
                {
                    throw new ApplicationException("All chars on each line should be a number");
                }

                return convertToMatrix(result);
            }

            throw new ArgumentException($"Soduko file {fileName} doesn't exists");
        }

        private int[][] convertToMatrix(string[] lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines));
            }

            return lines.Select(s => s.Select(c => (int)(c - '0')).ToArray()).ToArray();
        }

        private bool IsValidGrid(int[][] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                var column = new int[9];
                var block = new int[9];
                var row = (int[])(grid[i]).Clone();

                for (int j = 0; j < 9; j++)
                {
                    column[j] = grid[j][i];
                    block[j] = grid[(i / 3) * 3 + j / 3][i * 3 % 9 + j % 3];
                }

                if (!(IsValidValues(column) && IsValidValues(row) && IsValidValues(block)))
                {
                    return false;
                }
            }
            return true;
        }


        private bool IsValidValues(int[] values)
        {
            if (values.Length != 9)
            {
                return false;
            }

            int flag = 0;
            foreach (int value in values)
            {
                if (value != 0)
                {
                    int bit = 1 << value;
                    if ((flag & bit) != 0) return false;
                    flag |= bit;
                }
            }
            return true;
        }
    }
}
