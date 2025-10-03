namespace BlaisePascal.LessonsExamples.Domain.UnitTests
{
    public class EnemyTest
    {
        [Fact]
        public void EnemyName_WhenTheNameIsValid_TheNameMustBeAssignedCorrectly()
        {
            // Arrange
            Enemy newEnemy = new Enemy();

            // Act
            newEnemy.SetName("Stefano");

            // Assert
            Assert.Equal("Stefano", newEnemy.GetName());
        }
















    }
}
