namespace firstFormsApp_SQLServer
{
    partial class Dashboard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            foundPeopleListbox = new ListBox();
            lastNameTextBox = new TextBox();
            label1 = new Label();
            SearchBtn = new Button();
            SuspendLayout();
            // 
            // foundPeopleListbox
            // 
            foundPeopleListbox.FormattingEnabled = true;
            foundPeopleListbox.Location = new Point(12, 101);
            foundPeopleListbox.Name = "foundPeopleListbox";
            foundPeopleListbox.Size = new Size(331, 214);
            foundPeopleListbox.TabIndex = 0;
            // 
            // lastNameTextBox
            // 
            lastNameTextBox.Location = new Point(200, 12);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.Size = new Size(143, 23);
            lastNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(122, 13);
            label1.Name = "label1";
            label1.Size = new Size(72, 17);
            label1.TabIndex = 2;
            label1.Text = "Last Name";
            // 
            // SearchBtn
            // 
            SearchBtn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            SearchBtn.Location = new Point(200, 65);
            SearchBtn.Name = "SearchBtn";
            SearchBtn.Size = new Size(143, 30);
            SearchBtn.TabIndex = 3;
            SearchBtn.Text = "Search";
            SearchBtn.UseVisualStyleBackColor = true;
            SearchBtn.Click += SearchBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(783, 374);
            Controls.Add(SearchBtn);
            Controls.Add(label1);
            Controls.Add(lastNameTextBox);
            Controls.Add(foundPeopleListbox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox foundPeopleListbox;
        private TextBox lastNameTextBox;
        private Label label1;
        private Button SearchBtn;
    }
}
