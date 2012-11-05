VERSION 5.00
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3030
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   4560
   LinkTopic       =   "Form1"
   ScaleHeight     =   3030
   ScaleWidth      =   4560
   StartUpPosition =   3  '系統預設值
   Begin MSComDlg.CommonDialog CommonDialog1 
      Left            =   540
      Top             =   2400
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
      Caption         =   "Label1"
      Height          =   555
      Left            =   420
      TabIndex        =   1
      Top             =   1560
      Width           =   3495
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()

With CommonDialog1
    .FileName = ""
    .Filter = "BMP JPEG Files (*.bmp, *.jpg) |*.bmp;*.jpeg;*.jpg|All files (*.*) |*.*|"
    .ShowOpen
End With
If CommonDialog1.FileName = "" Then Exit Sub
Dim obj As New KwQRCodeReader
obj.FilePath = CommonDialog1.FileName
obj.decode
Label1.Caption = obj.Text



End Sub
