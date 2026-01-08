<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.msFile = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.rtbActions = New System.Windows.Forms.RichTextBox()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.lblFirmaNo = New System.Windows.Forms.Label()
        Me.lblFaysDB = New System.Windows.Forms.Label()
        Me.lblLogoDB = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.msFile.SuspendLayout()
        Me.SuspendLayout()
        '
        'msFile
        '
        Me.msFile.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.msFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.msFile.Location = New System.Drawing.Point(0, 0)
        Me.msFile.Name = "msFile"
        Me.msFile.Size = New System.Drawing.Size(775, 24)
        Me.msFile.TabIndex = 0
        Me.msFile.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.AboutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.FileToolStripMenuItem.Text = "Dosya"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.SettingsToolStripMenuItem.Text = "Ayarlar"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.AboutToolStripMenuItem.Text = "Hakkında"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.ExitToolStripMenuItem.Text = "Çıkış"
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnUpdate.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(546, 390)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(217, 78)
        Me.btnUpdate.TabIndex = 1
        Me.btnUpdate.Text = "Güncelle"
        Me.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'rtbActions
        '
        Me.rtbActions.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbActions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.rtbActions.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.rtbActions.Location = New System.Drawing.Point(12, 27)
        Me.rtbActions.Name = "rtbActions"
        Me.rtbActions.ReadOnly = True
        Me.rtbActions.Size = New System.Drawing.Size(751, 347)
        Me.rtbActions.TabIndex = 2
        Me.rtbActions.Text = ""
        '
        'lblServerName
        '
        Me.lblServerName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblServerName.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblServerName.Location = New System.Drawing.Point(9, 390)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(83, 17)
        Me.lblServerName.TabIndex = 3
        Me.lblServerName.Text = "Sunucu Adı:"
        '
        'lblFirmaNo
        '
        Me.lblFirmaNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFirmaNo.AutoSize = True
        Me.lblFirmaNo.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblFirmaNo.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFirmaNo.Location = New System.Drawing.Point(9, 441)
        Me.lblFirmaNo.Name = "lblFirmaNo"
        Me.lblFirmaNo.Size = New System.Drawing.Size(70, 17)
        Me.lblFirmaNo.TabIndex = 4
        Me.lblFirmaNo.Text = "Firma No:"
        '
        'lblFaysDB
        '
        Me.lblFaysDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFaysDB.AutoSize = True
        Me.lblFaysDB.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblFaysDB.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblFaysDB.Location = New System.Drawing.Point(229, 458)
        Me.lblFaysDB.Name = "lblFaysDB"
        Me.lblFaysDB.Size = New System.Drawing.Size(84, 17)
        Me.lblFaysDB.TabIndex = 5
        Me.lblFaysDB.Text = "Fays DB Adı:"
        '
        'lblLogoDB
        '
        Me.lblLogoDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLogoDB.AutoSize = True
        Me.lblLogoDB.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.lblLogoDB.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblLogoDB.Location = New System.Drawing.Point(9, 458)
        Me.lblLogoDB.Name = "lblLogoDB"
        Me.lblLogoDB.Size = New System.Drawing.Size(97, 17)
        Me.lblLogoDB.TabIndex = 6
        Me.lblLogoDB.Text = "LOGO DB Adı:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 300000
        '
        'FrmMain
        '
        Me.AcceptButton = Me.btnUpdate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(775, 480)
        Me.Controls.Add(Me.lblLogoDB)
        Me.Controls.Add(Me.lblFaysDB)
        Me.Controls.Add(Me.lblFirmaNo)
        Me.Controls.Add(Me.lblServerName)
        Me.Controls.Add(Me.rtbActions)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.msFile)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.msFile
        Me.MinimizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.msFile.ResumeLayout(False)
        Me.msFile.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents msFile As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents btnUpdate As Button
    Friend WithEvents rtbActions As RichTextBox
    Friend WithEvents lblServerName As Label
    Friend WithEvents lblFirmaNo As Label
    Friend WithEvents lblFaysDB As Label
    Friend WithEvents lblLogoDB As Label
    Friend WithEvents Timer1 As Timer
End Class
