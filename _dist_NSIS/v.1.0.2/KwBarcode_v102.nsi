; 該腳本使用 HM VNISEdit 腳本編輯器嚮導產生

; 安裝程序初始定義常量
!define PRODUCT_NAME "Kaiwood Barcode"
!define PRODUCT_VERSION "1.0.2"
!define PRODUCT_PUBLISHER "Kaiwood"
!define PRODUCT_WEB_SITE "http://www.kaiwood.com"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

!define KEY_UNINST_HEAD_ROOT_X86 "HKCR"
!define KEY_UNINST_ROOT_X86 "HKCR"
!define KEY_UNINST_QR_WRITER_HEAD_X86 "KwQRCodeWriter"
!define KEY_UNINST_QR_WRITER_X86 "CLSID\{69F37639-F632-433F-AA01-1BC326FD1D6F}"
!define KEY_UNINST_QR_READER_HEAD_X86 "KwQRCodeReader"
!define KEY_UNINST_QR_READER_X86 "CLSID\{3E73EE86-F46D-411D-BE6F-87060B7E6E6A}"
!define KEY_UNINST_BARCODE_HEAD_X86 "BarcodeCore"
!define KEY_UNINST_BARCODE_X86 "CLSID\{7C6D4FCD-073B-4585-943F-BCF0BB6404FD}"

!define KEY_UNINST_HEAD_ROOT_X64 "HKLM"
!define KEY_UNINST_ROOT_X64 "HKCR"
!define KEY_UNINST_QR_WRITER_HEAD_X64 "SOFTWARE\Classes\KwQRCodeWriter"
!define KEY_UNINST_QR_WRITER_X64 "Wow6432Node\${KEY_UNINST_QR_WRITER_X86}"
!define KEY_UNINST_QR_READER_HEAD_X64 "SOFTWARE\Classes\KwQRCodeReader"
!define KEY_UNINST_QR_READER_X64 "Wow6432Node\${KEY_UNINST_QR_READER_X86}"
!define KEY_UNINST_BARCODE_HEAD_X64 "SOFTWARE\Classes\BarcodeCore"
!define KEY_UNINST_BARCODE_X64 "Wow6432Node\${KEY_UNINST_BARCODE_X86}"

SetCompressor lzma

; ------ MUI 現代界面定義 (1.67 版本以上兼容) ------
!include "MUI.nsh"
!include "x64.nsh"

; MUI 預定義常量
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; 歡迎頁面
!insertmacro MUI_PAGE_WELCOME
; 安裝過程頁面
!insertmacro MUI_PAGE_INSTFILES
; 安裝完成頁面
!insertmacro MUI_PAGE_FINISH

; 安裝卸載過程頁面
!insertmacro MUI_UNPAGE_INSTFILES

; 安裝界面包含的語言設置
!insertmacro MUI_LANGUAGE "English"

; 安裝預釋放文件
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI 現代界面定義結束 ------

Name "${PRODUCT_NAME}"
OutFile "KwBarcode_v102.exe"
InstallDir "$PROGRAMFILES\Kaiwood\Barcode\v.1.0.2"
ShowInstDetails show
ShowUnInstDetails show

Section "Main" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File "zxing.dll"
  File "KwBarcode.dll"
SectionEnd

;Section -AdditionalIcons
;  CreateDirectory "$SMPROGRAMS\Kaiwood\Barcode\v.1.0.2"
;  CreateShortCut "$SMPROGRAMS\Kaiwood\Barcode\v.1.0.2\Uninstall.lnk" "$INSTDIR\uninst.exe"
;SectionEnd

Section -Post
        WriteUninstaller "$INSTDIR\uninst.exe"
        WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
        WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
        WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
        WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
        WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
        ${If} ${RunningX64}
        	WriteRegStr   HKLM "${KEY_UNINST_QR_WRITER_HEAD_X64}" "" "KwBarcode.KwQRCodeWriter"
        	WriteRegStr   HKLM "${KEY_UNINST_QR_WRITER_HEAD_X64}\CLSID" "" "{69F37639-F632-433F-AA01-1BC326FD1D6F}"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}" "" "KwBarcode.KwQRCodeWriter"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "" "mscoree.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "ThreadingModel" "Both"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "Class" "KwBarcode.KwQRCodeWriter"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32\1.0.2.0" "Class" "KwBarcode.KwQRCodeWriter"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X64}\ProgId" "" "KwQRCodeWriter"
        	WriteRegStr   HKLM "${KEY_UNINST_QR_READER_HEAD_X64}" "" "KwBarcode.KwQRCodeReader"
        	WriteRegStr   HKLM "${KEY_UNINST_QR_READER_HEAD_X64}\CLSID" "" "{3E73EE86-F46D-411D-BE6F-87060B7E6E6A}"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}" "" "KwBarcode.KwQRCodeReader"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "" "mscoree.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "ThreadingModel" "Both"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "Class" "KwBarcode.KwQRCodeReader"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32\1.0.2.0" "Class" "KwBarcode.KwQRCodeReader"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X64}\ProgId" "" "KwQRCodeReader"
        	WriteRegStr   HKLM "${KEY_UNINST_BARCODE_HEAD_X64}\" "" "KwBarcode.BarcodeCore"
        	WriteRegStr   HKLM "${KEY_UNINST_BARCODE_HEAD_X64}\CLSID" "" "{7C6D4FCD-073B-4585-943F-BCF0BB6404FD}"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}" "" "KwBarcode.BarcodeCore"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "" "mscoree.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "ThreadingModel" "Both"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "Class" "KwBarcode.BarcodeCore"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32\1.0.2.0" "Class" "KwBarcode.BarcodeCore"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
        	WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X64}\ProgId" "" "BarcodeCore"

        ${Else}
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_HEAD_X86}" "" "KwBarcode.KwQRCodeWriter"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_HEAD_X86}\CLSID" "" "{69F37639-F632-433F-AA01-1BC326FD1D6F}"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}" "" "KwBarcode.KwQRCodeWriter"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "" "mscoree.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "ThreadingModel" "Both"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "Class" "KwBarcode.KwQRCodeWriter"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32\1.0.2.0" "Class" "KwBarcode.KwQRCodeWriter"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_WRITER_X86}\ProgId" "" "KwQRCodeWriter"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_HEAD_X86}" "" "KwBarcode.KwQRCodeReader"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_HEAD_X86}" "" "{3E73EE86-F46D-411D-BE6F-87060B7E6E6A}"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}" "" "KwBarcode.KwQRCodeReader"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "" "mscoree.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "ThreadingModel" "Both"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "Class" "KwBarcode.KwQRCodeReader"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32\1.0.2.0" "Class" "KwBarcode.KwQRCodeReader"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_QR_READER_X86}\ProgId" "" "KwQRCodeReader"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_HEAD_X86}" "" "KwBarcode.BarcodeCore"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_HEAD_X86}" "" "{7C6D4FCD-073B-4585-943F-BCF0BB6404FD}"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}" "" "KwBarcode.BarcodeCore"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "" "mscoree.dll"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "ThreadingModel" "Both"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "Class" "KwBarcode.BarcodeCore"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32\1.0.2.0" "Class" "KwBarcode.BarcodeCore"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32\1.0.2.0" "Assembly" "KwBarcode, Version=1.0.2.0, Culture=neutral, PublicKeyToken=1ae9e675a4cd0779"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32\1.0.2.0" "RuntimeVersion" "v2.0.50727"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\InprocServer32\1.0.2.0" "CodeBase" "$INSTDIR\kwbarcode.dll"
                WriteRegStr   HKCR "${KEY_UNINST_BARCODE_X86}\ProgId" "" "BarcodeCore"
        ${EndIf}

SectionEnd

/******************************
 *  以下是安裝程序的卸載部分  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\KwBarcode.dll"
  Delete "$INSTDIR\zxing.dll"

;  Delete "$SMPROGRAMS\Kaiwood\Barcode\v.1.0.2\Uninstall.lnk"

;  RMDir "$SMPROGRAMS\Kaiwood\Barcode\v.1.0.2"

  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
        ${If} ${RunningX64}
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X64} "${KEY_UNINST_QR_WRITER_HEAD_X64}"
                DeleteRegKey ${KEY_UNINST_ROOT_X64} "${KEY_UNINST_QR_WRITER_X64}"
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X64} "${KEY_UNINST_QR_READER_HEAD_X64}"
                DeleteRegKey ${KEY_UNINST_ROOT_X64} "${KEY_UNINST_QR_READER_X64}"
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X64} "${KEY_UNINST_BARCODE_HEAD_X64}"
                DeleteRegKey ${KEY_UNINST_ROOT_X64} "${KEY_UNINST_BARCODE_X64}"
        ${Else}
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X86} "${KEY_UNINST_QR_WRITER_HEAD_X86}"
                DeleteRegKey ${KEY_UNINST_ROOT_X86} "${KEY_UNINST_QR_WRITER_X86}"
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X86} "${KEY_UNINST_QR_READER_HEAD_X86}"
                DeleteRegKey ${KEY_UNINST_ROOT_X86} "${KEY_UNINST_QR_READER_X86}"
                DeleteRegKey ${KEY_UNINST_HEAD_ROOT_X86} "${KEY_UNINST_BARCODE_HEAD_X86}"
                DeleteRegKey ${KEY_UNINST_ROOT_X86} "${KEY_UNINST_BARCODE_X86}"
        ${EndIf}



  SetAutoClose true
SectionEnd

#-- 根據 NSIS 腳本編輯規則，所有 Function 區段必須放置在 Section 區段之後編寫，以避免安裝程序出現未可預知的問題。--#

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Uninatall $(^Name) ?" IDYES +2
  Abort
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) unistall succeeded!"
FunctionEnd
