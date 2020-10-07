Imports MySql.Data.MySqlClient

Public Class dash
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If User.Identity.IsAuthenticated = False Then
            Response.Redirect("login.aspx")
        Else
            form1.Visible = True

            Try
                Dim sCon As MySqlConnection = New MySqlConnection($"Server={ConfigurationManager.AppSettings("appAddress")};Database={ConfigurationManager.AppSettings("appDB")};Uid={ConfigurationManager.AppSettings("appUser")};Pwd={ConfigurationManager.AppSettings("appPW")}")
                sCon.Open()
                Dim sqlCom As MySqlCommand = New MySqlCommand()
                sqlCom.Connection = sCon
                sqlCom.CommandText = "SELECT * FROM url;"
                sqlCom.CommandType = CommandType.Text
                Dim reader As MySqlDataReader = sqlCom.ExecuteReader()

                While reader.Read()
                    Dim row As TableRow = New TableRow()

                    Dim cell1 As TableCell = New TableCell()
                    cell1.Text = reader("_id")
                    row.Cells.Add(cell1)

                    Dim cell2 As TableCell = New TableCell()
                    cell2.Text = reader("url")
                    row.Cells.Add(cell2)

                    Dim cell3 As TableCell = New TableCell()
                    cell3.Text = reader("shorted")
                    row.Cells.Add(cell3)

                    Table1.Rows.Add(row)
                End While

                sCon.Close()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick
        If User.Identity.IsAuthenticated = True Then
            FormsAuthentication.SignOut()
            Response.Redirect("login.aspx")
        End If
    End Sub

    Private Sub btnDelete_ServerClick(sender As Object, e As EventArgs) Handles btnDelete.ServerClick
        If User.Identity.IsAuthenticated = True Then
            If Not String.IsNullOrWhiteSpace(txtID.Value) And IsNumeric(txtID.Value) Then
                Try
                    Dim sCon As MySqlConnection = New MySqlConnection($"Server={ConfigurationManager.AppSettings("appAddress")};Database={ConfigurationManager.AppSettings("appDB")};Uid={ConfigurationManager.AppSettings("appUser")};Pwd={ConfigurationManager.AppSettings("appPW")}")
                    sCon.Open()
                    Dim sqlCom As MySqlCommand = New MySqlCommand()
                    sqlCom.Connection = sCon
                    sqlCom.CommandText = $"DELETE FROM url WHERE _id=@txt;"
                    sqlCom.Parameters.Add(New MySqlParameter("@txt", txtID.Value))
                    sqlCom.CommandType = CommandType.Text
                    sqlCom.ExecuteNonQuery()
                    sCon.Close()
                Catch ex As Exception

                End Try

                Response.Redirect("dash.aspx")
            End If
        End If
    End Sub
End Class