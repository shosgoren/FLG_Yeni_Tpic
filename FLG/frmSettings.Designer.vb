<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSettings))
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.txtTigerDbName = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.lblTigerDbName = New System.Windows.Forms.Label()
        Me.lblUserName = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblFaysDbName = New System.Windows.Forms.Label()
        Me.txtFaysDbName = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblFirmaNo = New System.Windows.Forms.Label()
        Me.txtFirmaNo = New System.Windows.Forms.TextBox()
        Me.chkShowPass = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTigerMasterDbName = New System.Windows.Forms.TextBox()
        Me.cbName4 = New System.Windows.Forms.CheckBox()
        Me.cbName5 = New System.Windows.Forms.CheckBox()
        Me.cbName6 = New System.Windows.Forms.CheckBox()
        Me.cbName7 = New System.Windows.Forms.CheckBox()
        Me.cbName8 = New System.Windows.Forms.CheckBox()
        Me.cbOtomatikGuncelleme = New System.Windows.Forms.CheckBox()
        Me.cbName3 = New System.Windows.Forms.CheckBox()
        Me.cbSpecode2 = New System.Windows.Forms.CheckBox()
        Me.cbSpecode3 = New System.Windows.Forms.CheckBox()
        Me.cbSpecode4 = New System.Windows.Forms.CheckBox()
        Me.cbSpecode5 = New System.Windows.Forms.CheckBox()
        Me.cbName = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(98, 12)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(314, 20)
        Me.txtServerName.TabIndex = 0
        '
        'txtTigerDbName
        '
        Me.txtTigerDbName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTigerDbName.Location = New System.Drawing.Point(136, 266)
        Me.txtTigerDbName.Name = "txtTigerDbName"
        Me.txtTigerDbName.Size = New System.Drawing.Size(161, 20)
        Me.txtTigerDbName.TabIndex = 4
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(98, 38)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(99, 20)
        Me.txtUserName.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(98, 64)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(99, 20)
        Me.txtPassword.TabIndex = 2
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Location = New System.Drawing.Point(12, 15)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(62, 13)
        Me.lblServerName.TabIndex = 4
        Me.lblServerName.Text = "Sunucu Adı"
        '
        'lblTigerDbName
        '
        Me.lblTigerDbName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTigerDbName.AutoSize = True
        Me.lblTigerDbName.Location = New System.Drawing.Point(9, 269)
        Me.lblTigerDbName.Name = "lblTigerDbName"
        Me.lblTigerDbName.Size = New System.Drawing.Size(105, 13)
        Me.lblTigerDbName.TabIndex = 5
        Me.lblTigerDbName.Text = "LOGO Veritabanı Adı"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(12, 41)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(64, 13)
        Me.lblUserName.TabIndex = 6
        Me.lblUserName.Text = "Kullanıcı Adı"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(12, 67)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(37, 13)
        Me.lblPassword.TabIndex = 7
        Me.lblPassword.Text = "Parola"
        '
        'lblFaysDbName
        '
        Me.lblFaysDbName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFaysDbName.AutoSize = True
        Me.lblFaysDbName.Location = New System.Drawing.Point(9, 324)
        Me.lblFaysDbName.Name = "lblFaysDbName"
        Me.lblFaysDbName.Size = New System.Drawing.Size(65, 13)
        Me.lblFaysDbName.TabIndex = 9
        Me.lblFaysDbName.Text = "Fays DB Adı"
        '
        'txtFaysDbName
        '
        Me.txtFaysDbName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFaysDbName.Location = New System.Drawing.Point(136, 321)
        Me.txtFaysDbName.Name = "txtFaysDbName"
        Me.txtFaysDbName.Size = New System.Drawing.Size(161, 20)
        Me.txtFaysDbName.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Century Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(343, 295)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 46)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "Kaydet"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblFirmaNo
        '
        Me.lblFirmaNo.AutoSize = True
        Me.lblFirmaNo.Location = New System.Drawing.Point(212, 41)
        Me.lblFirmaNo.Name = "lblFirmaNo"
        Me.lblFirmaNo.Size = New System.Drawing.Size(49, 13)
        Me.lblFirmaNo.TabIndex = 11
        Me.lblFirmaNo.Text = "Firma No"
        '
        'txtFirmaNo
        '
        Me.txtFirmaNo.Location = New System.Drawing.Point(267, 38)
        Me.txtFirmaNo.Name = "txtFirmaNo"
        Me.txtFirmaNo.Size = New System.Drawing.Size(145, 20)
        Me.txtFirmaNo.TabIndex = 3
        '
        'chkShowPass
        '
        Me.chkShowPass.AutoSize = True
        Me.chkShowPass.Enabled = False
        Me.chkShowPass.Location = New System.Drawing.Point(203, 66)
        Me.chkShowPass.Name = "chkShowPass"
        Me.chkShowPass.Size = New System.Drawing.Size(97, 17)
        Me.chkShowPass.TabIndex = 12
        Me.chkShowPass.Text = "Parolayı Göster"
        Me.chkShowPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkShowPass.UseVisualStyleBackColor = True
        Me.chkShowPass.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 295)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "LOGO Master DB Adı"
        '
        'txtTigerMasterDbName
        '
        Me.txtTigerMasterDbName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtTigerMasterDbName.Location = New System.Drawing.Point(136, 292)
        Me.txtTigerMasterDbName.Name = "txtTigerMasterDbName"
        Me.txtTigerMasterDbName.Size = New System.Drawing.Size(161, 20)
        Me.txtTigerMasterDbName.TabIndex = 13
        '
        'cbName4
        '
        Me.cbName4.AutoSize = True
        Me.cbName4.Location = New System.Drawing.Point(12, 146)
        Me.cbName4.Name = "cbName4"
        Me.cbName4.Size = New System.Drawing.Size(127, 17)
        Me.cbName4.TabIndex = 16
        Me.cbName4.Text = "Name 4 = UrunGrup1"
        Me.cbName4.UseVisualStyleBackColor = True
        '
        'cbName5
        '
        Me.cbName5.AutoSize = True
        Me.cbName5.Location = New System.Drawing.Point(203, 100)
        Me.cbName5.Name = "cbName5"
        Me.cbName5.Size = New System.Drawing.Size(236, 17)
        Me.cbName5.TabIndex = 17
        Me.cbName5.Text = "Üretici Kodu PRODRCODE  Grup2 Güncelle"
        Me.cbName5.UseVisualStyleBackColor = True
        Me.cbName5.Visible = False
        '
        'cbName6
        '
        Me.cbName6.AutoSize = True
        Me.cbName6.Location = New System.Drawing.Point(203, 123)
        Me.cbName6.Name = "cbName6"
        Me.cbName6.Size = New System.Drawing.Size(227, 17)
        Me.cbName6.TabIndex = 18
        Me.cbName6.Text = "Grup Kodu STGRPCODE Grup4 Güncelle "
        Me.cbName6.UseVisualStyleBackColor = True
        Me.cbName6.Visible = False
        '
        'cbName7
        '
        Me.cbName7.AutoSize = True
        Me.cbName7.Location = New System.Drawing.Point(203, 146)
        Me.cbName7.Name = "cbName7"
        Me.cbName7.Size = New System.Drawing.Size(215, 17)
        Me.cbName7.TabIndex = 19
        Me.cbName7.Text = "Özel Kodu SPECODE4 Grup5 Güncelle "
        Me.cbName7.UseVisualStyleBackColor = True
        Me.cbName7.Visible = False
        '
        'cbName8
        '
        Me.cbName8.AutoSize = True
        Me.cbName8.Location = New System.Drawing.Point(203, 166)
        Me.cbName8.Name = "cbName8"
        Me.cbName8.Size = New System.Drawing.Size(107, 17)
        Me.cbName8.TabIndex = 20
        Me.cbName8.Text = "FPM Güncelleme"
        Me.cbName8.UseVisualStyleBackColor = True
        Me.cbName8.Visible = False
        '
        'cbOtomatikGuncelleme
        '
        Me.cbOtomatikGuncelleme.AutoSize = True
        Me.cbOtomatikGuncelleme.Location = New System.Drawing.Point(306, 66)
        Me.cbOtomatikGuncelleme.Name = "cbOtomatikGuncelleme"
        Me.cbOtomatikGuncelleme.Size = New System.Drawing.Size(110, 17)
        Me.cbOtomatikGuncelleme.TabIndex = 21
        Me.cbOtomatikGuncelleme.Text = "Otomatik versiyon"
        Me.cbOtomatikGuncelleme.UseVisualStyleBackColor = True
        '
        'cbName3
        '
        Me.cbName3.AutoSize = True
        Me.cbName3.Location = New System.Drawing.Point(12, 123)
        Me.cbName3.Name = "cbName3"
        Me.cbName3.Size = New System.Drawing.Size(104, 17)
        Me.cbName3.TabIndex = 23
        Me.cbName3.Text = "Name 3 = Grup9"
        Me.cbName3.UseVisualStyleBackColor = True
        '
        'cbSpecode2
        '
        Me.cbSpecode2.AutoSize = True
        Me.cbSpecode2.Location = New System.Drawing.Point(12, 169)
        Me.cbSpecode2.Name = "cbSpecode2"
        Me.cbSpecode2.Size = New System.Drawing.Size(135, 17)
        Me.cbSpecode2.TabIndex = 24
        Me.cbSpecode2.Text = "Specode 2 = stk_g2_sl"
        Me.cbSpecode2.UseVisualStyleBackColor = True
        '
        'cbSpecode3
        '
        Me.cbSpecode3.AutoSize = True
        Me.cbSpecode3.Location = New System.Drawing.Point(12, 192)
        Me.cbSpecode3.Name = "cbSpecode3"
        Me.cbSpecode3.Size = New System.Drawing.Size(149, 17)
        Me.cbSpecode3.TabIndex = 24
        Me.cbSpecode3.Text = "Specode 3 = stk_g4_renk"
        Me.cbSpecode3.UseVisualStyleBackColor = True
        '
        'cbSpecode4
        '
        Me.cbSpecode4.AutoSize = True
        Me.cbSpecode4.Location = New System.Drawing.Point(12, 215)
        Me.cbSpecode4.Name = "cbSpecode4"
        Me.cbSpecode4.Size = New System.Drawing.Size(140, 17)
        Me.cbSpecode4.TabIndex = 24
        Me.cbSpecode4.Text = "Specode 4 = stk_g5_g5"
        Me.cbSpecode4.UseVisualStyleBackColor = True
        '
        'cbSpecode5
        '
        Me.cbSpecode5.AutoSize = True
        Me.cbSpecode5.Location = New System.Drawing.Point(12, 238)
        Me.cbSpecode5.Name = "cbSpecode5"
        Me.cbSpecode5.Size = New System.Drawing.Size(140, 17)
        Me.cbSpecode5.TabIndex = 24
        Me.cbSpecode5.Text = "Specode 5 = stk_g6_g6"
        Me.cbSpecode5.UseVisualStyleBackColor = True
        '
        'cbName
        '
        Me.cbName.AutoSize = True
        Me.cbName.Location = New System.Drawing.Point(12, 100)
        Me.cbName.Name = "cbName"
        Me.cbName.Size = New System.Drawing.Size(126, 17)
        Me.cbName.TabIndex = 16
        Me.cbName.Text = "Name = stk_g1_RNo"
        Me.cbName.UseVisualStyleBackColor = True
        '
        'FrmSettings
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(454, 353)
        Me.Controls.Add(Me.cbSpecode5)
        Me.Controls.Add(Me.cbSpecode4)
        Me.Controls.Add(Me.cbSpecode3)
        Me.Controls.Add(Me.cbSpecode2)
        Me.Controls.Add(Me.cbName3)
        Me.Controls.Add(Me.cbOtomatikGuncelleme)
        Me.Controls.Add(Me.cbName8)
        Me.Controls.Add(Me.cbName7)
        Me.Controls.Add(Me.cbName6)
        Me.Controls.Add(Me.cbName5)
        Me.Controls.Add(Me.cbName)
        Me.Controls.Add(Me.cbName4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTigerMasterDbName)
        Me.Controls.Add(Me.chkShowPass)
        Me.Controls.Add(Me.txtFirmaNo)
        Me.Controls.Add(Me.lblFirmaNo)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblFaysDbName)
        Me.Controls.Add(Me.txtFaysDbName)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUserName)
        Me.Controls.Add(Me.lblTigerDbName)
        Me.Controls.Add(Me.lblServerName)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.txtTigerDbName)
        Me.Controls.Add(Me.txtServerName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ayarlar"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtServerName As TextBox
    Friend WithEvents txtTigerDbName As TextBox
    Friend WithEvents txtUserName As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents lblServerName As Label
    Friend WithEvents lblTigerDbName As Label
    Friend WithEvents lblUserName As Label
    Friend WithEvents lblPassword As Label
    Friend WithEvents lblFaysDbName As Label
    Friend WithEvents txtFaysDbName As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents lblFirmaNo As Label
    Friend WithEvents txtFirmaNo As TextBox
    Friend WithEvents chkShowPass As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtTigerMasterDbName As TextBox
    Friend WithEvents cbName4 As CheckBox
    Friend WithEvents cbName5 As CheckBox
    Friend WithEvents cbName6 As CheckBox
    Friend WithEvents cbName7 As CheckBox
    Friend WithEvents cbName8 As CheckBox
    Friend WithEvents cbOtomatikGuncelleme As CheckBox
    Friend WithEvents cbName3 As CheckBox
    Friend WithEvents cbSpecode2 As CheckBox
    Friend WithEvents cbSpecode3 As CheckBox
    Friend WithEvents cbSpecode4 As CheckBox
    Friend WithEvents cbSpecode5 As CheckBox
    Friend WithEvents cbName As CheckBox
End Class
