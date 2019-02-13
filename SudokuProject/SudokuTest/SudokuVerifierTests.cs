using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;

namespace SudokuTest
{
    [TestClass]
    public class SudokuVerifierTests
    {
        MockFileSystem fileSystem;
        SudokuVerifier sut;

        [TestInitialize]
        public void RunBeforeEvetyTest()
        {
            fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                                {
                                    { @"invalid-nondigit.txt", new MockFileData(File.ReadAllText("files/invalid-nondigit.txt")) },
                                    { @"invalid-nondigit-symbol.txt", new MockFileData(File.ReadAllText("files/invalid-nondigit-symbol.txt")) },
                                    { @"invalid-rowlength-less.txt", new MockFileData(File.ReadAllText("files/invalid-rowlength-less.txt")) },
                                    { @"invalid-rowlength-more.txt", new MockFileData(File.ReadAllText("files/invalid-rowlength-more.txt")) },
                                    { @"invalid-rownumber-less.txt", new MockFileData(File.ReadAllText("files/invalid-rownumber-less.txt")) },
                                    { @"invalid-rownumber-more.txt", new MockFileData(File.ReadAllText("files/invalid-rownumber-more.txt")) },
                                    { @"error-duplicate-numbers-row.txt", new MockFileData(File.ReadAllText("files/error-duplicate-numbers-row.txt")) },
                                    { @"error-duplicate-numbers-col.txt", new MockFileData(File.ReadAllText("files/error-duplicate-numbers-col.txt")) },
                                    { @"error-duplicate-numbers-block.txt", new MockFileData(File.ReadAllText("files/error-duplicate-numbers-block.txt")) },
                                    { @"input_sudoku[1].txt", new MockFileData(File.ReadAllText("files/input_sudoku[1].txt")) },

                                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FileDoesntExists()
        {
            sut = new SudokuVerifier(this.fileSystem);
            sut.IsValid("AnyFileName.txt");
        }

        [TestMethod]
        [DataRow("invalid-nondigit.txt", "All chars on each line should be a number")]
        [DataRow("invalid-nondigit-symbol.txt", "All chars on each line should be a number")]
        [DataRow("invalid-rowlength-less.txt", "Each line in Soduko file should have 9 chars")]
        [DataRow("invalid-rowlength-more.txt", "Each line in Soduko file should have 9 chars")]
        [DataRow("invalid-rownumber-less.txt", "Soduko file should have 9 lines of data")]
        [DataRow("invalid-rownumber-more.txt", "Soduko file should have 9 lines of data")]
        public void InvalidFileContent(string fileName, string expectedExceptionMessage)
        {
            sut = new SudokuVerifier(this.fileSystem);

            var ex = Assert.ThrowsException<ApplicationException>(() => sut.IsValid(fileName));

            Assert.AreEqual(ex.Message, expectedExceptionMessage);
        }

        [TestMethod]
        [DataRow("error-duplicate-numbers-row.txt")]
        [DataRow("error-duplicate-numbers-col.txt")]
        [DataRow("error-duplicate-numbers-block.txt")]
        public void DuplicateNumbers(string fileName)
        {
            sut = new SudokuVerifier(this.fileSystem);

            var result = sut.IsValid(fileName);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Valid()
        {
            sut = new SudokuVerifier(this.fileSystem);

            var result = sut.IsValid("input_sudoku[1].txt");

            Assert.IsTrue(result);
        }

    }
}
