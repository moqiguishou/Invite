Dim WS 
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
Set Form2 = obook.VBProject.VBComponents.Add(3)   '用户窗口2
Set Form3 = obook.VBProject.VBComponents.Add(3)   '用户窗口2
'Set Btn = obook.VBProject.VBComponents.Add(3).
'Set Btn = obook.VBProject.CommandBars.Add

''''''''''''''''''''''''''' VB代码区'''''''''''''''''''''''''''''''''''
''''''''''''''''''''''''''' 窗口1代码'''''''''''''''''''''''''''''''''''
strForm1 = _
"Private WithEvents Button1 As CommandButton" & vbCr & _
"Private WithEvents Button2 As CommandButton" & vbCr & _
"Private WithEvents Button3 As CommandButton" & vbCr & _
"Private WithEvents Image1 As Image" & vbCr & _
"Dim aX As Single, aY As Single" & vbCr & _
"                               " & vbCr & _
"Private Sub UserForm_Initialize()" & vbCr & _
"	UserForm1.Caption = ""邀请函""" & vbCr & _
"	UserForm1.Picture = LoadPicture(""" & WS.CurrentDirectory & "\background.jpg"")" & vbCr & _
"	UserForm1.Width = 640" & vbCr & _
"	UserForm1.Height = 480" & vbCr & _
"	LoadButton1 '载入控件" & vbCr & _
"	LoadButton2" & vbCr & _
"	LoadButton3" & vbCr & _
"	LoadImg" & vbCr & _
"End Sub" & vbCr & _
"Private Sub UserForm_Click()" & vbCr & _
"   Form1Beeps 1000, 500" & vbCr & _
"End Sub" & vbCr & _
"Private Sub UserForm_DblClick(ByVal Cancel As MSForms.ReturnBoolean)" & vbCr & _
"   Music" & vbCr & _
"End Sub" & vbCr & _
"Sub LoadButton1()" & vbCr & _
"	Set Button1 = Controls.Add(""Forms.CommandButton.1"", ""button"", True)" & vbCr & _
"	With Button1" & vbCr & _
"	.Visible = True '显示出来" & vbCr & _
"	.Width = 70" & vbCr & _
"	.Caption = ""Hello""" & vbCr & _
"	.Top = 10" & vbCr & _
"	.Left = 10" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Button1_Click()" & vbCr & _
"	MsgBox ""Hello, 这是一个提示对话框"",vbInformation,""提示:""" & vbCr & _
"End Sub" & vbCr & _
"Sub LoadButton2()" & vbCr & _
"	Set Button2 = Controls.Add(""Forms.CommandButton.1"", ""button2"", True)" & vbCr & _
"	With Button2" & vbCr & _
"	.Visible = True '显示出来" & vbCr & _
"	.Width = 70" & vbCr & _
"	.Caption = ""播放Flash""" & vbCr & _
"	.Top = 10" & vbCr & _
"	.Left = 100" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Button2_Click()" & vbCr & _
"	UserForm2.Show" & vbCr & _
"End Sub" & vbCr & _
"Sub LoadButton3()" & vbCr & _
"	Set Button3 = Controls.Add(""Forms.CommandButton.1"", ""button3"", True)" & vbCr & _
"	With Button3" & vbCr & _
"	.Visible = True '显示出来" & vbCr & _
"	.Width = 70" & vbCr & _
"	.Caption = ""打开WEB""" & vbCr & _
"	.Top = 10" & vbCr & _
"	.Left = 190" & vbCr & _
"	End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Button3_Click()" & vbCr & _
"	UserForm3.Show" & vbCr & _
"End Sub" & vbCr & _
"         ''Imaage控件" & vbCr & _
"Sub LoadImg()" & vbCr & _
"	Set Image1 = Controls.Add(""Forms.Image.1"", ""image"", True)" & vbCr & _
"	With Image1" & vbCr & _
"		.Visible = True '显示出来" & vbCr & _
"		.Width = 150" & vbCr & _
"		.Height = 120" & vbCr & _
"		.Top = 100" & vbCr & _
"		.Left = 10" & vbCr & _
"		.Picture = LoadPicture(" & """" & WS.CurrentDirectory & "" & "\background.jpg"")" & vbCr & _
"    End With" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Image1_MouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Single, ByVal Y As Single)" & vbCr & _
"    aX = X" & vbCr & _
"    aY = Y" & vbCr & _
"End Sub" & vbCr & _
"Private Sub Image1_MouseMove(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Single, ByVal Y As Single)" & vbCr & _
"    If Button = 1 Then" & vbCr & _
"        Image1.Move Image1.Left + (X - aX), Image1.Top + (Y - dY)" & vbCr & _
"    End If" & vbCr & _
"End Sub"
Form1.CodeModule.AddFromString strForm1   '添加代码

''''''''''''''''''''''''''' 窗口2代码'''''''''''''''''''''''''''''''''''
strForm2 = _
"Dim FCtl As Object" & vbCr & _
"Private Sub UserForm_Initialize()" & vbCr & _
"	 UserForm2.Caption = ""Flash播放器""" & vbCr & _
"	 UserForm2.Width=480" & vbCr & _
"	 UserForm2.Height=320" & vbCr & _
"	 Flash" & vbCr & _
"End Sub" & vbCr & _
"Sub Flash()" & vbCr & _
"    Set FCtl = UserForm2.Controls.Add(""ShockwaveFlash.ShockwaveFlash"", ""Flash"")" & vbCr & _
"    FCtl.Left = 0" & vbCr & _
"    FCtl.Top = 0" & vbCr & _
"    FCtl.Width = UserForm2.Width" & vbCr & _
"    FCtl.Height = UserForm2.Height" & vbCr & _
"    FCtl.Visible = True" & vbCr & _
"    FCtl.Object.loadmovie 0, " & """" & WS.CurrentDirectory & "" & "\人生路.swf""" & vbCr & _
"End Sub"
Form2.CodeModule.AddFromString strForm2   '添加代码

''''''''''''''''''''''''''' 窗口3代码'''''''''''''''''''''''''''''''''''
strForm3 = _
"Dim WebBrowser1 As Object" & vbCr & _
"Private Sub UserForm_Initialize()" & vbCr & _
"	 UserForm3.Caption = ""我的网页""" & vbCr & _
"	 UserForm3.Width=1024" & vbCr & _
"	 UserForm3.Height=768" & vbCr & _
"	 Web" & vbCr & _
"End Sub" & vbCr & _
"Sub Web()" & vbCr & _
"	Set WebBrowser1=Me.Controls.Add(""SHELL.EXPLORER.2"", ""web1"")" & vbCr & _
"	WebBrowser1.Visible = True" & vbCr & _
"	WebBrowser1.Move 0, 0, Me.Width, Me.Height" & vbCr & _
"	WebBrowser1.Navigate ""www.baidu.com""" & vbCr & _
"End Sub"
Form3.CodeModule.AddFromString strForm3   '添加代码

sys_X86orX64 = X86orX64()

''''''''''''''''''''''''''' 模块代码'''''''''''''''''''''''''''''''''''
If sys_X86orX64 = 0 Then			' System 32bit
strCode = _ 
"Private Declare Function Beep Lib ""kernel32"" Alias ""Beep"" (ByVal dwFreq As Long, ByVal dwDuration As Long) As Long"
ElseIf sys_X86orX64 = 1 Then		' System 64bit
strCode = _ 
"Private Declare PtrSafe Function Beep Lib ""kernel32"" Alias ""Beep"" (ByVal dwFreq As Long, ByVal dwDuration As Long) As Long"
End If

strCode = strCode & vbCr & _
"Sub Form1Beeps(x as Long, y as Long) 'Form1Beeps自定义函数" & vbCr & _   
"	Beep x, y" & vbCr & _ 
"End Sub" & vbCr & _
"Sub LoadForm() '窗口载入函数" & vbCr & _
"   Dim count"& vbCr & _
" 	count = 10"& vbCr & _
"	do Until count = 0"& vbCr & _
"	UserForm1.show" & vbCr & _
"	count = count - 1" & vbCr & _
"	Loop"& vbCr & _
"End Sub" & vbCr & _
"Sub Music()  '播放音乐" & vbCr & _
"	Beep 523,500" & vbCr & _
"	Beep 587,500" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 523,500" & vbCr & _
"	Beep 523,500" & vbCr & _
"	Beep 587,500" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 523,500" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 698,500" & vbCr & _
"	Beep 784,750" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 698,500" & vbCr & _
"	Beep 784,750" & vbCr & _
"	Beep 784,300" & vbCr & _
"	Beep 880,300" & vbCr & _
"	Beep 784,300" & vbCr & _
"	Beep 698,300" & vbCr & _
"	Beep 659,400" & vbCr & _
"	Beep 523,400" & vbCr & _
"	Beep 784,300" & vbCr & _
"	Beep 880,300" & vbCr & _
"	Beep 784,300" & vbCr & _
"	Beep 698,300" & vbCr & _
"	Beep 659,400" & vbCr & _
"	Beep 523,400" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 391,500" & vbCr & _
"	Beep 523,500" & vbCr & _
"	Beep 659,500" & vbCr & _
"	Beep 391,500" & vbCr & _
"	Beep 523,500" & vbCr & _
"End Sub"
oModule.CodeModule.AddFromString strCode  '添加代码

oexcel.run "LoadForm"
DXX   
oExcel.DisplayAlerts = False 
oBook.Close 
oExcel.Quit 

Sub DXX()
	'oexcel.run "loadform"
End Sub

Sub BeepMusic()
	oExcel.Run "Form1Beeps",500,100
	oExcel.Run "Form1Beeps",550,100
	oExcel.Run "Form1Beeps",600,100
	oExcel.Run "Form1Beeps",650,100
	oExcel.Run "Form1Beeps",700,700
	WScript.Sleep 200
	oExcel.Run "Form1Beeps",700,100
	oExcel.Run "Form1Beeps",650,100
	oExcel.Run "Form1Beeps",600,100
	oExcel.Run "Form1Beeps",550,100
	oExcel.Run "Form1Beeps",500,700
End Sub

Function X86orX64()
	On Error Resume Next
	strComputer = "."
	Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\cimv2")
	Set colItems = objWMIService.ExecQuery("Select * from Win32_ComputerSystem",,48)
	
	For Each objItem in colItems
		If InStr(objItem.SystemType, "86") <> 0 Then
			X86orX64 = 0
		ElseIf InStr(objItem.SystemType, "64") <> 0 Then
			X86orX64 = 1
		Else
			X86orX64 = objItem.SystemType
		End If
	Next
	return X86orX64
End Function
