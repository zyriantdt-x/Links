using Links.Admin.Services;

namespace Links.Admin.Forms
{
    partial class LoginForm
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
            this.label1 = new Label();
            this.username = new TextBox();
            this.password = new TextBox();
            this.label2 = new Label();
            this.button1 = new Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point( 12, 15 );
            this.label1.Name = "label1";
            this.label1.Size = new Size( 75, 20 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // username
            // 
            this.username.Location = new Point( 93, 12 );
            this.username.Name = "username";
            this.username.Size = new Size( 291, 27 );
            this.username.TabIndex = 1;
            // 
            // password
            // 
            this.password.Location = new Point( 93, 49 );
            this.password.Name = "password";
            this.password.Size = new Size( 291, 27 );
            this.password.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point( 12, 52 );
            this.label2.Name = "label2";
            this.label2.Size = new Size( 70, 20 );
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new Point( 12, 89 );
            this.button1.Name = "button1";
            this.button1.Size = new Size( 372, 32 );
            this.button1.TabIndex = 4;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click +=  this.button1_Click ;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new SizeF( 8F, 20F );
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size( 396, 129 );
            this.ControlBox = false;
            this.Controls.Add( this.button1 );
            this.Controls.Add( this.label2 );
            this.Controls.Add( this.password );
            this.Controls.Add( this.username );
            this.Controls.Add( this.label1 );
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Links Login";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox username;
        private TextBox password;
        private Label label2;
        private Button button1;
    }
}