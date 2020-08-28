Public Class frmChangePwd
    Dim objconn As New ADODB.Connection
    Dim objrs As New ADODB.Recordset

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim u, v As String
        u = txtUid.Text
        v = txtOldPwd.Text
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tbluser where uid='" & u & "' AND pwd='" & v & "'", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If objrs.EOF Then
            MsgBox("USER ID or old Password invalid", MsgBoxStyle.Exclamation)
        Else

            If txtPwd.Text <> txtCPwd.Text Then
                MsgBox("New Password and confirm Password is not same.", MsgBoxStyle.Exclamation)
            Else
                objconn.Execute("UPDATE tbluser set pwd = '" & txtPwd.Text & "' WHERE uid = '" & txtUid.Text & "'")
                MsgBox("Password changed successfully!")
            End If

        End If
        objrs.Close()
        objconn.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmChangePwd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtUid.Text = gbl_uid
        txtUid.Enabled = False
    End Sub
End Class