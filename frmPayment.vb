Public Class frmPayment
    Dim objconn As New ADODB.Connection
    Dim objrs As New ADODB.Recordset
    
    Private Sub fillGrid()
        objconn.Open(Constants.dsnName)
        objrs.Open("select * from tblaccount", objconn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
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
                dgv.Item(0, i).Value = objrs("tid").Value
                dgv.Item(1, i).Value = objrs("fid").Value
                dgv.Item(2, i).Value = objrs("amt").Value
                dgv.Item(3, i).Value = objrs("tdate").Value
                objrs.MoveNext()
                i = i + 1
            End While
        End If
        objrs.Close()
        objconn.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim query As String
        objconn.Open(Constants.dsnName)
        '.......Make entry into Account Table(tblaccount)
        query = "INSERT INTO tblaccount VALUES (null,'" & txtFarmerID.Text & "', now(), '" & txtamt.Text & "', '" & txtComment.Text & "')"
        objconn.Execute(query)

        '......Update Balance in farmer table
        query = "UPDATE tblfarmer set balamt=balamt-" & Val(txtamt.Text) & " where fid='" & txtFarmerID.Text & "'"
        objconn.Execute(query)

        objconn.Close()
        MsgBox("Payment details saved successfully", MsgBoxStyle.Information)

        fillGrid()

    End Sub

    Private Sub dgv_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        If e.RowIndex < 0 Then
            Return
        End If
        If MsgBox("Do you want to delete this record??", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
            Dim fid As String
            Dim amt As Decimal
            fid = dgv.Item(1, e.RowIndex).Value
            amt = dgv.Item(2, e.RowIndex).Value

            objconn.Open(Constants.dsnName)
            objconn.Execute("DELETE FROM tblaccount WHERE tid = '" & dgv.Item(0, e.RowIndex).Value & "'")

            objconn.Execute("UPDATE tblfarmer set balamt = balamt + " & amt & " where fid='" & fid & "'")

            objconn.Close()
            MsgBox("Payment transaction deleted successfully", MsgBoxStyle.Information)
            fillGrid()
        End If

    End Sub

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtdate.Text = Now
        fillGrid()
    End Sub
End Class