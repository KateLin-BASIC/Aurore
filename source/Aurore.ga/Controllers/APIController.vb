Imports System.Net
Imports System.Web.Mvc
Imports MySql.Data.MySqlClient

Namespace Controllers
    Public Class APIController
        Inherits Controller

        Function Index() As ActionResult
            If HttpContext.Request.HttpMethod = "POST" And Not String.IsNullOrWhiteSpace(Request.Form.Get("url")) Then
                Dim key = String.Concat("Cooltime", "-", Request.UserHostAddress)
                Dim allowExecute = False

                If HttpRuntime.Cache(key) Is Nothing Then
                    HttpRuntime.Cache.Add(key, True, Nothing, DateTime.Now.AddSeconds(5), HttpRuntime.Cache.NoSlidingExpiration, CacheItemPriority.Low, Nothing)
                    allowExecute = True
                End If

                If Not allowExecute Then
                    Response.TrySkipIisCustomErrors = True
                    Response.ContentType = "text/plain"
                    Response.Write("Please wait for 5 second.")
                    Response.StatusCode = 409
                Else
                    Dim filename As String = RandomString(5)
                    Dim uriResult As Uri = Nothing
                    Dim result As Boolean = Uri.TryCreate(Request.Form("url"), UriKind.Absolute, uriResult) AndAlso (uriResult.Scheme = Uri.UriSchemeHttp OrElse uriResult.Scheme = Uri.UriSchemeHttps)

                    If result = True Then
                        UrlWrite(Request.Form("url"), "/s/" & filename)
                        Response.Write($"https://aurore.ga/s/{filename}")
                    Else
                        UrlWrite("http://" & Request.Form("url"), "/s/" & filename)
                        Response.Write($"https://aurore.ga/s/{filename}")
                    End If
                End If
            Else
                Response.TrySkipIisCustomErrors = True
                Response.ContentType = "text/plain"
                Response.Write("Bad Request")
                Response.StatusCode = 400
            End If
        End Function

        Private Function UrlWrite(orgurl As String, shorted As String)
            Try
                Dim sCon As MySqlConnection = New MySqlConnection($"Server={ConfigurationManager.AppSettings("appAddress")};Database={ConfigurationManager.AppSettings("appDB")};Uid={ConfigurationManager.AppSettings("appUser")};Pwd={ConfigurationManager.AppSettings("appPW")}")
                sCon.Open()
                Dim sqlCom As MySqlCommand = New MySqlCommand()

                sqlCom.Connection = sCon
                sqlCom.CommandText = "INSERT INTO url (url, shorted) VALUES(@url, @short);"
                sqlCom.CommandType = CommandType.Text

                sqlCom.Parameters.AddWithValue("@url", orgurl)
                sqlCom.Parameters.AddWithValue("@short", shorted)

                sqlCom.ExecuteNonQuery()

                sCon.Close()
            Catch ex As Exception
            End Try
            Return True
        End Function

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
    End Class
End Namespace