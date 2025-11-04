using System;
using System.Drawing;
using System.Windows.Forms;

namespace StopwatchApplication
{
    /// <summary>
    /// Main form for the Stopwatch Application
    /// Provides user interface for controlling stopwatch operations
    /// Includes Start, Pause, Resume, Reset, and Stop functionality
    /// </summary>
    public partial class StopwatchForm : Form
    {
        private StopwatchEngine stopwatchEngine;
        private System.Windows.Forms.Timer uiTimer;
        private Label lblTime;
        private Button btnStart;
        private Button btnPause;
        private Button btnResume;
        private Button btnReset;
        private Button btnStop;
        private Label lblStatus;

        /// <summary>
        /// Initializes a new instance of the StopwatchForm class
        /// Sets up the UI and initializes the stopwatch engine
        /// </summary>
        public StopwatchForm()
        {
            InitializeComponent();
            stopwatchEngine = new StopwatchEngine();
            InitializeUITimer();
            UpdateDisplay();
            UpdateButtonStates();
        }

        /// <summary>
        /// Initializes the UI timer that updates the display every 100ms
        /// Uses a loop concept through repeated timer ticks
        /// </summary>
        private void InitializeUITimer()
        {
            uiTimer = new System.Windows.Forms.Timer();
            uiTimer.Interval = 100; // Update every 100ms for smooth display
            uiTimer.Tick += UiTimer_Tick;
        }

        /// <summary>
        /// Handles the UI timer tick event to update the display
        /// Called repeatedly in a loop-like fashion by the timer
        /// </summary>
        private void UiTimer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Updates the time display label with current elapsed time
        /// Shows time in HH:MM:SS format
        /// </summary>
        private void UpdateDisplay()
        {
            lblTime.Text = stopwatchEngine.GetFormattedTime();
        }

        /// <summary>
        /// Updates the enabled/disabled state of all buttons based on stopwatch state
        /// Uses if/else statements to determine button availability
        /// </summary>
        private void UpdateButtonStates()
        {
            // Start button: only enabled when stopped and at zero
            if (!stopwatchEngine.IsRunning && stopwatchEngine.ElapsedSeconds == 0)
            {
                btnStart.Enabled = true;
            }
            else
            {
                btnStart.Enabled = false;
            }

            // Pause button: only enabled when running
            if (stopwatchEngine.IsRunning)
            {
                btnPause.Enabled = true;
            }
            else
            {
                btnPause.Enabled = false;
            }

            // Resume button: only enabled when paused with time
            if (!stopwatchEngine.IsRunning && stopwatchEngine.ElapsedSeconds > 0)
            {
                btnResume.Enabled = true;
            }
            else
            {
                btnResume.Enabled = false;
            }

            // Reset button: enabled when there's elapsed time
            if (stopwatchEngine.ElapsedSeconds > 0)
            {
                btnReset.Enabled = true;
            }
            else
            {
                btnReset.Enabled = false;
            }

            // Stop button: enabled when there's elapsed time
            if (stopwatchEngine.ElapsedSeconds > 0)
            {
                btnStop.Enabled = true;
            }
            else
            {
                btnStop.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Start button click event
        /// Starts the stopwatch from 00:00:00
        /// Time increments every second, updating minutes and hours accordingly
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            stopwatchEngine.Start();
            uiTimer.Start();
            UpdateDisplay();
            UpdateButtonStates();
            lblStatus.Text = "Status: Running";
        }

        /// <summary>
        /// Handles the Pause button click event
        /// Pauses the stopwatch and displays current time
        /// </summary>
        private void btnPause_Click(object sender, EventArgs e)
        {
            stopwatchEngine.Pause();
            uiTimer.Stop();
            UpdateDisplay();
            UpdateButtonStates();
            
            // Display the paused time in status
            string currentTime = stopwatchEngine.GetFormattedTime();
            lblStatus.Text = "Status: Paused at " + currentTime;
        }

        /// <summary>
        /// Handles the Resume button click event
        /// Continues the stopwatch from the last paused time
        /// </summary>
        private void btnResume_Click(object sender, EventArgs e)
        {
            stopwatchEngine.Resume();
            uiTimer.Start();
            UpdateDisplay();
            UpdateButtonStates();
            lblStatus.Text = "Status: Running";
        }

        /// <summary>
        /// Handles the Reset button click event
        /// Resets the stopwatch back to 00:00:00 from current value
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            stopwatchEngine.Reset();
            uiTimer.Stop();
            UpdateDisplay();
            UpdateButtonStates();
            lblStatus.Text = "Status: Reset to 00:00:00";
        }

        /// <summary>
        /// Handles the Stop button click event
        /// Stops the stopwatch completely and displays last recorded time
        /// </summary>
        private void btnStop_Click(object sender, EventArgs e)
        {
            string finalTime = stopwatchEngine.GetFormattedTime();
            stopwatchEngine.Stop();
            uiTimer.Stop();
            UpdateDisplay();
            UpdateButtonStates();
            lblStatus.Text = "Status: Stopped at " + finalTime;
        }

        /// <summary>
        /// Initializes the form components
        /// Creates all UI controls and sets their properties
        /// Designs a user-friendly interface with clear time display
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // 
            // lblTime - Displays elapsed time in HH:MM:SS format
            // 
            this.lblTime.AutoSize = false;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(50, 30);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(400, 100);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "00:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.ForeColor = System.Drawing.Color.DarkBlue;
            
            // 
            // btnStart - Starts stopwatch from 00:00:00
            // 
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(50, 160);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 50);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.BackColor = System.Drawing.Color.LightGreen;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            
            // 
            // btnPause - Pauses stopwatch at current time
            // 
            this.btnPause.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPause.Location = new System.Drawing.Point(190, 160);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(120, 50);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.BackColor = System.Drawing.Color.LightYellow;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            
            // 
            // btnResume - Continues from paused time
            // 
            this.btnResume.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnResume.Location = new System.Drawing.Point(330, 160);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(120, 50);
            this.btnResume.TabIndex = 3;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.BackColor = System.Drawing.Color.LightCyan;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            
            // 
            // btnReset - Resets stopwatch to 00:00:00
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(120, 230);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 50);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.BackColor = System.Drawing.Color.LightGray;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            
            // 
            // btnStop - Stops stopwatch completely
            // 
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnStop.Location = new System.Drawing.Point(260, 230);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(120, 50);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.BackColor = System.Drawing.Color.LightCoral;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            
            // 
            // lblStatus - Shows current stopwatch status
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(50, 300);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(400, 30);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray;
            
            // 
            // StopwatchForm - Main application form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 360);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTime);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StopwatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stopwatch Application - C# Assignment";
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ResumeLayout(false);
        }
    }
}