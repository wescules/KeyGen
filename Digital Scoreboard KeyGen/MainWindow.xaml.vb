Class MainWindow 

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtSerialKey.Text = ""
        txtLicenseKey.Text = ""
        'txtSerialKey.Focus()
        lstsidelineproduct.Focus()
        'txtSerialKey.Text = Application.GenerateSerialKey
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As RoutedEventArgs) Handles btnGenerate.Click
        If txtSerialKey.Text.Trim.Length = 35 Then
            If lstsidelineproduct.SelectedItem IsNot Nothing Then
                Dim scbitem As ComboBoxItem = lstsidelineproduct.SelectedItem
                If scbitem.Tag.ToString.Trim = "1" Then
                    txtLicenseKey.Text = Application.GenerateLicenseKey(txtSerialKey.Text.Trim.ToUpper, scbitem.Tag.ToString.Trim)
                ElseIf scbitem.Tag.ToString.Trim = "2"
                    txtLicenseKey.Text = Application.GenerateLicenseKey(txtSerialKey.Text.Trim.ToUpper, scbitem.Tag.ToString.Trim)
                ElseIf scbitem.Tag.ToString.Trim = "3"
                    txtLicenseKey.Text = Application.GenerateLicenseKey(txtSerialKey.Text.Trim.ToUpper, scbitem.Tag.ToString.Trim)
                ElseIf scbitem.Tag.ToString.Trim = "4"
                    txtLicenseKey.Text = Application.GenerateLicenseKey(txtSerialKey.Text.Trim.ToUpper, scbitem.Tag.ToString.Trim)
                End If
            Else
                MessageBox.Show("Please select a Product")
            End If
        Else
            MessageBox.Show("Please enter a valid 35 digit alpha numeric Serial Key and try again" + vbNewLine + vbNewLine + "Example :" + vbNewLine + "13A3490A-4236D04A-DCA82EA8-799DB16C")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        End
    End Sub
End Class
