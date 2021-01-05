
namespace SteamFriendSearchApp
{
    partial class AddRecentGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.addGameIdButton = new System.Windows.Forms.Button();
            this.appIdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.appIdNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // addGameIdButton
            // 
            this.addGameIdButton.Location = new System.Drawing.Point(12, 38);
            this.addGameIdButton.Name = "addGameIdButton";
            this.addGameIdButton.Size = new System.Drawing.Size(120, 23);
            this.addGameIdButton.TabIndex = 0;
            this.addGameIdButton.Text = "Add";
            this.addGameIdButton.UseVisualStyleBackColor = true;
            this.addGameIdButton.Click += new System.EventHandler(this.addGameIdButton_Click);
            // 
            // appIdNumericUpDown
            // 
            this.appIdNumericUpDown.Location = new System.Drawing.Point(12, 12);
            this.appIdNumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.appIdNumericUpDown.Name = "appIdNumericUpDown";
            this.appIdNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.appIdNumericUpDown.TabIndex = 1;
            // 
            // AddRecentGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(144, 69);
            this.Controls.Add(this.appIdNumericUpDown);
            this.Controls.Add(this.addGameIdButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "AddRecentGameForm";
            this.Opacity = 0.8D;
            this.Text = "Add Recent Game";
            ((System.ComponentModel.ISupportInitialize)(this.appIdNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addGameIdButton;
        private System.Windows.Forms.NumericUpDown appIdNumericUpDown;
    }
}