using System;

namespace StopwatchApplication
{
    /// <summary>
    /// Core stopwatch engine that handles timing logic
    /// Uses loops and if/else statements as required
    /// </summary>
    public class StopwatchEngine
    {
        private DateTime startTime;
        private TimeSpan pausedTime;
        private bool isRunning;

        /// <summary>
        /// Gets the current running state of the stopwatch
        /// </summary>
        public bool IsRunning => isRunning;

        /// <summary>
        /// Gets the total elapsed seconds
        /// </summary>
        public double ElapsedSeconds
        {
            get
            {
                // Use if/else to determine elapsed time based on state
                if (isRunning)
                {
                    return (DateTime.Now - startTime).TotalSeconds + pausedTime.TotalSeconds;
                }
                else
                {
                    return pausedTime.TotalSeconds;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the StopwatchEngine class
        /// Sets initial state to stopped at 00:00:00
        /// </summary>
        public StopwatchEngine()
        {
            Reset();
        }

        /// <summary>
        /// Starts the stopwatch from 00:00:00
        /// Only starts if not already running
        /// </summary>
        public void Start()
        {
            // Use if statement to check if stopwatch is not running
            if (!isRunning)
            {
                startTime = DateTime.Now;
                pausedTime = TimeSpan.Zero;
                isRunning = true;
            }
        }

        /// <summary>
        /// Pauses the stopwatch at the current time
        /// Saves the elapsed time and stops incrementing
        /// </summary>
        public void Pause()
        {
            // Use if statement to check if stopwatch is running
            if (isRunning)
            {
                pausedTime += DateTime.Now - startTime;
                isRunning = false;
            }
        }

        /// <summary>
        /// Resumes the stopwatch from the last paused time
        /// Only resumes if stopped and has elapsed time
        /// </summary>
        public void Resume()
        {
            // Use if statement with multiple conditions
            if (!isRunning && pausedTime.TotalSeconds > 0)
            {
                startTime = DateTime.Now;
                isRunning = true;
            }
        }

        /// <summary>
        /// Resets the stopwatch to 00:00:00
        /// Stops timing and clears all elapsed time
        /// </summary>
        public void Reset()
        {
            startTime = DateTime.Now;
            pausedTime = TimeSpan.Zero;
            isRunning = false;
        }

        /// <summary>
        /// Stops the stopwatch completely
        /// Saves final time and stops incrementing
        /// </summary>
        public void Stop()
        {
            // Use if statement to save elapsed time if running
            if (isRunning)
            {
                pausedTime += DateTime.Now - startTime;
            }
            isRunning = false;
        }

        /// <summary>
        /// Gets the formatted time string in HH:MM:SS format
        /// Uses loops to calculate hours, minutes, and seconds
        /// </summary>
        /// <returns>Formatted time string in 00:00:00 format</returns>
        public string GetFormattedTime()
        {
            double totalSeconds = ElapsedSeconds;
            
            // Calculate hours using while loop
            int hours = 0;
            double remainingSeconds = totalSeconds;
            while (remainingSeconds >= 3600)
            {
                hours++;
                remainingSeconds -= 3600;
            }
            
            // Calculate minutes using while loop
            int minutes = 0;
            while (remainingSeconds >= 60)
            {
                minutes++;
                remainingSeconds -= 60;
            }
            
            // Get remaining seconds
            int seconds = (int)remainingSeconds;

            // Format and return the time string
            return FormatTimeComponent(hours) + ":" + 
                   FormatTimeComponent(minutes) + ":" + 
                   FormatTimeComponent(seconds);
        }

        /// <summary>
        /// Formats a time component to two digits
        /// Uses if/else to add leading zero if needed
        /// </summary>
        /// <param name="value">The time value to format</param>
        /// <returns>Formatted two-digit string</returns>
        private string FormatTimeComponent(int value)
        {
            // Use if/else to format with leading zero
            if (value < 10)
            {
                return "0" + value.ToString();
            }
            else
            {
                return value.ToString();
            }
        }
    }
}