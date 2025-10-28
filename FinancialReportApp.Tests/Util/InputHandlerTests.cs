using FinancialReportApp.Resources;
using FinancialReportApp.UI;
using FinancialReportApp.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace FinancialReportApp.Tests.Util
{
    [TestClass]
    public class InputHandlerTests
    {
        InputHandler inputHandler;

        Mock<IUserInterface> mockUserInterface;
        Mock<ILocaliser> mockLocaliser;

        [TestInitialize]
        public void Setup()
        {
            mockUserInterface = new Mock<IUserInterface>();
            mockLocaliser = new Mock<ILocaliser>();

            inputHandler = new InputHandler(mockUserInterface.Object, mockLocaliser.Object);
        }

        [TestMethod]
        public void PromptInt_ValidInput_ReturnsParsedInt()
        {
            var input = "42";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(input);

            var result = inputHandler.PromptInt("Enter an integer:");

            Assert.AreEqual(42, result);
            mockUserInterface.Verify(ui => ui.Write("Enter an integer:"), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptInt_InvalidThenValidInput_ReturnsParsedInt()
        {
            var invalidInput = "abc";
            var validInput = "100";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(invalidInput)
                .Returns(validInput);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptInt_Invalid)))
                .Returns("Invalid");

            var result = inputHandler.PromptInt("Enter an integer:");

            Assert.AreEqual(100, result);
            mockUserInterface.Verify(ui => ui.Write("Enter an integer:"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("Invalid"), Times.Once);
        }

        [TestMethod]
        public void PromptNullableDecimal_ValidInput_ReturnsParsedDecimal()
        {
            var input = "123.45";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(input);

            var result = inputHandler.PromptNullableDecimal("Enter a decimal:");

            Assert.AreEqual(123.45m, result);
            mockUserInterface.Verify(ui => ui.Write("Enter a decimal:"), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptNullableDecimal_EmptyInput_ReturnsNull()
        {
            var input = "";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(input);

            var result = inputHandler.PromptNullableDecimal("Enter a decimal:");

            Assert.IsNull(result);
            mockUserInterface.Verify(ui => ui.Write("Enter a decimal:"), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptNullableDecimal_InvalidThenValidInput_ReturnsParsedDecimal()
        {
            var inputText = "Enter a decimal:";

            var invalidInput = "xyz";
            var outOfRangeInput = "1000";
            var validInput = "500";

            var outOfRangeOutput = "Between {0} and {1}";
            var invalidOutput = "Invalid";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(invalidInput)
                .Returns(outOfRangeInput)
                .Returns(validInput);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptDecimal_Invalid))).Returns(invalidOutput);
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptDecimal_OutOfBounds), It.IsAny<object[]>()))
                .Returns((ResourceManager _, string __, object[] args) => string.Format(outOfRangeOutput, args));

            var result = inputHandler.PromptNullableDecimal(inputText, 0, 600);

            Assert.AreEqual(500m, result);
            mockUserInterface.Verify(ui => ui.Write(inputText), Times.Exactly(3));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(3));
            mockUserInterface.Verify(ui => ui.WriteLine(invalidOutput), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(outOfRangeOutput, 0, 600)), Times.Once);
        }

        [TestMethod]
        public void PromptNullableDecimal_InputOutsideOfRangeThenInside()
        {
            var inputText = "Enter a decimal:";

            var outOfRangeOutput = "Between {0} and {1}";
            var invalidOutput = "Invalid";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns("-1")
                .Returns("101")
                .Returns("50");

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptDecimal_OutOfBounds), It.IsAny<object[]>()))
                .Returns((ResourceManager _, string __, object[] args) => string.Format(outOfRangeOutput, args));

            var result = inputHandler.PromptNullableDecimal(inputText, 0, 100);

            Assert.AreEqual(50m, result);
            mockUserInterface.Verify(ui => ui.Write(inputText), Times.Exactly(3));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(3));
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(outOfRangeOutput, 0, 100)), Times.Exactly(2));
        }

        [TestMethod]
        public void PromptNullableDecimal_InputExactlyAtMinMax()
        {
            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns("0")
                .Returns("100");

            var resultMin = inputHandler.PromptNullableDecimal("Enter a decimal:", 0, 100);
            Assert.AreEqual(0m, resultMin);

            var resultMax = inputHandler.PromptNullableDecimal("Enter a decimal:", 0, 100);
            Assert.AreEqual(100m, resultMax);

            mockUserInterface.Verify(ui => ui.Write("Enter a decimal:"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
        }

        [TestMethod]
        public void PromptDecimal_MultipleInvalidThenValidInput_ReturnsParsedDecimal()
        {
            var inputText = "Enter a decimal:";

            var inputs = new List<string> { "abc", "12.34.56", "-100", "200" };
            var callIndex = 0;

            var outOfRangeOutput = "Between {0} and {1}";
            var invalidOutput = "Invalid";

            mockUserInterface.Setup(ui => ui.ReadLine())
                .Returns(() => inputs[callIndex])
                .Callback(() => callIndex++);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptDecimal_Invalid)))
             .Returns(invalidOutput);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptDecimal_OutOfBounds), It.IsAny<object[]>()))
                         .Returns((ResourceManager _, string __, object[] args) => string.Format(outOfRangeOutput, args));

            var result = inputHandler.PromptDecimal(inputText, 0, 300);

            Assert.AreEqual(200m, result);
            mockUserInterface.Verify(ui => ui.Write(inputText), Times.Exactly(4));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(4));
            mockUserInterface.Verify(ui => ui.WriteLine(invalidOutput), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine(string.Format(outOfRangeOutput, 0, 300)), Times.Once);
        }

        [TestMethod]
        public void PromptDecimal_ValidInputAtBounds_ReturnsParsedDecimal()
        {
            var minInput = "0";
            var maxInput = "100";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(minInput)
                .Returns(maxInput);

            var resultMin = inputHandler.PromptDecimal("Enter a decimal:", 0, 100);
            Assert.AreEqual(0m, resultMin);

            var resultMax = inputHandler.PromptDecimal("Enter a decimal:", 0, 100);
            Assert.AreEqual(100m, resultMax);

            mockUserInterface.Verify(ui => ui.Write("Enter a decimal:"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
        }

        [TestMethod]
        public void PromptString_ValidInput_ReturnsString()
        {
            var input = "Hello, World!";

            mockUserInterface.Setup(ui => ui.ReadLine())
                .Returns(input);

            var result = inputHandler.PromptString("Enter a string:");

            Assert.AreEqual(input, result);
            mockUserInterface.Verify(ui => ui.Write("Enter a string:"), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptString_InvalidThenValidInput_ReturnsString()
        {
            var inputPrompt = "input";

            var emptyInput = "";
            var validInput = "Valid String";

            var invalidOutput = "Invalid";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(emptyInput)
                .Returns(validInput);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptString_Invalid)))
             .Returns(invalidOutput);

            var result = inputHandler.PromptString(inputPrompt);

            Assert.AreEqual(validInput, result);
            mockUserInterface.Verify(ui => ui.Write(inputPrompt), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine(invalidOutput), Times.Once);
        }

        [TestMethod]
        public void PromptEnum_ValidInput_ReturnsEnum()
        {
            var input = "2";
            var selectOption = "select";

            mockUserInterface.Setup(ui => ui.ReadLine())
                .Returns(input);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptEnum_Option)))
             .Returns(selectOption);

            var result = inputHandler.PromptEnum<TestEnum>("Enter an option (OptionOne, OptionTwo, OptionThree):");

            Assert.AreEqual(TestEnum.OptionTwo, result);
            mockUserInterface.Verify(ui => ui.WriteLine("1. OptionOne"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("2. OptionTwo"), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("3. OptionThree"), Times.Once);
            mockUserInterface.Verify(ui => ui.Write(selectOption), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
            mockUserInterface.Verify(ui => ui.WriteLine("Invalid input. Please select a valid option."), Times.Never);
        }

        [TestMethod]
        public void PromptEnum_InvalidThenValidInput_ReturnsEnum()
        {
            var invalidInput = "5";
            var input = "2";

            var invalidOutput = "invalid";
            var selectOption = "selectOption";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(invalidInput)
                .Returns(input);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptEnum_Invalid)))
             .Returns(invalidOutput);
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptEnum_Option)))
             .Returns(selectOption);

            var result = inputHandler.PromptEnum<TestEnum>("Enter an option (OptionOne, OptionTwo, OptionThree):");

            Assert.AreEqual(TestEnum.OptionTwo, result);
            mockUserInterface.Verify(ui => ui.WriteLine("1. OptionOne"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("2. OptionTwo"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine("3. OptionThree"), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.Write(selectOption), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine(invalidOutput), Times.Once);
        }

        [TestMethod]
        public void PromptYesNo_YesInput_ReturnsTrue()
        {
            var inputPrompt = "Confirm";
            var inputPromptSuffix = " (y/n): ";
            var input = "y";

            mockUserInterface.Setup(ui => ui.ReadLine())
                .Returns(input);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_QuestionSuffix)))
                .Returns(inputPromptSuffix);
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_YesInputs)))
                .Returns("y,yes");
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_NoInputs)))
                .Returns("n,no");

            var result = inputHandler.PromptYesNo(inputPrompt);

            Assert.IsTrue(result);
            mockUserInterface.Verify(ui => ui.Write(string.Concat(inputPrompt, inputPromptSuffix)), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptYesNo_NoInput_ReturnsFalse()
        {
            var inputPrompt = "Confirm";
            var inputPromptSuffix = " (y/n): ";
            var input = "n";

            mockUserInterface.Setup(ui => ui.ReadLine())
                .Returns(input);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_QuestionSuffix)))
                .Returns(inputPromptSuffix);
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_YesInputs)))
                .Returns("y,yes");
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_NoInputs)))
                .Returns("n,no");

            var result = inputHandler.PromptYesNo(inputPrompt);
            Assert.IsFalse(result);
            mockUserInterface.Verify(ui => ui.Write(string.Concat(inputPrompt, inputPromptSuffix)), Times.Once);
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Once);
        }

        [TestMethod]
        public void PromptYesNo_InvalidThenValidInput_ReturnsBool()
        {
            var inputPrompt = "Confirm";
            var inputPromptSuffix = " (y/n): ";
            var invalidInput = "maybe";
            var validInput = "y";

            var invalidOutput = "invalid";

            mockUserInterface.SetupSequence(ui => ui.ReadLine())
                .Returns(invalidInput)
                .Returns(validInput);

            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_QuestionSuffix)))
                .Returns(inputPromptSuffix);
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_YesInputs)))
                .Returns("y,yes");
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_NoInputs)))
                .Returns("n,no");
            mockLocaliser.Setup(l => l.Get(It.IsAny<ResourceManager>(), nameof(Strings_InputHandling.PromptYesNo_Invalid)))
                .Returns(invalidOutput);

            var result = inputHandler.PromptYesNo(inputPrompt);

            Assert.IsTrue(result);
            mockUserInterface.Verify(ui => ui.Write(string.Concat(inputPrompt, inputPromptSuffix)), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.ReadLine(), Times.Exactly(2));
            mockUserInterface.Verify(ui => ui.WriteLine(invalidOutput), Times.Once);
        }
    }

    public enum TestEnum
    {
        OptionOne,
        OptionTwo,
        OptionThree
    }
}
