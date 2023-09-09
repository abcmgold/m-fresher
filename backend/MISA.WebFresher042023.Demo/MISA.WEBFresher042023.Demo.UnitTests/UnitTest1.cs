using MISA.WebFresher042023.Demo;

namespace MISA.WEBFresher042023.Demo.UnitTests
{
    [TestFixture]
    public class CaculatorTests
    {
        [TestCase(4,5,9)]
        [TestCase(1,1,2)]
        [TestCase(int.MaxValue, 1, (long)(int.MaxValue) + 1)]
        public void Add_ValidInput_Success(int a, int b, long expectedValue)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Add(a, b);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        [TestCase(4, 5, -1)]
        [TestCase(1, 1, 0)]
        [TestCase(int.MaxValue, int.MinValue, (long)2*int.MaxValue + 1)]
        public void Sub_ValidInput_Success(int a, int b, long expectedValue)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Sub(a, b);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        [TestCase(4, 5, 20)]
        [TestCase(1, 1, 1)]
        [TestCase(int.MaxValue, int.MinValue, (long)int.MaxValue * int.MinValue)]
        public void Mul_ValidInput_Success(int a, int b, long expectedValue)
        {
            // Arrange

            // Act
            var actualResult = new Caculator().Mul(a, b);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedValue));
        }

        [Test]
        public void Div_InValidInput_Exception()
        {
            //arrange
            var a = 5;
            var b = 0;
            var expectedMessage = "Khong chia duoc cho 0";
            //action
            var handler = () => new Caculator().Div(a, b);
            //assert
            //Assert.That(handler, Throws.Exception);
            var exception = Assert.Throws<Exception>(() => handler());
            Assert.That(expectedMessage, Is.EqualTo(exception.Message));
        }
    }
}