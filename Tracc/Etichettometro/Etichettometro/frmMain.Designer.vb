<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
  Inherits System.Windows.Forms.Form

  'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Richiesto da Progettazione Windows Form
  Private components As System.ComponentModel.IContainer

  'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
  'Può essere modificata in Progettazione Windows Form.  
  'Non modificarla mediante l'editor del codice.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboR1 = New System.Windows.Forms.ComboBox()
        Me.cboR2 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboR3 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNEti = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBoxNEtixAtleta = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboColOrd = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDaRiga = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(22, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(166, 33)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Seleziona file excel "
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(22, 43)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(750, 287)
        Me.DataGridView1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 479)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Colonna QrCode"
        '
        'cboR1
        '
        Me.cboR1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboR1.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboR1.FormattingEnabled = True
        Me.cboR1.Location = New System.Drawing.Point(192, 477)
        Me.cboR1.Name = "cboR1"
        Me.cboR1.Size = New System.Drawing.Size(77, 22)
        Me.cboR1.TabIndex = 3
        '
        'cboR2
        '
        Me.cboR2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboR2.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboR2.FormattingEnabled = True
        Me.cboR2.Location = New System.Drawing.Point(407, 479)
        Me.cboR2.Name = "cboR2"
        Me.cboR2.Size = New System.Drawing.Size(77, 22)
        Me.cboR2.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(297, 481)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Colonna Riga1"
        '
        'cboR3
        '
        Me.cboR3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboR3.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboR3.FormattingEnabled = True
        Me.cboR3.Location = New System.Drawing.Point(635, 479)
        Me.cboR3.Name = "cboR3"
        Me.cboR3.Size = New System.Drawing.Size(77, 22)
        Me.cboR3.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(520, 481)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Colonna Riga2"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(23, 337)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(166, 34)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Seleziona logo"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(192, 336)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(580, 113)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 523)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(147, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "N° Etichette per pagina"
        '
        'txtNEti
        '
        Me.txtNEti.Location = New System.Drawing.Point(192, 521)
        Me.txtNEti.Name = "txtNEti"
        Me.txtNEti.Size = New System.Drawing.Size(100, 20)
        Me.txtNEti.TabIndex = 11
        Me.txtNEti.Text = "12"
        Me.txtNEti.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Cambria", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(16, 618)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(172, 31)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "GENERA ETICHETTE"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBoxNEtixAtleta
        '
        Me.TextBoxNEtixAtleta.Location = New System.Drawing.Point(475, 523)
        Me.TextBoxNEtixAtleta.Name = "TextBoxNEtixAtleta"
        Me.TextBoxNEtixAtleta.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxNEtixAtleta.TabIndex = 14
        Me.TextBoxNEtixAtleta.Text = "1"
        Me.TextBoxNEtixAtleta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(303, 525)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(146, 15)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "N° Etichette per record"
        '
        'cboColOrd
        '
        Me.cboColOrd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColOrd.Font = New System.Drawing.Font("Cambria", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboColOrd.FormattingEnabled = True
        Me.cboColOrd.Location = New System.Drawing.Point(192, 557)
        Me.cboColOrd.Name = "cboColOrd"
        Me.cboColOrd.Size = New System.Drawing.Size(225, 22)
        Me.cboColOrd.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(20, 559)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Ordina per"
        '
        'txtDaRiga
        '
        Me.txtDaRiga.Location = New System.Drawing.Point(406, 12)
        Me.txtDaRiga.Name = "txtDaRiga"
        Me.txtDaRiga.Size = New System.Drawing.Size(100, 20)
        Me.txtDaRiga.TabIndex = 18
        Me.txtDaRiga.Text = "2"
        Me.txtDaRiga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Cambria", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(234, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 15)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Inizia dalla riga"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 599)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(313, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Usa multi selezione sulla griglia per scegliere i record da stampare"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 661)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtDaRiga)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboColOrd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxNEtixAtleta)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txtNEti)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.cboR3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboR2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboR1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Etichettometro"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cboR1 As ComboBox
    Friend WithEvents cboR2 As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboR3 As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtNEti As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents TextBoxNEtixAtleta As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cboColOrd As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtDaRiga As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
End Class
