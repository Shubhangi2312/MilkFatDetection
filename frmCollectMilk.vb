Public Class frmCollectMilk
    Dim objConn As New ADODB.Connection
    Dim objRs As New ADODB.Recordset

    Dim RxString As String
    Public Delegate Sub DisplayTextDelegate()

    Private Sub frmCollectMilk_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        objConn.Close()
        SerialPort1.Close()
    End Sub

    Private Sub frmCollectMilk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtdate.Text = Now
        objConn.Open(Constants.dsnName)
        'SerialPort1.Open()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtamt.Text = "" Or txtdate.Text = "" Or txtFarmerID.Text = "" Or txtmfat.Text = "" Or txtmwt.Text = "" Or txtrate.Text = "" Then
            MsgBox("Please fill all the fields before saving the details", MsgBoxStyle.Exclamation)
        Else
            Dim fid As String = txtFarmerID.Text
            Dim tdate As String = txtdate.Text
            Dim amt As String = txtamt.Text
            Dim wt As Decimal = Val(txtmwt.Text)
            Dim fat As Decimal = Val(txtmfat.Text)
            Dim rate As Decimal = Val(txtrate.Text)
        End If
    End Sub

    Private Sub btnFat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFat.Click
        '............Get fat content from the arduino kit
        SerialPort1.Write("x")
        Dim fat As Decimal = Val(txtmfat.Text)
        'Dim totalamt As Decimal

    End Sub


    Private Sub SerialPort1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        RxString = SerialPort1.ReadLine()
        Me.Invoke(New DisplayTextDelegate(AddressOf DisplayText))
    End Sub

    Private Sub DisplayText()
        txtmfat.Text = RxString
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub txtmfat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtmfat.TextChanged

    End Sub

    Private Sub txtamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamt.TextChanged

    End Sub
End Class