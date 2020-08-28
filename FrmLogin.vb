Public Class FrmLogin
    Dim objconn As New ADODB.Connection
    Dim objrs As New ADODB.Recordset

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim u, v As String
        u = txtUID.Text
        v = txtPWD.Text
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tbluser where uid='" & u & "' AND pwd='" & v & "'", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If objrs.EOF Then
            MsgBox("USER ID or Password invalid", MsgBoxStyle.Exclamation)
        Else
            gbl_uid = u
            gbl_pwd = v
            FrmMain.Show()
        End If
        objrs.Close()
        objconn.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtUID.Text = ""
        txtPWD.Text = ""
    End Sub

    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtPWD.Text = ""
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
