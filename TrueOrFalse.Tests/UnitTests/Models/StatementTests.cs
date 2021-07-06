using TrueOrFalse.Models;
using Xunit;

namespace TrueOrFalse.Tests.UnitTests.Models
{
    public class StatementTests
    {
        [Fact]
        public void HasEqualValues_WithEqualValues_ReturnsTrue()
        {
            Statement statement1 = new("text", true);
            Statement statement2 = new("text", true);

            Assert.True(statement1.HasEqualValues(statement2));
        }

        [Fact]
        public void HasEqualValues_WithUnequalValues_ReturnsFalse()
        {
            Statement statement1 = new("text1", true);
            Statement statement2 = new("text2", false);

            Assert.False(statement1.HasEqualValues(statement2));
        }
    }
}
