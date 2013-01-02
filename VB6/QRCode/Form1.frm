VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "comdlg32.ocx"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3030
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   9630
   LinkTopic       =   "Form1"
   ScaleHeight     =   3030
   ScaleWidth      =   9630
   StartUpPosition =   3  '系統預設值
   Begin VB.CommandButton Command2 
      Caption         =   "Generate QRCode"
      Height          =   435
      Left            =   7800
      TabIndex        =   3
      Top             =   2460
      Width           =   1695
   End
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   360
      TabIndex        =   2
      Text            =   "Kaiwood QRCode Test"
      Top             =   2460
      Width           =   7215
   End
   Begin MSComDlg.CommonDialog CommonDialog2 
      Left            =   9120
      Top             =   0
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   8520
      Top             =   0
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Open QRCode Image"
      Height          =   1095
      Left            =   360
      TabIndex        =   0
      Top             =   180
      Width           =   1995
   End
   Begin VB.Label Label1 
      Height          =   555
      Left            =   420
      TabIndex        =   1
      Top             =   1560
      Width           =   8595
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
' ###################################################################################
' QRCode Encoder/Decoder VB6 範例程式
'
' * 需安裝 .NET Framework 2.0 (通常未更新過的 XP 需要，XP以上的系統基本上都已內建)
' * 需註冊 KwBarcode.dll 至系統中
'   [方法1: 使用 Reg File 註冊]
'       32-bit OS: 執行 KwBarcode_Reg_32bit.reg
'       64-bit OS: 執行 KwBarcode_Reg_64bit.reg
'
'   [方法2: 手動註冊方法]
'       於命令提示字元(cmd.exe) 輸入下面的命令 (若使用 Win7/Vista，需使用系統管理員身份開啟命令提示字元)
'           RegAsm KwBarcode.dll
'
'   [手動解除 KwBarcode.dll 註冊方法]
'       於命令提示字元(cmd.exe) 輸入下面的命令 (若使用 Win7/Vista，需使用系統管理員身份開啟命令提示字元)
'           RegAsm KwBarcode.dll /u
'
' * 完成上述步驟，將執行檔(Project1.exe) 與 KwBarcode.dll, zxing.dll 放置於同一目錄下即可正常執行。
'
' * 開發者設定:
'       選單->專案->設定引用項目->瀏覽->KwBarcode.tlb (查詢方法: 檢視->瀏覽物件->搜尋 KwQRCodeReader, KwQRCodeWriter)
'       將 KwBarcode.dll, zxing.dll 放置於相同目錄下
'       以codebase方式 註冊 KwBarcode.dll 至系統中
'       RegAsm KwBarcode.dll /codebase
'
' * Comdlg.ocx not registered issue:
'       將 Comdlg32.ocx copy 至 C:\Windows\System32\ 資料夾
'       執行 regsvr32 %SystemRoot%\System32\comdlg32.ocx (好像不用也可以)
'
' ###################################################################################

Option Explicit

Private Sub Command1_Click()
    ' QRCode Reader
    
    ' Open file Dialog
    With CommonDialog1
        .FileName = ""
        .Filter = "BMP JPEG Files (*.bmp, *.jpg) |*.bmp;*.jpeg;*.jpg|All files (*.*) |*.*|"
        .ShowOpen
    End With
    If CommonDialog1.FileName = "" Then Exit Sub
    
    ' QRCode Reader
    Dim obj As New KwQRCodeReader
    obj.FilePath = CommonDialog1.FileName
    
    ' 解碼 function 結果儲存於 obj.text 中
    ' Byte 資料儲存於 obj.RawByte (Byte Array)
    obj.decode
    Label1.Caption = obj.text

End Sub

Private Sub Command2_Click()
    ' QRCode 產生器
    
    Dim text As String
    text = Text1.text
    
    ' Save File Dialog
    With CommonDialog2
        .FileName = ""
        .Filter = "BMP Files (*.bmp) |*.bmp"
        .ShowOpen
    End With
    If CommonDialog2.FileName = "" Then Exit Sub
    
    'QRCode Writer 物件
    Dim obj As New KwQRCodeWriter
    Dim path As String
    path = CommonDialog2.FileName
    
    ' 產生 QRCode 並存成圖檔
    Call obj.encodeAndSave(text, path)

End Sub
