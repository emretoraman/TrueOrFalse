using TrueOrFalse.Models;
using Xunit;

namespace TrueOrFalse.Tests.UnitTests.Models
{
    public class StatementTests
    {
        [Fact]
        public void Equals_WithEqualObject_ReturnsTrue()
        {
            Statement statement1 = new("text", true);
            Statement statement2 = new("text", true);

            Assert.True(statement1.Equals(statement2));
            Assert.Equal(statement1, statement2);
        }

        [Fact]
        public void Equals_WithUnequalObject_ReturnsFalse()
        {
            Statement statement1 = new("text1", true);
            Statement statement2 = new("text2", false);

            Assert.False(statement1.Equals(statement2));
            Assert.NotEqual(statement1, statement2);
        }
    }
}
