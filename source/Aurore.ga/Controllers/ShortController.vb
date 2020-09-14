﻿Imports System.Web.Mvc
Imports MySql.Data.MySqlClient

Namespace s
    Public Class ShortController
        Inherits Controller

        ' GET: Short
        Function Index()
        Dim sCon As MySqlConnection = New MySqlConnection("Server=localhost;Database=DATABASE_NAME_HERE;Uid=root;Pwd=PASSWORD_HERE;")
            sCon.Open()
            Dim sqlCom As MySqlCommand = New MySqlCommand()
            sqlCom.Connection = sCon
            sqlCom.CommandText = "SELECT * FROM url WHERE shorted=@url_name"
            sqlCom.CommandType = CommandType.Text
            sqlCom.Parameters.Add(New MySqlParameter("@url_name", Request.Url.PathAndQuery))
            Dim reader As MySqlDataReader = sqlCom.ExecuteReader()

            While reader.Read()
                Return Redirect(reader("_url"))
            End While

            Return Redirect("/")

            sCon.Close()
        End Function
    End Class
End Namespace
