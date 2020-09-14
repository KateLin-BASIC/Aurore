Imports System.IO
Imports MySql.Data.MySqlClient

Public Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientScript.GetPostBackEventReference(Me, "")

        If Not IsPostBack Then
            Session("CheckRefresh") = Server.UrlDecode(System.DateTime.Now.ToString())
        End If
    End Sub

    Private Sub index_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ViewState("CheckRefresh") = Session("CheckRefresh")
    End Sub

    Private Function RandomString(ByRef Length As String) As String
        Dim str As String = Nothing
        Dim rnd As New Random
        For i As Integer = 0 To Length
            Dim chrInt As Integer = 0
            Do
                chrInt = rnd.Next(30, 122)
                If (chrInt >= 48 And chrInt <= 57) Or (chrInt >= 65 And chrInt <= 90) Or (chrInt >= 97 And chrInt <= 122) Then
                    Exit Do
                End If
            Loop
            str &= Chr(chrInt)
        Next
        Return str
    End Function

    Private Sub btnGen_ServerClick(sender As Object, e As EventArgs) Handles btnGen.ServerClick
        If Session("CheckRefresh").ToString() = ViewState("CheckRefresh").ToString() Then
            If Not String.IsNullOrWhiteSpace(txtUrl.Value) Then
                Dim filename As String = RandomString(5)
                Dim uriResult As Uri
                Dim result As Boolean = Uri.TryCreate(txtUrl.Value, UriKind.Absolute, uriResult) AndAlso (uriResult.Scheme = Uri.UriSchemeHttp OrElse uriResult.Scheme = Uri.UriSchemeHttps)

                If result = True Then
                    UrlWrite(txtUrl.Value, "/s/" & filename)
                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "", $"run('https://aurore.ga/s/{filename}')", True)
                    txtUrl.Value = ""
                Else
                    UrlWrite("http://" & txtUrl.Value, "/s/" & filename)
                    ScriptManager.RegisterStartupScript(Page, Page.[GetType](), "", $"run('https://aurore.ga/s/{filename}')", True)
                    txtUrl.Value = ""
                End If
            End If

            Session("CheckRefresh") = Server.UrlDecode(System.DateTime.Now.ToString())
        End If
    End Sub

    Private Function UrlWrite(orgurl As String, shorted As String)
        Try
            Dim sCon As MySqlConnection = New MySqlConnection("Server=localhost;Database=DATABASE_NAME_HERE;Uid=root;Pwd=PASSWORD_HERE")
            sCon.Open()
            Dim sqlCom As MySqlCommand = New MySqlCommand()

            sqlCom.Connection = sCon
            sqlCom.CommandText = "INSERT INTO url (_url, shorted) VALUES(@url_org, @shorted_url);"
            sqlCom.CommandType = CommandType.Text

            sqlCom.Parameters.AddWithValue("@url_org", orgurl)
            sqlCom.Parameters.AddWithValue("@shorted_url", shorted)

            sqlCom.ExecuteNonQuery()

            sCon.Close()
        Catch ex As Exception
        End Try
    End Function
End Class