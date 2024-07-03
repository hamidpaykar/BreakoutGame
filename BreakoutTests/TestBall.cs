namespace BreakoutTest
{

    // Setting up the test fixture for the Ball class tests
    [TestFixture]
    public class TestBall
    {
        public Block block; // Declaring a public Block object
        public Ball ball; // Declaring a public Ball object

        // Method to set up any required objects or state before all tests are run
        [OneTimeSetUp]
        public void Setup()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Galaga v0.1"
            };

            var game = new Game(windowArgs); // Creating a new game instance

            // Initializing the block with a specific shape, image, and health
            block = new Block(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", 3, 3);

            // Initializing the ball with a specific position and image
            ball = new Ball(new Vec2F(0.5f - 0.2f / 2, 0.1f), new Image(Path.Combine("Assets", "Images", "ball.png")));
        }

        // Method to set up any required objects or state before each test
        [SetUp]
        public void Setup2()
        {
            var windowArgs = new WindowArgs()
            {
                Title = "Galaga v0.1"
            };

            var game = new Game(windowArgs, false); // Creating a new game instance

            // Initializing the block with a specific shape, image, and health
            block = new Block(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                "blue-block.png", 3, 3);
        }

        // Test method to check if the firstPush function actually works and sets the isStarted field to true
        [Test]
        public void testFirstPush()
        {
            Vec2F positionPrevious = ball.Shape.Position; // Storing the previous position of the ball
            ball.firstPush(); // Calling the firstPush method on the ball
            Vec2F positionAfter = ball.Shape.Position; // Storing the position of the ball after firstPush

            // Asserting that the position of the ball has changed after the first push
            Assert.AreNotEqual(positionAfter, positionPrevious);

            // Asserting that the isStarted field of the ball is set to true after the first push
            Assert.AreEqual(true, ball.IsStarted);
        }
    }
}

// Note: This is a black box test as it verifies the functionality of the firstPush method by observing its effects on the ball's state without knowing the internal implementation details.
