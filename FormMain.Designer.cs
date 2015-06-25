namespace Tools.CMSCreator
{
	partial class FormMain
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.radioButtonCMS = new System.Windows.Forms.RadioButton();
			this.radioButtonSign = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// radioButtonCMS
			// 
			this.radioButtonCMS.AutoSize = true;
			this.radioButtonCMS.Location = new System.Drawing.Point(12, 12);
			this.radioButtonCMS.Name = "radioButtonCMS";
			this.radioButtonCMS.Size = new System.Drawing.Size(82, 17);
			this.radioButtonCMS.TabIndex = 0;
			this.radioButtonCMS.TabStop = true;
			this.radioButtonCMS.Text = "Create CMS";
			this.radioButtonCMS.UseVisualStyleBackColor = true;
			// 
			// radioButtonSign
			// 
			this.radioButtonSign.AutoSize = true;
			this.radioButtonSign.Location = new System.Drawing.Point(12, 35);
			this.radioButtonSign.Name = "radioButtonSign";
			this.radioButtonSign.Size = new System.Drawing.Size(80, 17);
			this.radioButtonSign.TabIndex = 1;
			this.radioButtonSign.TabStop = true;
			this.radioButtonSign.Text = "Create Sign";
			this.radioButtonSign.UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(617, 370);
			this.Controls.Add(this.radioButtonSign);
			this.Controls.Add(this.radioButtonCMS);
			this.Name = "FormMain";
			this.Text = "CMS/Sign Creator";
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButtonCMS;
		private System.Windows.Forms.RadioButton radioButtonSign;


	}
}

