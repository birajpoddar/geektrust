using NUnit.Framework;
using System.IO;
using GeekTrust;
using Shouldly;
using System.Collections.Generic;
using GeekTrustTests.Helper;

namespace GeekTrustTests;

class BaseTest
{
    string projDir, inputPath, envPath;

    public BaseTest()
    {
        projDir = string.Empty;
        inputPath = string.Empty;
        envPath = string.Empty;
    }

    [OneTimeSetUp]
    public void Setup()
    {
        var currDir = Directory.GetCurrentDirectory();
        projDir = Path.Combine(currDir, "..", "..", "..", "..", "GeekTrust");
        envPath = Path.Combine(projDir, ".env");

    }

    [TearDown]
    public void Reset()
    {
        Program.ErrorMessages = new List<string>();
        Program.SuccessMessages = new List<string>();
    }

    [Order(1)]
    [TestCase(1, TestName = "1st Test, Valid File without Topup")]
    [TestCase(2, TestName = "2nd Test, Valid File with Topup")]
    public void PositiveTest(int fileNum)
    {
        // Creates the Input File path
        inputPath = Path.Combine(projDir, "sample_input", $"input{fileNum}.txt");

        // Read the Console Output using reader
        using (ConsoleOutput reader = new())
        {
            // Calls Main method with the input file path
            Program.Main(inputPath, envPath);

            // Assert that Output is not Empty
            reader.GetOuput().ShouldNotBeEmpty();
        }

        // Assertions
        Program.ErrorMessages.ShouldBeEmpty();
        Program.SuccessMessages.ShouldNotBeEmpty();
    }

    [TestCase(17, TestName = "17th Test, Valid File with Topup & no Request Renewal requested")]
    public void PositiveTestWithNoOutput(int fileNum)
    {
        // Creates the Input File path
        inputPath = Path.Combine(projDir, "sample_input", $"input{fileNum}.txt");

        // Read the Console Output using reader
        using (ConsoleOutput reader = new())
        {
            // Calls Main method with the input file path
            Program.Main(inputPath, envPath);

            // Assert that Output is Empty
            reader.GetOuput().ShouldBeEmpty();
        }

        // Assertions
        Program.ErrorMessages.ShouldBeEmpty();
        Program.SuccessMessages.ShouldNotBeEmpty();
    }

    [Order(2)]
    [TestCase(3, TestName = "3rd Test, Invalid Date")]
    [TestCase(4, TestName = "4th Test, No Plans")]
    [TestCase(5, TestName = "5th Test, Duplicate Plans")]
    [TestCase(6, TestName = "6th Test, Duplicate Topups")]
    [TestCase(7, TestName = "7th Test, No Plans with Topup")]
    [TestCase(8, TestName = "8th Test, Empty File")]
    [TestCase(9, TestName = "9th Test, StartDate Missing")]
    [TestCase(10, TestName = "10th Test, Duplicate StartDate")]
    [TestCase(11, TestName = "11th Test, Invalid Arguments for Plan")]
    [TestCase(12, TestName = "12th Test, Invalid Arguments for Topup")]
    [TestCase(13, TestName = "13th Test, Invalid Topup Month")]
    [TestCase(14, TestName = "14th Test, Duplicate RequestRenewal")]
    [TestCase(15, TestName = "15th Test, Invalid Arguments for RequestRenewal")]
    [TestCase(16, TestName = "16th Test, Invalid Arguments for StartDate")]
    [TestCase(20, TestName = "20th Test, Input File doesn't Exist")]
    public void NegativeTest(int fileNum)
    {
        // Creates the Input File path
        inputPath = Path.Combine(projDir, "sample_input", $"input{fileNum}.txt");

        // Read the Console Output using reader
        using (ConsoleOutput reader = new())
        {
            // Calls Main method with the input file path
            Program.Main(inputPath, envPath);

            // Assert that Output is not Empty
            reader.GetOuput().ShouldNotBeEmpty();
        }

        // Assertions
        Program.ErrorMessages.ShouldNotBeEmpty();
        Program.SuccessMessages.ShouldBeEmpty();
    }
}
