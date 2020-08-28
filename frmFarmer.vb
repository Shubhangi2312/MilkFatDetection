Public Class frmFarmer
    Dim objconn As New ADODB.Connection
    Dim objrs As New ADODB.Recordset
    Private Sub frmUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fillGrid()
    End Sub

    Private Sub fillGrid()
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tblfarmer", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
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
                dgv.Item(0, i).Value = objrs("fid").Value
                dgv.Item(1, i).Value = objrs("fname").Value
                dgv.Item(2, i).Value = objrs("ph").Value
                dgv.Item(3, i).Value = objrs("balamt").Value
                objrs.MoveNext()
                i = i + 1
            End While
        End If
        objconn.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim query As String
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tblfarmer where fid='" & txtFarmerID.Text & "'", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If objrs.EOF Then
            query = "INSERT INTO `tblfarmer` VALUES ('" & txtFarmerID.Text & "', '" & txtFName.Text & "','" & txtPh.Text & "', '" & txtaddress.Text & "',0)"
            objconn.Execute(query)
        Else
            MsgBox("Farmer with the same Farmer ID. already exists in the database", MsgBoxStyle.Exclamation)
        End If
        objrs.Close()
        objconn.Close()
        MsgBox("New Farmer added successfully", MsgBoxStyle.Information)
        fillGrid()
    End Sub

    Private Sub dgv_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        If e.RowIndex < 0 Then
            Return
        End If
        If MsgBox("Do you want to delete this record??", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
            objconn.Open(Constants.dsnName)
            objconn.Execute("DELETE FROM tblfarmer WHERE fid = '" & dgv.Item(0, e.RowIndex).Value & "'")
            objconn.Close()
            MsgBox("Farmer deleted successfully", MsgBoxStyle.Information)
            fillGrid()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgv_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
End Class