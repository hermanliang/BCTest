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
'       將 KwBarcode.dll, zxing.dll 放置於 D:\GlobalDll
'       編輯 Windows 環境變數，將 D:\GlobalDll 新增至 PATH 變數內 (讓執行階段可以 Access 到 KwBarcode.dll, zxing.dll)
'       註冊 KwBarcode.dll 至系統中
'
' * Comdlg.ocx not registered issue:
'       將 Comdlg32.ocx copy 至 C:\Windows\System32\ 資料夾
'       執行 regsvr32 %SystemRoot%\System32\comdlg32.ocx (好像不用也可以)
'
' ###################################################################################