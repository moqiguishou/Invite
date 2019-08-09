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
"Private WithEvents txt_test as MSForms.textbox" & vbCr & _
"Public num As Integer" & vbCr & _
"Private Sub UserForm_Initialize()" & vbCr & _												
"	UserForm1.Caption = ""周****吧(@′?`@)""" & vbCr & _
"	UserForm1.Picture = LoadPicture("""& WS.CurrentDirectory & "\BG.jpg"")" & vbCr & _
"	UserForm1.Width = 800" & vbCr & _
"	UserForm1.Height = 640" & vbCr & _
"	LoadButton_agree" & vbCr & _
"	LoadButton_disagree" & vbCr & _
"	LoadTextBox" & vbCr & _
"End Sub" & vbCr & _
"Public Function GetXCursorPos() As Long" & vbCrLf & _ 
"Dim pt As POINTAPI : GetCursorPos pt : GetXCursorPos = pt.X" & vbCrLf & _ 
"End Function" & vbCrLf & _ 
"Public Function GetYCursorPos() As Long" & vbCrLf & _ 
"Dim pt As POINTAPI: GetCursorPos pt : GetYCursorPos = pt.Y" & vbCrLf & _ 
"End Function" & vbCrLf & _ 
" 'Private Sub UserForm_Click()" & vbCr & _
" '   MsgBox ""test"",vbYesNoCancel+vbInformation+vbDefaultButton1,""ForTest""" & vbCr & _
" 'End Sub" & vbCr & _
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
"	txt_test.Text = ""moqi""" & vbCr & _
"End Sub" & vbCr & _
"Private Sub LoadButton_disagree()" & vbCr & _	
"	Set Button_DisAgree = Controls.Add(""Forms.Image.1"", ""Image"", True)" & vbCr & _
"	With Button_DisAgree" & vbCr & _
"	.Visible = True" & vbCr & _
"	.Width = 150" & vbCr & _
"	.Height = 120" & vbCr & _
"	.Picture = LoadPicture("""& WS.CurrentDirectory & "\niu.ico"")" & vbCr & _
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
"End Sub"& vbCrLf & _ 
"Private Sub LoadTextBox()" & vbCrLf & _ 
"	Set txt_test = Controls.Add(""Forms.TextBox.1"", ""TextBox"", True)" & vbCrLf & _ 
"	With txt_test" & vbCr & _
"	.Visible = True" & vbCr & _
"	.Width = 200" & vbCr & _
"	.Height = 30" & vbCr & _
"	.Top = 100" & vbCr & _
"	.Left = 10" & vbCr & _
"	.Text = ""ss""" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"	Public Sub SetText(str As String)" & vbCr & _
"txt_test.Text = str" & vbCr & _
"End Sub" & vbCr & _
"Private Sub UserForm_MouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Single, ByVal Y As Single)" & vbCr & _
"	If Button = 1 Then" & vbCr & _
"		MsgBox ""Hello, "",vbYesNoCancel+vbInformation+vbDefaultButton1,""提示:""" & vbCr & _
"		txt_test.Text = num" & vbCr & _
"	End If" & vbCr & _
"	If Button = 2 Then" & vbCr & _
"		MsgBox ""Hello2, "",vbYesNoCancel+vbInformation+vbDefaultButton1,""提示:""" & vbCr & _
"	End If" & vbCr & _
"End Sub" & vbCr & _
"Public Sub numJia()" & vbCr & _
"	num = num+1" & vbCr & _
"End Sub"
Form1.CodeModule.AddFromString strForm1   '添加代码

'=========================模块===================================='
strCode = strCode & vbCr & _
"Private Declare PtrSafe Function SetTimer Lib ""user32.dll"" (ByVal hwnd As Long, ByVal nIDEvent As Long, ByVal uElapse As Long, ByVal lpTimerFunc As Long) As longprt" & vbCr & _
"Private Declare PtrSafe Function KillTimer Lib ""user32.dll"" (ByVal hwnd As Long, ByVal nIDEvent As Long) As Long " & vbCr & _
"Public lTimerID As long" & vbCr & _
"Sub LoadForm()" & vbCr & _
"   Dim count" & vbCr & _
" 	count = 2" & vbCr & _
"	StartTimer" & vbCr & _
"	do Until count = 0" & vbCr & _
"		UserForm1.show" & vbCr & _
"		count = count - 1" & vbCr & _
"	Loop" & vbCr & _
"End Sub" & vbCr & _
"Sub StartTimer()" & vbCr & _
"	If lTimerID = 0 Then" & vbCr & _
"		lTimerID = SetTimer(0&, 0&, 1, AddressOf OnTime)" & vbCr & _
"	Else" & vbCr & _
"		Call StopTimer" & vbCr & _
"		lTimerID = SetTimer(0&, 0&, 1, AddressOf OnTime)" & vbCr & _
"	End If" & vbCr & _
"End Sub" & vbCr & _
"Sub StopTimer()" & vbCr & _
"	KillTimer 0&, lTimerID" & vbCr & _
"End Sub" & vbCr & _
"Sub OnTime(ByVal HWnd As Long, ByVal uMsg As Long, ByVal nIDEvent As Long, ByVal dwTimer As Long)" & vbCr & _
"	'UserForm1.numJia" & vbCr & _
"End Sub"
'Sub XunH()
'	isRun = False
'	while not isRun
'		
'	Wend
'End Sub



oModule.CodeModule.AddFromString strCode  '添加代码

oexcel.run "LoadForm"