REM 本机ip:192.168.0.150	密码:1 用户名:  Showlo
net use \\192.168.0.150\ipc$ "1" /user:"Showlo"
REM 需要拷贝的文件: \\192.168.0.114\Share\gateway.zip  存放路径: F:\test\gateway.zip
echo F | xcopy \\192.168.0.114\Share\gateway.zip F:\test\gateway.zip /Y /r /k /f /e /s /c /h
net use * /del /y