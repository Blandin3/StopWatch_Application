using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StopwatchApplication;

namespace StopwatchApplication.Tests
{
    /// <summary>
    /// Unit tests for the StopwatchEngine class
    /// Following Test-Driven Development (TDD) approach
    /// Tests all functionalities: Start, Pause, Resume, Reset, Stop
    /// </summary>
    [TestClass]
    public class StopwatchEngineTests
    {
        private StopwatchEngine stopwatch;

        /// <summary>
        /// Initializes test setup before each test
        /// Creates a fresh stopwatch instance for each test
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            stopwatch = new StopwatchEngine();
        }

        /// <summary>
        /// Tests that stopwatch initializes with zero time
        /// Verifies initial state is stopped at 00:00:00
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldInitializeWithZeroTime()
        {
            // Arrange & Act
            var sw = new StopwatchEngine();

            // Assert
            Assert.AreEqual(0, sw.ElapsedSeconds, 0.1, "Stopwatch should start at 0 seconds");
            Assert.IsFalse(sw.IsRunning, "Stopwatch should not be running initially");
            Assert.AreEqual("00:00:00", sw.GetFormattedTime(), "Stopwatch should display 00:00:00");
        }

        /// <summary>
        /// Tests that Start method begins timing from 00:00:00
        /// Verifies stopwatch increments time after starting
        /// </summary>
        [TestMethod]
        public void Start_ShouldBeginTimingFromZero()
        {
            // Act
            stopwatch.Start();
            Thread.Sleep(1000); // Wait 1 second

            // Assert
            Assert.IsTrue(stopwatch.IsRunning, "Stopwatch should be running after Start");
            Assert.IsTrue(stopwatch.ElapsedSeconds >= 0.9 && stopwatch.ElapsedSeconds <= 1.2, 
                "Stopwatch should show approximately 1 second elapsed");
        }

        /// <summary>
        /// Tests that Pause method stops timing and maintains current time
        /// Verifies time doesn't increment after pausing
        /// </summary>
        [TestMethod]
        public void Pause_ShouldStopTimingAndMaintainCurrentTime()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);

            // Act
            stopwatch.Pause();
            double pausedTime = stopwatch.ElapsedSeconds;
            Thread.Sleep(500);

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should not be running after Pause");
            Assert.AreEqual(pausedTime, stopwatch.ElapsedSeconds, 0.2, 
                "Elapsed time should not change after pausing");
        }

        /// <summary>
        /// Tests that Resume continues from paused time
        /// Verifies time accumulates correctly after resuming
        /// </summary>
        [TestMethod]
        public void Resume_ShouldContinueFromPausedTime()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);
            stopwatch.Pause();
            double pausedTime = stopwatch.ElapsedSeconds;

            // Act
            stopwatch.Resume();
            Thread.Sleep(1000);

            // Assert
            Assert.IsTrue(stopwatch.IsRunning, "Stopwatch should be running after Resume");
            Assert.IsTrue(stopwatch.ElapsedSeconds >= pausedTime + 0.9, 
                "Elapsed time should continue from paused time");
        }

        /// <summary>
        /// Tests that Reset returns stopwatch to 00:00:00
        /// Verifies all elapsed time is cleared
        /// </summary>
        [TestMethod]
        public void Reset_ShouldReturnToZeroTime()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);
            stopwatch.Pause();

            // Act
            stopwatch.Reset();

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should not be running after Reset");
            Assert.AreEqual(0, stopwatch.ElapsedSeconds, 0.1, "Elapsed time should be 0 after Reset");
            Assert.AreEqual("00:00:00", stopwatch.GetFormattedTime(), "Time should display 00:00:00");
        }

        /// <summary>
        /// Tests that Stop stops timing and maintains final time
        /// Verifies stopped time doesn't change
        /// </summary>
        [TestMethod]
        public void Stop_ShouldStopTimingAndMaintainFinalTime()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);

            // Act
            stopwatch.Stop();
            double stoppedTime = stopwatch.ElapsedSeconds;
            Thread.Sleep(500);

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should not be running after Stop");
            Assert.AreEqual(stoppedTime, stopwatch.ElapsedSeconds, 0.2, 
                "Elapsed time should not change after stopping");
        }

        /// <summary>
        /// Tests time formatting for seconds only
        /// Verifies correct HH:MM:SS format for short durations
        /// </summary>
        [TestMethod]
        public void GetFormattedTime_ShouldFormatSecondsCorrectly()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(5000); // 5 seconds
            stopwatch.Pause();

            // Act
            string formattedTime = stopwatch.GetFormattedTime();

            // Assert
            Assert.AreEqual("00:00:05", formattedTime, "Time should be formatted as 00:00:05");
        }

        /// <summary>
        /// Tests time formatting structure
        /// Verifies format is always HH:MM:SS with colons
        /// </summary>
        [TestMethod]
        public void GetFormattedTime_ShouldHaveCorrectFormat()
        {
            // Act
            string formattedTime = stopwatch.GetFormattedTime();

            // Assert
            Assert.IsTrue(formattedTime.Contains(":"), "Time should contain colon separators");
            Assert.AreEqual(8, formattedTime.Length, "Time should be 8 characters (HH:MM:SS)");
            Assert.AreEqual(3, formattedTime.Split(':').Length, "Time should have 3 parts (hours:minutes:seconds)");
        }

        /// <summary>
        /// Tests that multiple pause/resume cycles work correctly
        /// Verifies time accumulates properly through multiple cycles
        /// </summary>
        [TestMethod]
        public void MultiplePauseResumeCycles_ShouldAccumulateTimeCorrectly()
        {
            // Arrange & Act
            stopwatch.Start();
            Thread.Sleep(1000); // 1 second
            stopwatch.Pause();
            
            stopwatch.Resume();
            Thread.Sleep(1000); // Another 1 second
            stopwatch.Pause();

            // Assert
            Assert.IsTrue(stopwatch.ElapsedSeconds >= 1.8 && stopwatch.ElapsedSeconds <= 2.3, 
                "Total time should be approximately 2 seconds after two 1-second cycles");
        }

        /// <summary>
        /// Tests that Start cannot restart when already running
        /// Verifies calling Start multiple times doesn't reset the timer
        /// </summary>
        [TestMethod]
        public void Start_WhenAlreadyRunning_ShouldNotRestart()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);
            double timeAfterFirstSecond = stopwatch.ElapsedSeconds;

            // Act
            stopwatch.Start(); // Try to start again
            Thread.Sleep(500);

            // Assert
            Assert.IsTrue(stopwatch.ElapsedSeconds >= timeAfterFirstSecond + 0.4, 
                "Time should continue incrementing, not restart");
        }

        /// <summary>
        /// Tests that Resume does nothing when elapsed time is zero
        /// Verifies Resume only works after stopwatch has been started
        /// </summary>
        [TestMethod]
        public void Resume_WithZeroTime_ShouldNotStart()
        {
            // Act
            stopwatch.Resume();
            Thread.Sleep(500);

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should not start from Resume with zero time");
            Assert.AreEqual(0, stopwatch.ElapsedSeconds, 0.1, "Time should remain at 0");
        }

        /// <summary>
        /// Tests Reset while stopwatch is running
        /// Verifies Reset works regardless of running state
        /// </summary>
        [TestMethod]
        public void Reset_WhileRunning_ShouldStopAndResetToZero()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(1000);

            // Act
            stopwatch.Reset();

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should be stopped after Reset");
            Assert.AreEqual(0, stopwatch.ElapsedSeconds, 0.1, "Time should be 0 after Reset");
        }

        /// <summary>
        /// Tests ElapsedSeconds property accuracy
        /// Verifies elapsed time is calculated correctly
        /// </summary>
        [TestMethod]
        public void ElapsedSeconds_ShouldReturnAccurateValue()
        {
            // Arrange
            stopwatch.Start();
            Thread.Sleep(2000);
            stopwatch.Pause();

            // Act
            double elapsed = stopwatch.ElapsedSeconds;

            // Assert
            Assert.IsTrue(elapsed >= 1.8 && elapsed <= 2.3, 
                "Elapsed seconds should be approximately 2 seconds");
        }

        /// <summary>
        /// Tests that Pause when not running doesn't cause errors
        /// Verifies method handles invalid state gracefully
        /// </summary>
        [TestMethod]
        public void Pause_WhenNotRunning_ShouldNotCauseError()
        {
            // Act
            stopwatch.Pause();

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should remain stopped");
            Assert.AreEqual(0, stopwatch.ElapsedSeconds, 0.1, "Time should remain at 0");
        }

        /// <summary>
        /// Tests that Stop when not running doesn't cause errors
        /// Verifies method handles invalid state gracefully
        /// </summary>
        [TestMethod]
        public void Stop_WhenNotRunning_ShouldNotCauseError()
        {
            // Act
            stopwatch.Stop();

            // Assert
            Assert.IsFalse(stopwatch.IsRunning, "Stopwatch should remain stopped");
            Assert.AreEqual(0, stopwatch.ElapsedSeconds, 0.1, "Time should remain at 0");
        }

        /// <summary>
        /// Tests IsRunning property accuracy
        /// Verifies property reflects actual stopwatch state
        /// </summary>
        [TestMethod]
        public void IsRunning_ShouldReflectActualState()
        {
            // Initial state
            Assert.IsFalse(stopwatch.IsRunning, "Initially should not be running");

            // After Start
            stopwatch.Start();
            Assert.IsTrue(stopwatch.IsRunning, "Should be running after Start");

            // After Pause
            stopwatch.Pause();
            Assert.IsFalse(stopwatch.IsRunning, "Should not be running after Pause");

            // After Resume
            stopwatch.Resume();
            Assert.IsTrue(stopwatch.IsRunning, "Should be running after Resume");

            // After Stop
            stopwatch.Stop();
            Assert.IsFalse(stopwatch.IsRunning, "Should not be running after Stop");
        }
    }
}