Dim WS
Dim strRegKey
set WS = CreateObject("wscript.Shell") 



WS.RegWrite "HKEY_CURRENT_USER\Software\Microsoft\Office\11.0\Excel\Security\AccessVBOM",1,"REG_DWORD" 
WS.RegWrite "HKEY_CURRENT_USER\Software\Microsoft\Office\12.0\Excel\Security\AccessVBOM",1,"REG_DWORD" 
WS.RegWrite "HKEY_CURRENT_USER\Software\Microsoft\Office\14.0\Excel\Security\AccessVBOM",1,"REG_DWORD" 
WS.RegWrite "HKEY_CURRENT_USER\Software\Microsoft\Office\15.0\Excel\Security\AccessVBOM",1,"REG_DWORD" 
WS.RegWrite "HKEY_CURRENT_USER\Software\Microsoft\Office\16.0\Excel\Security\AccessVBOM",1,"REG_DWORD" 

Dim oExcel, oBook, oModule 
Dim Form1,Form2,Form3
Set oExcel = CreateObject("excel.application")
Set oBook = oExcel.Workbooks.Add 
Set oModule = obook.VBProject.VBComponents.Add(1)  '模块
Set Form1 = obook.VBProject.VBComponents.Add(3)   '用户窗口1
'Set Form2 = obook.VBProject.VBComponents.Add(3)   '用户窗口2
'Set Form3 = obook.VBProject.VBComponents.Add(3)   '用户窗口2

strRegKey = "HKEY_CURRENT_USER\Software\Microsoft\Office\$\Excel\Security\AccessVBOM"
strRegKey = Replace(strRegKey, "$", oExcel.Version)
WS.RegWrite strRegKey, 1, "REG_DWORD"
'========================Main窗口==============================='
strForm1 = _
"Option Explicit" & vbCrLf & _ 
"Private Type POINTAPI : X As Long : Y As Long : End Type" & vbCrLf & _ 
"Private Declare PtrSafe Function GetCursorPos Lib ""user32"" (lpPoint As POINTAPI) As Long" & vbCrLf & _ 
"Private WithEvents Button_Agree As CommandButton" & vbCr & _
"Private WithEvents Button_DisAgree As Image" & vbCr & _
"Private WithEvents txt_test As TextBox" & vbCr & _
"Private Sub UserForm_Initialize()" & vbCr & _
"	UserForm1.Caption = ""周****吧(@′?`@)""" & vbCr & _
"	UserForm1.Width = 800" & vbCr & _
"	UserForm1.Height = 640" & vbCr & _
"	LoadButton_agree" & vbCr & _
"	LoadButton_disagree" & vbCr & _
"	LoadTextBox" & vbCr & _
"	'LoadImg" & vbCr & _
"End Sub" & vbCr & _
"Public Function GetXCursorPos() As Long" & vbCrLf & _ 
"Dim pt As POINTAPI : GetCursorPos pt : GetXCursorPos = pt.X" & vbCrLf & _ 
"End Function" & vbCrLf & _ 
"Public Function GetYCursorPos() As Long" & vbCrLf & _ 
"Dim pt As POINTAPI: GetCursorPos pt : GetYCursorPos = pt.Y" & vbCrLf & _ 
"End Function" & vbCrLf & _ 
"Private Sub UserForm_Click()" & vbCr & _
"   MsgBox ""test"",vbYesNoCancel+vbInformation+vbDefaultButton1,""ForTest""" & vbCr & _
"End Sub" & vbCr & _
"Private Sub LoadButton_agree()" & vbCr & _
"	Set Button_Agree = Controls.Add(""Forms.CommandButton.1"", ""button"", True)" & vbCr & _
"	With Button_Agree" & vbCr & _
"	.Visible = True" & vbCr & _
"	.Width = 70" & vbCr & _
"	.Caption = ""同意了！""" & vbCr & _
"	.Top = 10" & vbCr & _
"	.Left = 10" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Button_Agree_Click()" & vbCr & _
"	MsgBox ""Hello, "",vbYesNoCancel+vbInformation+vbDefaultButton1,""提示:""" & vbCr & _
"End Sub" & vbCr & _
"Private Sub LoadButton_disagree()" & vbCr & _
"	Set Button_DisAgree = Controls.Add(""Forms.Image.1"", ""Image"", True)" & vbCr & _
"	With Button_DisAgree" & vbCr & _
"	.Visible = True" & vbCr & _
"	.Width = 150" & vbCr & _
"	.Height = 120" & vbCr & _
"	.Top = 30" & vbCr & _
"	.Left = 10" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Button_DisAgree_Click()" & vbCr & _
"	Dim x1 As Integer" & vbCr & _
"	Dim y1 As Integer" & vbCr & _
"	Dim Pos As String" & vbCr & _
"	x1 = GetXCursorPos" & vbCr & _
"	y1 = GetYCursorPos" & vbCr & _
"	Pos = x1 & "" "" & y1" & vbCr & _
"	MsgBox Pos,vbYesNoCancel+vbInformation+vbDefaultButton1,""拒绝:""" & vbCr & _
"End Sub" & vbCr & _
"Private Sub LoadTextBox() As Long" & vbCrLf & _ 
"Set Button_DisAgree = Controls.Add(""Forms.Image.1"", ""Image"", True)" & vbCrLf & _ 
"End Sub"
Form1.CodeModule.AddFromString strForm1   '添加代码

'=========================模块===================================='
strCode = strCode & vbCr & _
"Sub LoadForm()" & vbCr & _
"   Dim count" & vbCr & _
" 	count = 2" & vbCr & _
"	do Until count = 0" & vbCr & _
"	UserForm1.show" & vbCr & _
"	count = count - 1" & vbCr & _
"	Loop" & vbCr & _
"End Sub"
oModule.CodeModule.AddFromString strCode  '添加代码

oexcel.run "LoadForm"