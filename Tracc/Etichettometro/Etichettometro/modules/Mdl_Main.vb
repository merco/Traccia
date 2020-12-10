Module Mdl_Main

    Public Structure TParametri

        Dim FileINI As String
        Dim versione As String
        Dim AppName As String
        Dim FileLog As String
        Dim AppPath As String
        Dim CommandLine As String
        Dim UserName As String
        Dim ComputerName As String

  End Structure

    Public Parametri As TParametri
 
    Public Sub deTokenizer(strIn As String, Delim As String, ByRef list As System.Collections.Generic.List(Of String))
        list = New System.Collections.Generic.List(Of String)
        If strIn <> "" Then
            Dim C() As String = Split(strIn, Delim)

            For Each theC As String In C
                list.Add(theC)
            Next
        End If
    End Sub
  Public Function deTokenizer(strIn As String, Delim As String) As System.Collections.Generic.List(Of String)
    Dim L As New System.Collections.Generic.List(Of String)
    If strIn <> "" Then
      Dim C() As String = Split(strIn, Delim)


      For Each theC As String In C
        L.Add(theC)
      Next
    End If
    Return L
  End Function
  Public Function EditScript(testo As String) As String
    Dim tmpFile As String = IO.Path.GetTempFileName
    CancellaFile(tmpFile)
    tmpFile = tmpFile & ".TXT"

    ScriviFile(tmpFile, testo)
    Dim pf As String = IO.Path.Combine(Parametri.AppPath, "MD\Notepad2.exe")

    Dim p As Process = Process.Start(pf, tmpFile)

    p.WaitForExit()

    Dim appo As String = LeggiFile(tmpFile)
    CancellaFile(tmpFile)

    Return appo

  End Function

  Public Sub CaricaParametri()

    With Parametri
      .AppName = "Etichettometro "
      .versione = " v 1.0 "

      .AppPath = My.Application.Info.DirectoryPath & "\"



      .FileLog = System.IO.Path.Combine(.AppPath, "log.txt")
    End With
  End Sub

End Module