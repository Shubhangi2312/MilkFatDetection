Public Class frmUsers
    Dim objconn As New ADODB.Connection
    Dim objrs As New ADODB.Recordset
    Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillGrid()
    End Sub

    Private Sub fillGrid()
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tbluser", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If Not objrs.EOF Then
            Dim count As Integer
            count = 0
            While Not objrs.EOF
                count = count + 1
                objrs.MoveNext()
            End While
            dgv.RowCount = count
            objrs.MoveFirst()
            Dim i As Integer
            i = 0
            While Not objrs.EOF
                dgv.Item(0, i).Value = objrs("Uid").Value
                dgv.Item(1, i).Value = objrs("Pwd").Value
                dgv.Item(2, i).Value = objrs("uname").Value
                dgv.Item(3, i).Value = objrs("ph").Value

                objrs.MoveNext()
                i = i + 1
            End While
        End If
        objconn.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim query As String

        If txtPwd.Text <> txtCPwd.Text Then
            MsgBox("Password and confirm password do not match",MsgBoxStyle.Exclamation)
            Return
        End If

        objconn.Open(Constants.dsnName)

        query = "INSERT INTO `tbluser` (`uid`, `pwd`, `uname`, `ph`) VALUES ('" & txtUid.Text & "', '" & txtPwd.Text & "', '" & txtuname.Text & "', '" & txtPh.Text & "');"

        objconn.Execute(query)

        objconn.Close()
        MsgBox("User details saved successfully",MsgBoxStyle.Information)

        fillGrid()
    End Sub

    Private Sub dgv_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick

        If e.RowIndex < 0 Then
            Return
        End If


        If MsgBox("Do you want to delete this record??", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
            objconn.Open(Constants.dsnName)
            objconn.Execute("DELETE FROM tbluser WHERE uid = '" & dgv.Item(0, e.RowIndex).Value & "'")
            objconn.Close()
            MsgBox("User deleted successfully", MsgBoxStyle.Information)
            fillGrid()
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    
    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
End Class