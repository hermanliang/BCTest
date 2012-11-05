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
   StartUpPosition =   3  '系統預設值
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
' ###################################################################################
' Barcode Encoder/Decoder VB6 範例程式
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
' * 完成上述步驟，將執行檔(Project1.exe) 與 KwBarcode.dll 放置於同一目錄下即可正常執行。
'
' * 開發者設定:
'       選單->專案->設定引用項目->瀏覽->KwBarcode.tlb (查詢方法: 檢視->瀏覽物件->搜尋 BarcodeCore)
'       將 KwBarcode.dll 放置於 D:\GlobalDll
'       編輯 Windows 環境變數，將 D:\GlobalDll 新增至 PATH 變數內 (讓執行階段可以 Access 到 KwBarcode.dll)
'       註冊 KwBarcode.dll 至系統中
'
' ###################################################################################

Option Explicit

Private Sub Command1_Click()

    Dim BCCore As New BarcodeCore
    Dim Bits(), Values() As Variant
    Dim oValues() As Long
    Dim Barcodes() As String
    Dim iTestName, iTestName2, oTestName, oTestName2 As String
    Dim f1, f2 As Single
    Dim i As Integer
    
    iTestName = "A-01" ' 4碼英數字含標點符號 bit = 28
    iTestName2 = "TEST" ' 4碼英數字 bit = 24
    f1 = -1.23 ' 浮點數 bit = 32
    
    ' ###################################################################################
    ' #                              輸入值設定區 (開始)                                #
    
    ' 輸入值 bit 大小列表
    ' Bit List (0-7, 0-7, 0-15, 0-255, 0-255, 4碼英數字含標點符號, 浮點數, 4碼英數字)
    Bits = Array( _
        3, _
        3, _
        4, _
        8, _
        8, _
        28, _
        32, _
        24 _
        )
    
    ' 輸入值列表 (值需符合 bit 的整數範圍，否則 Encode 後傳回值會為空)
    ' 文字轉整數 BarcodeCore.textToInt (24-bit), BarcodeCore.text128ToInt (28-bit)
    ' 浮點數轉整數 BarcodeCore.floatToInt
    Values = Array( _
        7, _
        4, _
        10, _
        200, _
        150, _
        BCCore.text128ToInt(iTestName), _
        BCCore.floatToInt(f1), _
        BCCore.textToInt(iTestName2) _
        )

    ' #                              輸入值設定區 (結束)                                #
    ' ###################################################################################
    
    ' 呼叫Barcode編碼函式，回傳編碼後的Barcode (String Array, 10碼/string)
    ' 若輸入值格式有誤，回傳值將為空
    Barcodes = initBarcodeEncode(Bits, Values)
    
'    Dim BitsL(7) As Long
'    Dim ValuesL(7) As Long
'    For i = 0 To UBound(Bits)
'        BitsL(i) = CLng(Bits(i))
'        ValuesL(i) = CLng(Values(i))
'    Next
'    Barcodes = initBarcodeEncode2(BitsL, ValuesL)
        
    
    Dim mBarcodes As String
    
    On Error GoTo Err1
    mBarcodes = ""
    For i = 0 To UBound(Barcodes)
        mBarcodes = mBarcodes + Barcodes(i)
    Next
    

    ' 呼叫 Barcode 解碼函式，回傳解碼後的數值 (Long Array)
    ' oValues = initBarcodeDecode(Barcodes, Bits)
    oValues = initBarcodeDecode2(mBarcodes, Bits)
    
    ' 解碼結果輸出
    Dim blockNumber As Integer
    blockNumber = UBound(Barcodes) + 1
    
    ' 整數轉回文字 BarcodeCore.intToText, BarcodeCore.intToText128
    oTestName = BCCore.intToText128(oValues(5))
    oTestName2 = BCCore.intToText(oValues(7))
    ' 整數轉回浮點數 BarcodeCore.intToFloat
    f2 = BCCore.intToFloat(oValues(6))
    
    
    ' 測試結果訊息
    MsgBox ( _
        "Barcodes(" + CStr(blockNumber) + " Blocks): " + mBarcodes + _
        vbCrLf + _
        "IN: " + _
        CStr(Values(0)) + " / " + _
        CStr(Values(1)) + " / " + _
        CStr(Values(2)) + " / " + _
        CStr(Values(3)) + " / " + _
        CStr(Values(4)) + " / " + _
        iTestName + " / " + _
        iTestName2 + " / " + _
        CStr(f1) + _
        vbCrLf + _
        "OUT: " + _
        CStr(oValues(0)) + " / " + _
        CStr(oValues(1)) + " / " + _
        CStr(oValues(2)) + " / " + _
        CStr(oValues(3)) + " / " + _
        CStr(oValues(4)) + " / " + _
        oTestName + " / " + _
        oTestName2 + " / " + _
        CStr(f2))
    Exit Sub
    
Err1:
    MsgBox ("格式錯誤!")

End Sub

'#########################################################################################################
'#  APIs                                                                                                 #
'#  Private Function initBarcodeEncode(BitLists() As Variant, ValueLists() As Variant) As String()       #
'#  Private Function initBarcodeEncode2(BitLists() As Long, ValueLists() As Long) As String()            #
'#  Private Function initBarcodeDecode(Barcodes() As String, BitLists() As Variant) As Long()            #
'#  Private Function initBarcodeDecode2(BarcodeStrings As String, BitLists() As Variant) As Long()       #
'#                                                                                                       #
'#########################################################################################################

Private Function initBarcodeEncode(BitLists() As Variant, ValueLists() As Variant) As String()
    If UBound(BitLists) <> UBound(ValueLists) Then
        MsgBox ("資料長度不一致")
        Exit Function
    End If
    
    Dim i As Integer
    Dim Obj As New BarcodeCore
    
    For i = 0 To UBound(BitLists)
        Obj.addBit (BitLists(i))
        Obj.addValue (ValueLists(i))
    Next
    
    Obj.initBarcodeEncoder
    
    On Error GoTo LErr
    
    initBarcodeEncode = Obj.Barcodes
    
LErr:

End Function

Private Function initBarcodeEncode2(BitLists() As Long, ValueLists() As Long) As String()
    If UBound(BitLists) <> UBound(ValueLists) Then
        MsgBox ("資料長度不一致")
        Exit Function
    End If
    
    Dim i As Integer
    Dim Obj As New BarcodeCore
    
    For i = 0 To UBound(BitLists)
        Obj.addBit (BitLists(i))
        Obj.addValue (ValueLists(i))
    Next
    
    Obj.initBarcodeEncoder
    
    On Error GoTo LErr
    
    initBarcodeEncode2 = Obj.Barcodes
    
LErr:

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

Private Function initBarcodeDecode2(BarcodeStrings As String, BitLists() As Variant) As Long()
    Dim Barcodes() As String
    Dim Obj As New BarcodeCore
    Obj.setBarcodes (BarcodeStrings)
    initBarcodeDecode2 = initBarcodeDecode(Obj.Barcodes, BitLists)
End Function
