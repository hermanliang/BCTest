VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3030
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   4560
   LinkTopic       =   "Form1"
   ScaleHeight     =   3030
   ScaleWidth      =   4560
   StartUpPosition =   3  '╰参箇砞
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   555
      Left            =   1260
      TabIndex        =   0
      Top             =   960
      Width           =   1695
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command1_Click()

    Dim BCCore As New BarcodeCore
    Dim Bits(), Values() As Variant
    Dim oValues() As Long
    Dim Barcodes() As String
    Dim iTestName, oTestName As String
    Dim f1, f2 As Single
    Dim i As Integer
    
    iTestName = "Test" ' 4絏璣计 bit = 24
    f1 = -1.23 ' 疊翴计 bit = 32
    
    ' Bit List (0-7, 0-7, 0-15, 0-255, 0-255, 4絏璣计, 疊翴计)
    Bits = Array(3, 3, 4, 8, 8, 24, 32)
    
    ' Value List
    ' ゅ锣俱计 BarcodeCore.textToInt
    ' 疊翴计锣俱计 BarcodeCore.floatToInt
    Values = Array(3, 4, 10, 200, 150, BCCore.textToInt(iTestName), BCCore.floatToInt(f1))
    
    Barcodes = initBarcodeEncode(Bits, Values)
    
    oValues = initBarcodeDecode(Barcodes, Bits)
    
    ' 俱计锣ゅ BarcodeCore.intToText
    oTestName = BCCore.intToText(oValues(5))
    ' 俱计锣疊翴计 BarcodeCore.intToFloat
    f2 = BCCore.intToFloat(oValues(6))
    
    Dim mBarcodes As String
    mBarcodes = ""
    For i = 0 To UBound(Barcodes)
        mBarcodes = mBarcodes + Barcodes(i)
    Next
    
    
    MsgBox ( _
        "Barcodes: " + mBarcodes + _
        vbCrLf + _
        "IN: " + _
        CStr(Values(0)) + " / " + _
        CStr(Values(1)) + " / " + _
        CStr(Values(2)) + " / " + _
        CStr(Values(3)) + " / " + _
        CStr(Values(4)) + " / " + _
        iTestName + " / " + _
        CStr(f1) + _
        vbCrLf + _
        "OUT: " + _
        CStr(oValues(0)) + " / " + _
        CStr(oValues(1)) + " / " + _
        CStr(oValues(2)) + " / " + _
        CStr(oValues(3)) + " / " + _
        CStr(oValues(4)) + " / " + _
        oTestName + " / " + _
        CStr(f2))


End Sub

Private Function initBarcodeEncode(BitLists() As Variant, ValueLists() As Variant) As String()
    If UBound(BitLists) <> UBound(ValueLists) Then
        MsgBox ("戈ぃ璓")
        Exit Function
    End If
    
    Dim i As Integer
    Dim Obj As New BarcodeCore
    
    For i = 0 To UBound(BitLists)
        Obj.addBit (BitLists(i))
        Obj.addValue (ValueLists(i))
    Next
    
    Obj.initBarcodeEncoder
    initBarcodeEncode = Obj.Barcodes

End Function

Private Function initBarcodeDecode(Barcodes() As String, BitLists() As Variant) As Long()

    Dim Obj As New BarcodeCore
    Dim i As Integer
    For i = 0 To UBound(Barcodes)
        Obj.addBarcode (Barcodes(i))
    Next
    For i = 0 To UBound(BitLists)
        Obj.addBit (BitLists(i))
    Next
    Obj.initBarcodeDecoder
    initBarcodeDecode = Obj.outValues
    
End Function
