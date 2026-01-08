Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Net

Public Class FrmMain
    'COLLATE SQL_Latin1_General_CP1254_CI_AS

    Dim rowCount As Integer = 0
    Dim processTime As Integer = 0
    Dim collationName As String = ""

    Private LOGODB As String
    Private LOGOMDB As String
    Private FAYSDB As String
    Private FirmNo As String
    Private ServerName As String

    Public Sub IslemBilgisi(ByVal rowCount As Integer, ByVal text As String)

        If rowCount <> 0 Then
            rtbActions.Text = String.Concat(rtbActions.Text, Environment.NewLine(), text, " ", "(" & rowCount, " row(s) affected)")

            'rtbActions.Find(String.Format("(" & rowCount, "row(s) affected)"))
            'rtbActions.SelectionColor = Color.Red
        Else
            Dim tablo As String
            tablo = text.Split(" ")(0) + " " + text.Split(" ")(1)
            tablo = tablo.Substring(0, tablo.Length - 2)
            rtbActions.Text = String.Concat(rtbActions.Text, Environment.NewLine(), tablo, " herhangi bir değişiklik olmadı.")
        End If
    End Sub
    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Dim frmLogin As New LoginForm
        frmLogin.ShowDialog()
        Dim frmsettings As New FrmSettings
        'Me.Hide()
        'frmsettings.ShowDialog()
        'Me.lblFaysDB.Text = FrmSettings.txtFaysDbName.Text
        'Me.lblFirmaNo.Text = FrmSettings.txtFirmaNo.Text
        'Me.lblLogoDB.Text = FrmSettings.txtTigerDbName.Text
        'Me.lblServerName.Text = FrmSettings.txtServerName.Text
        'Me.Show()
        lblFaysDB.Text = "Fays DB Adı: " + frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("FaysDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblLogoDB.Text = "LOGO DB Adı: " + frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblFirmaNo.Text = "Firma No: " + frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoFirmaNo"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblServerName.Text = "Sunucu Adı: " + frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")

        FAYSDB = frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("FaysDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        LOGODB = frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        FirmNo = frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoFirmaNo"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        ServerName = frmsettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        LOGOMDB = frmsettings.AES_Decrypt(LOGOMDB, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Dim result As MsgBoxResult = MsgBox("Program kapanacaktır emin misiniz?", MsgBoxStyle.YesNo, Title:="Uyarı")
        If result = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Developed by Anlaş Otomasyon", MsgBoxStyle.Information, Title:="Hakkında")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If MsgBox("LOGO -> Fays entegrasyonu başlayacak. Devam etmek istiyor musunuz?", MsgBoxStyle.YesNo, Title:="Bilgi") = MsgBoxResult.Yes Then
            Dim sw As Stopwatch = Stopwatch.StartNew()
#Region "SqlConnection and Query"
            Try
                Dim sqlHelper As New SqlHelper
                sqlHelper.GetSqlConnection()
                rtbActions.Text = String.Concat("Veritabanı bağlantısı kuruldu.")
                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Yeni Eklenen Malzeme Kartları***********", Environment.NewLine())
#Region "Get Collation Name"
                Try
                    collationName = "COLLATE "
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    'LogoDB
                    cmd.CommandText = String.Format("SELECT collation_name FROM sys.databases WHERE name = '" & LOGODB & "'")

                    collationName = collationName & CStr(cmd.ExecuteScalar())

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "RowCountControl ve INSERT"


#Region "INSERT QUERY"

                If ConfigurationManager.AppSettings.Get("NAME4").ToString().Equals("1") Then

                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format(
                            "INSERT INTO " & FAYSDB & ".dbo.stk_Kart (Grup6,StokKodu,UrunGrup1,stk_g1_RNo,stk_g2_sl,stk_g4_renk,stk_g5_g5,stk_g6_g6,Grup9,MiktarBirimi ,BarkodNo ,UrunGrup4 ,logoref,Aktif,Talep_Aktif)
SELECT KART.SHELFLIFE,KART.CODE AS KODU,KART.NAME4 AS ADI,KART.NAME,KART.SPECODE2,KART.SPECODE3,KART.SPECODE4,KART.SPECODE5,KART.NAME3,Birimadi.CODE AS BIRIMI, Case Barkod.BARCODE When null then '' else Barkod.BARCODE END BarkodNo,
                            CASE KART.CARDTYPE 
WHEN 1 THEN '(TM) Ticari Mal' 
WHEN 2 THEN '(KK) Karma Koli' 
WHEN 3 THEN '(DM) Depozitolu Mal' 
WHEN 4 THEN '(SK) Sabit Kıymet'
WHEN 10 THEN '(HM) Hammadde' 
WHEN 11 THEN '(YM) Yarı Mamul' 
WHEN 12 THEN '(MM) Mamul' 
WHEN 13 THEN '(TK) Tüketim Malı' 
WHEN 20 THEN '(MS) Genel Malzeme Sınıfı' 
WHEN 21 THEN '(MT) Tablolu Malzeme Sınıfı'
END UrunGrup4,KART.LOGICALREF,'1','1'

FROM LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) LEFT OUTER JOIN
LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  LEFT OUTER JOIN
LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF LEFT OUTER JOIN
LG_" & FirmNo & "_UNITBARCODE AS Barkod WITH (NOLOCK) ON Birimadi.LOGICALREF = Barkod.UNITLINEREF AND Barkod.ITEMREF = Kart.LOGICALREF AND Barkod.LINENR = 1
where KART.ACTIVE = 0 AND KART.CARDTYPE NOT IN(21,22) AND KART.LOGICALREF NOT IN(SELECT logoref 
FROM " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) WHERE logoref IS NOT NULL) 
AND KART.CODE " & collationName & " NOT IN(select StokKodu from " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) WHERE StokKodu " & collationName & " IS NOT NULL) 
AND (Barkod.BARCODE " & collationName & " NOT IN(select BarkodNo from " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) 
WHERE BarkodNo " & collationName & " IS NOT NULL) OR Barkod.BARCODE " & collationName & " IS NULL) ")


                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki eklemeler yapıldı.")

                    Catch ex As Exception
                        MsgBox(ex.Message)

                    End Try

                Else 'LOGO'daki ürün açıklaması 2 alanı stk_Kart tablosundaki UrunGrup3'e KAYDEDİLMESİN.
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()


                        cmd.CommandText = String.Format("INSERT INTO " & FAYSDB & ".dbo.stk_Kart (Grup6,StokKodu,UrunGrup1,MiktarBirimi ,BarkodNo ,UrunGrup4 ,logoref,Aktif,Talep_Aktif)
SELECT KART.SHELFLIFE,KART.CODE AS KODU,KART.NAME AS ADI,Birimadi.CODE AS BIRIMI, Case Barkod.BARCODE When null then '' else Barkod.BARCODE END BarkodNo,CASE KART.CARDTYPE 
WHEN 1 THEN '(TM) Ticari Mal' 
WHEN 2 THEN '(KK) Karma Koli' 
WHEN 3 THEN '(DM) Depozitolu Mal' 
WHEN 4 THEN '(SK) Sabit Kıymet' 
WHEN 10 THEN '(HM) Hammadde' 
WHEN 11 THEN '(YM) Yarı Mamul' 
WHEN 12 THEN '(MM) Mamul' 
WHEN 13 THEN '(TK) Tüketim Malı' 
WHEN 20 THEN '(MS) Genel Malzeme Sınıfı' 
END UrunGrup4,KART.LOGICALREF,'1','1'

FROM LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) LEFT OUTER JOIN
LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  LEFT OUTER JOIN
LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF LEFT OUTER JOIN
LG_" & FirmNo & "_UNITBARCODE AS Barkod WITH (NOLOCK) ON Birimadi.LOGICALREF = Barkod.UNITLINEREF AND Barkod.ITEMREF = Kart.LOGICALREF AND Barkod.LINENR = 1
where KART.ACTIVE = 0 AND KART.CARDTYPE NOT IN(21,22) AND KART.LOGICALREF NOT IN(SELECT logoref 
FROM " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) WHERE logoref IS NOT NULL) 
AND KART.CODE " & collationName & " NOT IN(select StokKodu from " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) WHERE StokKodu " & collationName & " IS NOT NULL) 
AND (Barkod.BARCODE " & collationName & " NOT IN(select BarkodNo from " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) 
WHERE BarkodNo " & collationName & " IS NOT NULL) OR Barkod.BARCODE " & collationName & " IS NULL) ")


                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki eklemeler yapıldı.")



                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                End If


#End Region
                'End If

#End Region

#Region "cari_KART_INSERT"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("INSERT INTO " & FAYSDB & ".dbo.cari_Kart (FirmaKodu, FirmaAdi, Adres, Grup1, logorefc)
  
  SELECT  CODE, DEFINITION_, ADDR1, 
  CASE CARDTYPE
  WHEN 1 THEN '(AL) ALICI'
  WHEN 2 THEN '(SA) SATICI'
  WHEN 3 THEN '(AS) ALICI + SATICI'
  WHEN 4 THEN '(GS) GRUP ŞİRKETİ'
  END Grup1,LOGICALREF
  
  FROM LG_" & FirmNo & "_CLCARD WITH (NOLOCK) WHERE CARDTYPE <> 22 AND LOGICALREF NOT IN(SELECT logorefc FROM " & FAYSDB & ".dbo.cari_Kart WITH (NOLOCK) WHERE logorefc IS NOT NULL) AND CODE " & collationName & " NOT IN(SELECT FirmaKodu FROM " & FAYSDB & ".dbo.cari_Kart WITH (NOLOCK) WHERE FirmaKodu " & collationName & " IS NOT NULL)")
                    IslemBilgisi(cmd.ExecuteNonQuery(), "cari_Kart tablosuna eklemeler yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Depo INSERT"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("INSERT INTO " & FAYSDB & ".dbo.stk_Depo (DepoKodu, DepoTuru,Adres)
  
  SELECT  NAME,
  CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		ELSE CONVERT(varchar(3),NR) END DepoTuru,
        EMAILADDR
  
  FROM " & LOGOMDB & ".dbo.L_CAPIWHOUSE WITH (NOLOCK) WHERE 
  CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		ELSE CONVERT(varchar(3),NR) END NOT IN(SELECT DepoTuru FROM " & FAYSDB & ".dbo.stk_Depo WITH (NOLOCK) WHERE DepoTuru IS NOT NULL) AND
  EMAILADDR='fays@fays.com.tr'
  
  AND 
  (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "'")
                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Depo tablosuna eklemeler yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Malzeme Kodları***********", Environment.NewLine())
                If ConfigurationManager.AppSettings.Get("FPM").ToString().Equals("1") Then
#Region "FPM FMU_ISHAREKET StokKodu UPDATE"

                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.urunkod=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK) ," & FAYSDB & ".dbo.FMU_ISHAREKET SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK) 
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.urunkod " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR SFL.urunkod<>ITM.CODE " & collationName & ")")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_ISHAREKET tablosundaki Stok Kodu güncellemeleri yapıldı.")

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
#Region "FPM FMU_RECETE StokKodu UPDATE"

                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.hmkod=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.FMU_RECETE SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.hmkod " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR SFL.hmkod<>ITM.CODE " & collationName & ")")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETE tablosundaki Stok Kodu güncellemeleri yapıldı.")

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
#Region "FPM FMU_RECETEALT StokKodu UPDATE"

                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.urunkod=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.FMU_RECETEALT SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.urunkod " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR SFL.urunkod<>ITM.CODE " & collationName & ")")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETEALT tablosundaki Stok Kodu güncellemeleri yapıldı.")

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
                End If
#Region "stk_Fislines StokKodu Demirbaş Kodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SFL SET SFL.stk_g8_g8=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.stk_g8_g8 " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR SFL.stk_g8_g8<>ITM.CODE " & collationName & ")")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Demirbaş Kodu güncellemeleri yapıldı.")

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_BakimFis EkipmanKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.EkipmanKodu=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_BakimFis STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.EkipmanKodu " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR STL.EkipmanKodu<>ITM.CODE " & collationName & ")")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_BakimFis tablosundaki StokKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "sa_SiparisLines StokKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.StokKodu=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.sa_SiparisLines STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.StokKodu " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR STL.StokKodu<>ITM.CODE " & collationName & ")")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "sa_SiparisLines tablosundaki StokKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_TransLines StokKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.StokKodu=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_TransLines STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.StokKodu " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR STL.StokKodu<>ITM.CODE " & collationName & ")")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki StokKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Fislines StokKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SFL SET SFL.StokKodu=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.StokKodu " & collationName & " AND (STK.StokKodu<>ITM.CODE " & collationName & " OR SFL.StokKodu<>ITM.CODE " & collationName & ")")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Stok Kodu güncellemeleri yapıldı.")

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Kart StokKodu UPDATE"

                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STK SET STK.StokKodu=ITM.CODE 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu<>ITM.CODE " & collationName & "")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki StokKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Kart UrunGrup4 (Stok Türü) UPDATE"
                '                ("UPDATE STK SET STK.UrunGrup4=ITM.CODE 
                'FROM " & FAYSDB & ".dbo.stk_Kart STK,LG_" & FirmNo & "_ITEMS ITM 
                'WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu<>ITM.CODE " & collationName & "")
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("Update B SET B.UrunGrup4=CASE A.CARDTYPE
WHEN 1 THEN  '(TM) Ticari Mal' 
WHEN 2 THEN  '(KK) Karma Koli' 
WHEN 3 THEN  '(DM) Depozitolu Mal' 
WHEN 4 THEN  '(SK) Sabit Kıymet' 
WHEN 10 THEN '(HM) Hammadde' 
WHEN 11 THEN '(YM) Yarı Mamul' 
WHEN 12 THEN '(MM) Mamul' 
WHEN 13 THEN '(TK) Tüketim Malı' 
WHEN 20 THEN '(MS) Genel Malzeme Sınıfı' 
End
                    From LG_" & FirmNo & "_ITEMS AS A WITH (NOLOCK) INNER Join (Select CASE UrunGrup4 
WHEN '(TM) Ticari Mal' THEN   1
WHEN '(KK) Karma Koli' THEN   2
WHEN '(DM) Depozitolu Mal' THEN 3
WHEN '(SK) Sabit Kıymet'   THEN  4
WHEN '(HM) Hammadde'  THEN 10
WHEN '(YM) Yarı Mamul'  THEN 11
WHEN '(MM) Mamul'  THEN 12
WHEN '(TK) Tüketim Malı' THEN 13
WHEN '(MS) Genel Malzeme Sınıfı'  THEN 20
End tur,StokKodu,logoref from " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK))  T On T.tur<>A.CARDTYPE And T.StokKodu=A.CODE  " & collationName & "
And T.logoref=A.LOGICALREF INNER JOIN " & FAYSDB & ".dbo.stk_Kart B WITH (NOLOCK) On B.StokKodu=t.StokKodu " & collationName & " And b.logoref=t.logoref
WHERE B.UrunGrup4<>CASE A.CARDTYPE
WHEN 1 THEN  '(TM) Ticari Mal' 
WHEN 2 THEN  '(KK) Karma Koli' 
WHEN 3 THEN  '(DM) Depozitolu Mal' 
WHEN 4 THEN  '(SK) Sabit Kıymet' 
WHEN 10 THEN '(HM) Hammadde' 
WHEN 11 THEN '(YM) Yarı Mamul' 
WHEN 12 THEN '(MM) Mamul' 
WHEN 13 THEN '(TK) Tüketim Malı' 
WHEN 20 THEN '(MS) Genel Malzeme Sınıfı' 
End")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki UrunGrup4 (Kart Türü) güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Fislines StokTuru UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SFL SET SFL.StokTuru=CASE STK.UrunGrup4
WHEN '(SK) Sabit Kıymet' THEN  'D'
ELSE 'S'
END
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK)
WHERE STK.StokKodu=SFL.StokKodu " & collationName & " AND STK.idNo=SFL.StokRefNo and SFL.StokTuru<>CASE STK.UrunGrup4
WHEN '(SK) Sabit Kıymet' THEN  'D'
ELSE 'S'
END ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Stok Türü güncellemeleri yapıldı.")

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region


                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Malzeme İsimleri***********", Environment.NewLine())
                If ConfigurationManager.AppSettings.Get("FPM").ToString().Equals("1") Then
#Region "FPM FMU_ISHAREKET STOK ADI UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.urunad=CONVERT(VARCHAR(50),ITM.NAME) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.FMU_ISHAREKET SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM  WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.urunkod " & collationName & " AND  SFL.urunad <> ITM.NAME " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_ISHAREKET tablosundaki Stok Adı güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
#Region "FPM FMU_RECETE STOK ADI UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.hmad=CONVERT(VARCHAR(50),ITM.NAME) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.FMU_RECETE SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.hmkod " & collationName & " AND  SFL.hmad <> ITM.NAME " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETE tablosundaki Stok Adı güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
#Region "FPM FMU_RECETEALT STOK ADI UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.urunad=CONVERT(VARCHAR(50),ITM.NAME) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.FMU_RECETEALT SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.urunkod " & collationName & " AND  SFL.urunad <> ITM.NAME " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETEALT tablosundaki Stok Adı güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
                End If

                If ConfigurationManager.AppSettings.Get("NAME4").ToString().Equals("1") Then 'LOGO'daki ürün açıklaması 3 alanı stk_Kart tablosundaki UrunGrup1'e güncellensin.
#Region "stk_BakimFis EkipmanAdi UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.EkipmanAdi=CONVERT(VARCHAR(200),ITM.NAME4) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_BakimFis STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.EkipmanKodu " & collationName & " AND  STL.EkipmanAdi <> ITM.NAME4 " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_BakimFis tablosundaki EkipmanAdi güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


#End Region
#Region "sa_SiparisLines UrunGrup1 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup1=CONVERT(VARCHAR(200),ITM.NAME4) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.sa_SiparisLines STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM  WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.StokKodu " & collationName & " AND  STL.UrunGrup1 <> ITM.NAME4 " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "sa_SiparisLines tablosundaki UrunGrup1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


#End Region
#Region "stk_TransLines UrunGrup1 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup1=CONVERT(VARCHAR(200),ITM.NAME4) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_TransLines STL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=STL.StokKodu " & collationName & " AND  STL.UrunGrup1 <> ITM.NAME4 " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki UrunGrup1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


#End Region
#Region "stk_FisLines UrunGrup1 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.UrunGrup1=CONVERT(VARCHAR(200),ITM.NAME4) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.StokKodu " & collationName & " AND  SFL.UrunGrup1 <> ITM.NAME4 " & collationName & "")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki UrunGrup1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

#End Region
#Region "stk_Kart UrunGrup1 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STK SET STK.UrunGrup1=CONVERT(VARCHAR(200),ITM.NAME4),STK.Grup6=ITM.SHELFLIFE
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.UrunGrup1<>ITM.NAME4 AND STK.StokKodu = ITM.CODE " & collationName & " AND ITM.ACTIVE = 0 ")
                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki UrunGrup1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                End If
#End Region

                If ConfigurationManager.AppSettings.Get("NAME").ToString().Equals("1") Then
#Region "stk_FisLines Grup3 (stk_kart_stk_g1_RNo) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.Grup3=STK.stk_g1_RNo 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.StokKodu " & collationName & " AND ISNULL(SFL.Grup3,'') <> ISNULL(STK.stk_g1_RNo,'') " & collationName & " ")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Parça No 1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart stk_g1_RNo (NAME) (PARÇA NO 1) NAME UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STK SET STK.stk_g1_RNo=CONVERT(VARCHAR(50),ITM.NAME)
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu = ITM.CODE " & collationName & " AND (ISNULL(STK.stk_g1_RNo,'')<>CONVERT(VARCHAR(50),ISNULL(ITM.NAME,'') " & collationName & " )AND ITM.ACTIVE = 0 )
")
                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki PARÇA NO 1 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                If ConfigurationManager.AppSettings.Get("NAME3").ToString().Equals("1") Then
#Region "stk_FisLines stk_g9_g9 NAME3 (Kullanım Alanı) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE SFL SET SFL.stk_g9_g9=CONVERT(VARCHAR(201),ITM.NAME3) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines SFL WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu=SFL.StokKodu " & collationName & " AND (SFL.stk_g9_g9 <> ITM.NAME3 " & collationName & ")")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Kullanım Alanı güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart Grup9 NAME3 (Kullanım Alanı) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STK SET STK.Grup9=CONVERT(VARCHAR(201),ITM.NAME3) 
FROM " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK)
WHERE STK.logoref=ITM.LOGICALREF AND STK.StokKodu = ITM.CODE " & collationName & " AND (ISNULL(STK.Grup9,'')<>CONVERT(VARCHAR(201),ISNULL(ITM.NAME3,'') " & collationName & ") )")
                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Kullanım Alanı güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Standart Barkodlar***********", Environment.NewLine())
#Region "stk_Kart tablosundaki BarkodNo UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STK SET STK.BarkodNo=BAR.BARCODE 
FROM LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK),LG_" & FirmNo & "_ITMUNITA IMU WITH (NOLOCK), LG_" & FirmNo & "_UNITSETL UNL WITH (NOLOCK),LG_" & FirmNo & "_UNITBARCODE BAR WITH (NOLOCK), " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK)
WHERE ITM.LOGICALREF=IMU.ITEMREF AND 
IMU.UNITLINEREF=UNL.LOGICALREF AND 
UNL.MAINUNIT=1 AND 
ITM.LOGICALREF=BAR.ITEMREF AND 
UNL.LOGICALREF=BAR.UNITLINEREF AND 
IMU.LOGICALREF=BAR.ITMUNITAREF AND 
ITM.CARDTYPE<>22 AND 
ITM.LOGICALREF=STK.logoref AND 
BAR.LINENR = 1 AND
BAR.TYP <> 1 AND
BAR.BARCODE " & collationName & " NOT IN(SELECT BarkodNo FROM " & FAYSDB & ".dbo.stk_Kart WITH (NOLOCK) WHERE BarkodNo " & collationName & " IS NOT NULL) AND
(ISNULL(BAR.BARCODE,'')<>ISNULL(STK.BarkodNo,'') " & collationName & " )
")
                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki BarkodNo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "" & FAYSDB & "..UPDATE_BARCODE_NUMBER"
                    cmd.ExecuteNonQuery()

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#Region "sa_SiparisLines BarkodNo UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.BarkodNo=BAR.BARCODE 
FROM LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK),LG_" & FirmNo & "_ITMUNITA IMU WITH (NOLOCK), LG_" & FirmNo & "_UNITSETL UNL WITH (NOLOCK),LG_" & FirmNo & "_UNITBARCODE BAR WITH (NOLOCK)," & FAYSDB & ".dbo.sa_SiparisLines STL WITH (NOLOCK)
WHERE ITM.LOGICALREF=IMU.ITEMREF AND 
IMU.UNITLINEREF=UNL.LOGICALREF AND 
UNL.MAINUNIT=1 AND 
ITM.LOGICALREF=BAR.ITEMREF AND 
UNL.LOGICALREF=BAR.UNITLINEREF AND 
IMU.LOGICALREF=BAR.ITMUNITAREF AND 
ITM.CARDTYPE<>22 AND 
ITM.CODE=STL.StokKodu " & collationName & " AND 
BAR.LINENR = 1 AND
BAR.TYP <> 1 AND
BAR.BARCODE " & collationName & " NOT IN(SELECT BarkodNo FROM " & FAYSDB & ".dbo.sa_SiparisLines WITH (NOLOCK) WHERE BarkodNo " & collationName & " IS NOT NULL) AND
BAR.BARCODE<>STL.BarkodNo " & collationName & "")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "sa_SiparisLines tablosundaki BarkodNo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_FisLines BarkodNo UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.BarkodNo=BAR.BARCODE 
FROM LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK),LG_" & FirmNo & "_ITMUNITA IMU WITH (NOLOCK), LG_" & FirmNo & "_UNITSETL UNL WITH (NOLOCK),LG_" & FirmNo & "_UNITBARCODE BAR WITH (NOLOCK)," & FAYSDB & ".dbo.stk_FisLines STL WITH (NOLOCK)
WHERE ITM.LOGICALREF=IMU.ITEMREF AND 
IMU.UNITLINEREF=UNL.LOGICALREF AND 
UNL.MAINUNIT=1 AND 
ITM.LOGICALREF=BAR.ITEMREF AND 
UNL.LOGICALREF=BAR.UNITLINEREF AND 
IMU.LOGICALREF=BAR.ITMUNITAREF AND 
ITM.CARDTYPE<>22 AND 
ITM.CODE=STL.StokKodu " & collationName & " AND 
BAR.LINENR = 1 AND
BAR.TYP <> 1 AND
BAR.BARCODE " & collationName & " NOT IN(SELECT BarkodNo FROM " & FAYSDB & ".dbo.stk_FisLines WITH (NOLOCK) WHERE BarkodNo " & collationName & " IS NOT NULL) AND
BAR.BARCODE<>STL.BarkodNo " & collationName & "")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki BarkodNo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_TransLines BarkodNo UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.BarkodNo=BAR.BARCODE 
FROM LG_" & FirmNo & "_ITEMS ITM WITH (NOLOCK),LG_" & FirmNo & "_ITMUNITA IMU WITH (NOLOCK), LG_" & FirmNo & "_UNITSETL UNL WITH (NOLOCK),LG_" & FirmNo & "_UNITBARCODE BAR WITH (NOLOCK)," & FAYSDB & ".dbo.stk_TransLines STL WITH (NOLOCK)
WHERE ITM.LOGICALREF=IMU.ITEMREF AND 
IMU.UNITLINEREF=UNL.LOGICALREF AND 
UNL.MAINUNIT=1 AND 
ITM.LOGICALREF=BAR.ITEMREF AND 
UNL.LOGICALREF=BAR.UNITLINEREF AND 
IMU.LOGICALREF=BAR.ITMUNITAREF AND 
ITM.CARDTYPE<>22 AND 
ITM.CODE=STL.StokKodu " & collationName & " AND 
BAR.LINENR = 1 AND
BAR.TYP <> 1 AND
BAR.BARCODE " & collationName & " NOT IN(SELECT BarkodNo FROM " & FAYSDB & ".dbo.stk_TransLines WITH (NOLOCK) WHERE BarkodNo " & collationName & " IS NOT NULL) AND
BAR.BARCODE<>STL.BarkodNo " & collationName & "
")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki BarkodNo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "sa_SiparisLines LOGO'da Barkod  Yok ise stk_Kart'tan al"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("Update SFL SET SFL.BarkodNo = STK.BarkodNo

From " & FAYSDB & ".dbo.stk_Kart STK,(Select * from " & FAYSDB & ".dbo.sa_SiparisLines WITH (NOLOCK) where StokKodu " & collationName & " in (

SELECT 

KART.CODE AS KODU

FROM 
LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) LEFT OUTER JOIN
LG_" & FirmNo & "_UNITBARCODE AS Barkod WITH (NOLOCK) ON  Barkod.ITEMREF = Kart.LOGICALREF LEFT OUTER JOIN
" & FAYSDB & ".dbo.stk_Kart AS SK WITH (NOLOCK) ON SK.logoref=kart.LOGICALREF
WHERE Barkod.BARCODE " & collationName & " is null and sk.BarkodNo " & collationName & " is not null and KART.ACTIVE = 0
)) SFL

where STK.StokKodu = SFL.StokKodu " & collationName & "  AND (ISNULL(STK.BarkodNo,'')<>ISNULL(SFL.BarkodNo,'') " & collationName & " )
")
                    cmd.CommandTimeout = 600
                    IslemBilgisi(cmd.ExecuteNonQuery(), "sa_SiparisLines tablosundaki BarkodNo güncellemeleri LOGO bağımsız  yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_FisLines LOGO'da Barkod  Yok ise stk_Kart'tan al"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("Update SFL SET SFL.BarkodNo = STK.BarkodNo

From " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),(Select * from " & FAYSDB & ".dbo.stk_FisLines WITH (NOLOCK) where StokKodu " & collationName & " in (

SELECT 

KART.CODE AS KODU

FROM 
LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) LEFT OUTER JOIN
LG_" & FirmNo & "_UNITBARCODE AS Barkod WITH (NOLOCK) ON  Barkod.ITEMREF = Kart.LOGICALREF LEFT OUTER JOIN
" & FAYSDB & ".dbo.stk_Kart AS SK WITH (NOLOCK) ON SK.logoref=kart.LOGICALREF
WHERE Barkod.BARCODE " & collationName & " is null and sk.BarkodNo " & collationName & " is not null and KART.ACTIVE = 0
)) SFL

where STK.StokKodu = SFL.StokKodu " & collationName & " AND (ISNULL(STK.BarkodNo,'')<>ISNULL(SFL.BarkodNo,'') " & collationName & "  )
")
                    cmd.CommandTimeout = 600
                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki BarkodNo güncellemeleri LOGO bağımsız  yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_TransLines LOGO'da Barkod Yok ise stk_Kart'tan al"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("Update STL SET STL.BarkodNo = STK.BarkodNo

From " & FAYSDB & ".dbo.stk_Kart STK WITH (NOLOCK),(Select * from " & FAYSDB & ".dbo.stk_TransLines WITH (NOLOCK) where StokKodu " & collationName & " in (

SELECT 

KART.CODE AS KODU

FROM 
LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) LEFT OUTER JOIN
LG_" & FirmNo & "_UNITBARCODE AS Barkod WITH (NOLOCK) ON  Barkod.ITEMREF = Kart.LOGICALREF LEFT OUTER JOIN
" & FAYSDB & ".dbo.stk_Kart AS SK WITH (NOLOCK) ON SK.logoref=kart.LOGICALREF
WHERE Barkod.BARCODE " & collationName & " is null and sk.BarkodNo is not null and KART.ACTIVE = 0
)) STL

where STK.StokKodu = STL.StokKodu " & collationName & "  AND (ISNULL(STK.BarkodNo,'')<>ISNULL(STL.BarkodNo,'') " & collationName & "  )
")
                    cmd.CommandTimeout = 600
                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki BarkodNo güncellemeleri LOGO bağımsız  yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Ana Birim İsimleri***********", Environment.NewLine())
                If ConfigurationManager.AppSettings.Get("FPM").ToString().Equals("1") Then
#Region "FPM FMU_RECETE MiktarBirimi UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.hmbirim=LOGO.BIRIMI

From " & FAYSDB & ".dbo.FMU_RECETE as STL WITH (NOLOCK),
(SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK)  JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF
Where CARDTYPE <> 22 ) as LOGO

Where STL.hmkod = LOGO.KODU " & collationName & " and (ISNULL(STL.hmbirim,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETE tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "FPM FMU_ISHAREKET MiktarBirimi UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.birim=LOGO.BIRIMI

From " & FAYSDB & ".dbo.FMU_ISHAREKET as STL WITH (NOLOCK),
(SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK)  JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF
Where CARDTYPE <> 22 ) as LOGO

Where STL.urunkod = LOGO.KODU " & collationName & " and (ISNULL(STL.birim,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_ISHAREKET tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "FPM FMU_RECETEALT MiktarBirimi UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.birim=LOGO.BIRIMI

From " & FAYSDB & ".dbo.FMU_RECETEALT as STL WITH (NOLOCK),
(SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart  WITH (NOLOCK) JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF
Where CARDTYPE <> 22 ) as LOGO

Where STL.urunkod = LOGO.KODU " & collationName & " and (ISNULL(STL.birim,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "FMU_RECETEALT tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
                End If
#Region "sa_SiparisLines MiktarBirimi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.MiktarBirimi=LOGO.BIRIMI

From " & FAYSDB & ".dbo.sa_SiparisLines as STL WITH (NOLOCK), (SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart  WITH (NOLOCK) JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF
Where CARDTYPE <> 22 ) as LOGO

Where STL.StokKodu = LOGO.KODU " & collationName & " and (ISNULL(STL.MiktarBirimi,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & ") ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "sa_SiparisLines tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_FisLines MiktarBirimi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STL SET STL.MiktarBirimi=LOGO.BIRIMI

From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK), (SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK)  JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF
Where CARDTYPE <> 22 ) as LOGO

Where STL.StokKodu = LOGO.KODU " & collationName & " and (ISNULL(STL.MiktarBirimi,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_TransLines MiktarBirimi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SFL SET SFL.MiktarBirimi=LOGO.BIRIMI

From " & FAYSDB & ".dbo.stk_TransLines as SFL WITH (NOLOCK), (SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK) JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0 JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF  
Where CARDTYPE <> 22 ) as LOGO

Where SFL.StokKodu = LOGO.KODU " & collationName & " and (ISNULL(SFL.MiktarBirimi,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Kart MiktarBirimi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE STK SET STK.MiktarBirimi=LOGO.BIRIMI

From " & FAYSDB & ".dbo.stk_Kart as STK WITH (NOLOCK), (SELECT KART.ACTIVE AS KULLANIMBILGISI,KART.CARDTYPE AS KARTTURU,KART.CODE AS KODU,KART.NAME AS ADI,
Birimadi.CODE AS BIRIMI 
FROM DBO.LG_" & FirmNo & "_ITEMS AS Kart WITH (NOLOCK)  JOIN
DBO.LG_" & FirmNo & "_ITMUNITA AS Birimid WITH (NOLOCK) ON kart.LOGICALREF = Birimid.ITEMREF AND Birimid.LINENR=1 AND Birimid.VARIANTREF = 0  JOIN
DBO.LG_" & FirmNo & "_UNITSETL AS Birimadi WITH (NOLOCK) ON Birimid.UNITLINEREF = Birimadi.LOGICALREF  
Where CARDTYPE <> 22 ) as LOGO

Where STK.StokKodu = LOGO.KODU " & collationName & " and (ISNULL(STK.MiktarBirimi,'')<>ISNULL(LOGO.BIRIMI,'') " & collationName & " )")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki MiktarBirimi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
                If ConfigurationManager.AppSettings.Get("PRODUCERCODE").ToString().Equals("1") Then
                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********UPDATE PRODUCERCODE***********", Environment.NewLine())
#Region "stk_FisLines Üretici Kodu PRODUCERCODE stk_g2_sl UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g2_sl=LOGO.PRODUCERCODE

From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName & " 

Where  (ISNULL(STL.stk_g2_sl,'') <> ISNULL(LOGO.PRODUCERCODE,'') " & collationName & " )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki stk_g2_sl güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_TransLines Üretici Kodu PRODUCERCODE stk_g2_sl UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g2_sl=LOGO.PRODUCERCODE

From " & FAYSDB & ".dbo.stk_TransLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName & " 

Where  (ISNULL(STL.stk_g2_sl,'') <> ISNULL(LOGO.PRODUCERCODE,'') " & collationName & " )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki stk_g2_sl güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart Üretici Kodu PRODUCERCODE Grup2 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                        cmd.CommandText = String.Format("UPDATE STL SET STL.Grup2=LOGO.PRODUCERCODE

From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName & " 

Where  (ISNULL(STL.Grup2,'') <> ISNULL(LOGO.PRODUCERCODE,'') " & collationName & "  )")
                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Grup2 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region
                If ConfigurationManager.AppSettings.Get("STGRPCODE").ToString().Equals("1") Then
                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********UPDATE STGRPCODE***********", Environment.NewLine())
#Region "stk_FisLines Grup Kodu STGRPCODE stk_g4_renk UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g1_RNo=LOGO.STGRPCODE
                        From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName)

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki stk_g4_renk güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_TransLines Grup Kodu STGRPCODE stk_g4_renk UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g4_renk=LOGO.STGRPCODE
                    From " & FAYSDB & ".dbo.stk_TransLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName)

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki stk_g4_renk güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart Grup Kodu STGRPCODE Grup4 UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format("UPDATE STL SET STL.Grup4=LOGO.STGRPCODE
                From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName)


                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Grup4 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                If ConfigurationManager.AppSettings.Get("SPECODE2").ToString().Equals("1") Then
                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Parça No 2***********", Environment.NewLine())
#Region "stk_FisLines SPECODE2 UrunGrup4 (Parça No 2) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup4=LOGO.SPECODE2
                        From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName & "  AND (ISNULL(STL.UrunGrup4,'')<>ISNULL(LOGO.SPECODE2,'') " & collationName & "  )")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Parça No 2 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart SPECODE2 stk_g2_sl (Parça No 2) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()                       '                       

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g2_sl=LOGO.SPECODE2
                From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE " & collationName & "  AND (ISNULL(STL.stk_g2_sl,'')<>ISNULL(LOGO.SPECODE2,'') " & collationName & "  )")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Parça No 2 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If

#End Region
                If ConfigurationManager.AppSettings.Get("SPECODE3").ToString().Equals("1") Then

                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Parça No 3***********", Environment.NewLine())
#Region "stk_FisLines SPECODE3 UrunGrup2 (Parça No 3) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup2=LOGO.SPECODE3
                        From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND (ISNULL(STL.UrunGrup2,'')<>ISNULL(LOGO.SPECODE3,'') " & collationName & " )")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Parça No 3 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart SPECODE3 stk_g4_renk (Parça No 3) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()                       '                       

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g4_renk=LOGO.SPECODE3
                From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND (ISNULL(STL.stk_g4_renk,'')<>ISNULL(LOGO.SPECODE3,'') " & collationName & " )")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Parça No 3 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                If ConfigurationManager.AppSettings.Get("SPECODE4").ToString().Equals("1") Then
                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Parça No 4***********", Environment.NewLine())
#Region "stk_FisLines SPECODE4 UrunGrup3 (Parça No 4) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()

                        'cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup3=LOGO.SPECODE4
                        'From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND (ISNULL(STL.UrunGrup3,'')<>ISNULL(LOGO.SPECODE4,'') " & collationName & " )")

                        cmd.CommandText = String.Format("UPDATE STL SET STL.UrunGrup3=LOGO.SPECODE4
                        From " & FAYSDB & ".dbo.stk_FisLines as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND UrunGrup3=''")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Parça No 4 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
#End Region
#Region "stk_Kart SPECODE4 stk_g5_g5 (Parça No 4) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()                       '                       

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g5_g5=LOGO.SPECODE4
                From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND (ISNULL(STL.stk_g5_g5,'')<>ISNULL(LOGO.SPECODE4,'') " & collationName & " )")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Parça No 4 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                If ConfigurationManager.AppSettings.Get("SPECODE5").ToString().Equals("1") Then
                    rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Parça No 5***********", Environment.NewLine())
#Region "stk_Kart SPECODE5 stk_g6_g6 (Parça No 5) UPDATE"
                    Try
                        Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()                       '                       

                        cmd.CommandText = String.Format("UPDATE STL SET STL.stk_g6_g6=LOGO.SPECODE5
                From " & FAYSDB & ".dbo.stk_Kart as STL WITH (NOLOCK) JOIN  DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON STL.StokKodu = LOGO.CODE  " & collationName & " AND (ISNULL(STL.stk_g6_g6,'')<>ISNULL(LOGO.SPECODE5,'') " & collationName & ")")

                        cmd.CommandTimeout = 600

                        IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki Parça No 5 güncellemeleri yapıldı.")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                End If
#End Region

                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Cari Kartlar***********", Environment.NewLine())
#Region "stk_Fis FirmaAdi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SF SET SF.FirmaAdi = LTK.DEFINITION_
FROM " & FAYSDB & ".dbo.cari_Kart CTK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_Fis SF WITH (NOLOCK), LG_" & FirmNo & "_CLCARD LTK WITH (NOLOCK)
WHERE CTK.logorefc = LTK.LOGICALREF AND CTK.FirmaKodu=SF.FirmaKodu " & collationName & " AND SF.FirmaAdi<>LTK.DEFINITION_ " & collationName & "")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Fis tablosundaki FirmaAdi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Fis FirmaKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SF SET SF.FirmaKodu = LTK.CODE
FROM " & FAYSDB & ".dbo.cari_Kart CTK WITH (NOLOCK)," & FAYSDB & ".dbo.stk_Fis SF WITH (NOLOCK), LG_" & FirmNo & "_CLCARD LTK WITH (NOLOCK)
WHERE CTK.logorefc = LTK.LOGICALREF AND CTK.FirmaKodu=SF.FirmaKodu " & collationName & " AND SF.FirmaKodu<>LTK.CODE " & collationName & "")
                    cmd.CommandTimeout = 600
                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Fis tablosundaki FirmaKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "cari_Kart FirmaKodu UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE CTK SET CTK.FirmaKodu=LTK.CODE 
FROM " & FAYSDB & ".dbo.cari_Kart CTK WITH (NOLOCK),LG_" & FirmNo & "_CLCARD LTK  WITH (NOLOCK)
WHERE CTK.logorefc=LTK.LOGICALREF AND CTK.FirmaKodu<>LTK.CODE " & collationName & "")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "cari_Kart tablosundaki FirmaKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "cari_Kart FirmaAdi UPDATE"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE CTK SET CTK.FirmaAdi = LTK.DEFINITION_
FROM " & FAYSDB & ".dbo.cari_Kart CTK WITH (NOLOCK), LG_" & FirmNo & "_CLCARD LTK WITH (NOLOCK)
WHERE CTK.logorefc = LTK.LOGICALREF AND CTK.FirmaAdi<>LTK.DEFINITION_ " & collationName & "")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "cari_Kart tablosundaki FirmaAdi güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
                rtbActions.Text += String.Concat(Environment.NewLine(), "*********Güncellenen Depo İsimleri***********", Environment.NewLine())
#Region "Depolar ile ilgili UPDATE işlemleri"

#Region "UrunGrup5 Depo"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE UG5 SET UG5.Depo = LWH.NAME
  FROM " & FAYSDB & ".dbo.stk_URUNGRUP5 UG5, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE LWH.EMAILADDR='fays@fays.com.tr' AND FWH.DepoKodu = UG5.Depo " & collationName & " AND LWH.NAME <> UG5.Depo " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "UrunGrup5 tablosundaki Depo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "FisLines Depo"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE SFL SET  SFL.Depo = LWH.NAME

  FROM " & FAYSDB & ".dbo.stk_FisLines SFL, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE LWH.EMAILADDR='fays@fays.com.tr' AND FWH.DepoKodu = SFL.Depo " & collationName & " AND LWH.NAME <> SFL.Depo " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "'AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_FisLines tablosundaki Depo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Fis Grup3"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE FWN SET  FWN.Grup3 = LWH.NAME

  FROM " & FAYSDB & ".dbo.stk_Fis FWN, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE  LWH.EMAILADDR='fays@fays.com.tr' AND FWH.DepoKodu = FWN.Grup3 " & collationName & " AND LWH.NAME <> FWN.Grup3 " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "'
  AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Fis tablosundaki Grup3 güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_TransLines Depo"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE TSL SET TSL.Depo = LWH.NAME

   FROM " & FAYSDB & ".dbo.stk_TransLines TSL, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE LWH.EMAILADDR='fays@fays.com.tr' AND FWH.DepoKodu = TSL.Depo " & collationName & " AND LWH.NAME <> TSL.Depo " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru  ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_TransLines tablosundaki Depo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Trans HedefDepo"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE TWN SET  TWN.HedefDepo = LWH.NAME

  FROM " & FAYSDB & ".dbo.stk_Trans TWN, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE LWH.EMAILADDR='fays@fays.com.tr' AND  FWH.DepoKodu = TWN.HedefDepo " & collationName & " AND LWH.NAME <> TWN.HedefDepo " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Trans tablosundaki HedefDepo güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Trans KaynakDepo"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE TWN SET  TWN.KaynakDepo = LWH.NAME

  FROM " & FAYSDB & ".dbo.stk_Trans TWN, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH, " & FAYSDB & ".dbo.stk_Depo FWH
  
  WHERE LWH.EMAILADDR='fays@fays.com.tr' AND FWH.DepoKodu = TWN.KaynakDepo " & collationName & " AND LWH.NAME <> TWN.KaynakDepo " & collationName & " AND (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Depo tablosundaki DepoKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Depo DepoKodu"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE FWH SET FWH.DepoKodu = LWH.NAME,FWH.Adres=LWH.EMAILADDR
FROM " & FAYSDB & ".dbo.stk_Depo FWH, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH
WHERE (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru
 AND  FWH.DepoKodu <> LWH.NAME " & collationName & " ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Depo tablosundaki DepoKodu güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#Region "stk_Depo Adres"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE FWH SET FWH.Adres=LWH.EMAILADDR
FROM " & FAYSDB & ".dbo.stk_Depo FWH, " & LOGOMDB & ".dbo.L_CAPIWHOUSE LWH
WHERE (CASE  
  WHEN FIRMNR < 10 then '00' + Convert(varchar(3),FIRMNR)
  WHEN FIRMNR < 100 then '0' + Convert(varchar(3),FIRMNR)
  ELSE Convert(varchar(3),FIRMNR) END) = '" & FirmNo & "' AND 
        (CASE  WHEN NR<10 THEN '00'+ CONVERT(varchar(3),NR)
		      WHEN NR <100 THEN '0' + CONVERT(varchar(3),NR)
		       ELSE CONVERT(varchar(3),NR) END) = FWH.DepoTuru
 AND  FWH.DepoKodu = LWH.NAME " & collationName & " and ISNULL(FWH.Adres,'')<>ISNULL(LWH.EMAILADDR,'') ")

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Depo tablosundaki Adres - Mail güncellemeleri yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region

#End Region
                rtbActions.Text += String.Concat(Environment.NewLine(), "********Logo'da çıkarılan kartlar Fays WMS üzerinden kaldırıldı********", Environment.NewLine())

#Region "stk_Kart Pasif"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE KART SET Aktif=0  FROM " & FAYSDB & ".dbo.stk_Kart  AS KART WITH (NOLOCK) JOIN DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON KART.logoref = LOGO.LOGICALREF
where ACTIVE = 1 and Aktif<>0 ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "Logo'da pasif olan kartlar Fays WMS pasif olarak ayarlandı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Kart Aktif"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("UPDATE KART SET Aktif=1  FROM " & FAYSDB & ".dbo.stk_Kart  AS KART WITH (NOLOCK) JOIN DBO.LG_" & FirmNo & "_ITEMS as LOGO WITH (NOLOCK) ON KART.logoref = LOGO.LOGICALREF
where ACTIVE = 0 and Aktif<>1 ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "Logo'da aktif olan kartlar Fays WMS aktif olarak ayarlandı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
#Region "stk_Kart Silme"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("delete from " & FAYSDB & ".dbo.stk_Kart
where ISNULL(logoref,'') not in (select LOGICALREF from LG_" & FirmNo & "_ITEMS WITH (NOLOCK) ) and stokkodu NOT IN (SELECT distinct(stokkodu) FROM " & FAYSDB & ".dbo.stk_fislines WITH (NOLOCK) )")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "stk_Kart tablosundaki hareketi olmayan ve LOGO'da karşılığı olmayan kartların kaldırılma işlemi yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region
                rtbActions.Text += String.Concat(Environment.NewLine(), "********Logo'da ambarların e-mail adresi bölümünde fays@fays.com.tr olmayan kartlar Fays WMS üzerinden kaldırıldı********", Environment.NewLine())
#Region "stk_depo kart silme"
                Try
                    Dim cmd As SqlCommand = sqlHelper.GetSqlCommand()
                    cmd.CommandText = String.Format("delete from " & FAYSDB & ".dbo.stk_Depo
where (ISNULL(DepoKodu,'') not in (select NAME " & collationName & " from " & LOGOMDB & ".dbo.L_CAPIWHOUSE WITH (NOLOCK) WHERE EMAILADDR='fays@fays.com.tr')) ")
                    cmd.CommandTimeout = 600

                    IslemBilgisi(cmd.ExecuteNonQuery(), "Logo ERP veri tabanında ambarlara ait e-mail adresi fays@fays.com.tr olmayan kartların stk_depo tablosundan kaldırılma işlemi yapıldı.")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
#End Region




            Catch ex As Exception
                MsgBox(ex.Message)
                Dim frmLogin As New LoginForm
                frmLogin.ShowDialog()
            End Try
#End Region

            sw.Stop()
            rtbActions.Text += String.Concat(Environment.NewLine(), "*********İşlem Süre Bilgisi***********", Environment.NewLine())
            rtbActions.Text += String.Concat(Environment.NewLine(), "İşlem süresi = " & sw.ElapsedMilliseconds, " ", "milisaniye")
            processTime = sw.ElapsedMilliseconds
        End If
        For i As Integer = 0 To rtbActions.Lines.Count - 1
            Dim startIndex As Integer = rtbActions.Text.IndexOf("(",
                                              rtbActions.GetFirstCharIndexFromLine(i))
            If startIndex > -1 Then
                Dim endIndex As Integer = rtbActions.Text.IndexOf(")", startIndex)
                If endIndex > -1 Then
                    rtbActions.Select(startIndex, endIndex - startIndex + 1)
                    rtbActions.SelectionColor = Color.Red
                End If
            End If
        Next
        rtbActions.SelectionStart = rtbActions.TextLength
        rtbActions.ScrollToCaret()
        MsgBox(String.Format("Güncelleme işlemi tamamlandı..." & Environment.NewLine & "Process time = " & processTime & " milisaniye"), MsgBoxStyle.OkOnly, Title:="Bilgi")

    End Sub
    Private Sub CloseMe(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
        Application.Exit()
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = " Fays Logo Güncelleme - Versiyon Bilgisi : " & Application.ProductVersion
        If CountInstances() > 1 Then Application.Exit()


        Dim frm As FrmSettings = New FrmSettings()



        If ConfigurationManager.AppSettings.Get("OtomatikGuncelleme").ToString().Equals("1") Then
            If CheckForInternetConnection() Then
                Dim indir As WebClient = New WebClient()
                Dim programVersiyon As String = Assembly.Load("FLG").GetName().Version.ToString()
                Dim guncelVersiyon As String = indir.DownloadString("http://www.fayscrm.com/Download/Tpic/FLG/versiyon.txt")

                If programVersiyon <> guncelVersiyon Then

                    If MessageBox.Show("Yeni bir sürüm yayınlandı.Yüklemek ister misiniz ?", "Uyarı", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        Process.Start($"{Application.StartupPath}\Guncelleme.exe")
                        Environment.Exit(0)
                    End If
                End If
            Else
                MessageBox.Show("İnternet bağlantınız olmadığı için yeni versiyon kontrol edilemedi.")
            End If
        End If

        lblFaysDB.Text = "Fays DB Adı: " + FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("FaysDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblLogoDB.Text = "LOGO DB Adı: " + FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblFirmaNo.Text = "Firma No: " + FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoFirmaNo"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        lblServerName.Text = "Sunucu Adı: " + FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")

        FAYSDB = FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("FaysDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        LOGODB = FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        FirmNo = FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoFirmaNo"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        ServerName = FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        LOGOMDB = FrmSettings.AES_Decrypt(LOGOMDB, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")

    End Sub
    Public Shared Function CheckForInternetConnection() As Boolean
        Try

            Using client = New WebClient()

                Using client.OpenRead("http://www.fayscrm.com")
                    Return True
                End Using
            End Using

        Catch
            Return False
        End Try
    End Function

    Private Shared Function CountInstances() As Integer

        Dim strAppLoc As String() = System.Reflection.Assembly.
        GetExecutingAssembly().Location.Split("\".ToCharArray())

        Dim strAppName As String = strAppLoc(strAppLoc.Length - 1)


        Dim strProcessQuery As String = "select name from CIM_Process where name = '" & strAppName & "'"

        Dim searcher As System.Management.ManagementObjectSearcher =
        New System.Management.ManagementObjectSearcher _
        (strProcessQuery)

        Dim intCountInstances As Integer = 0

        For Each item As System.Management.ManagementObject In
            searcher.[Get]()

            intCountInstances += 1

            If intCountInstances > 1 Then
                MessageBox.Show("FLG programı başka bir kullanıcı üzerinde açık bulunmaktadır.Görev yöneticisi üzerinden kontrol edebilirsiniz.")
                Exit For

            End If

        Next

        Return intCountInstances

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Application.Exit()
    End Sub
End Class
