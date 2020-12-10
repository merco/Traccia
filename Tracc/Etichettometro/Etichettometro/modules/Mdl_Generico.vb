Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary

Public Module Mdl_Generico

  Private Const MaxBuffer As Integer = 4095
  ' API functions

  Private Declare Ansi Function GetPrivateProfileString _
    Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
    (ByVal lpApplicationName As String,
        ByVal lpKeyName As String, ByVal lpDefault As String,
        ByVal lpReturnedString As System.Text.StringBuilder,
        ByVal nSize As Integer, ByVal lpFileName As String) _
    As Integer

  Private Declare Ansi Function GetPrivateProfileString _
    Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
    (ByVal lpApplicationName As String,
        ByVal lpKeyName As Integer, ByVal lpDefault As String,
        ByVal lpReturnedString() As Byte,
        ByVal nSize As Integer, ByVal lpFileName As String) _
    As Integer

  Private Declare Ansi Function GetPrivateProfileString _
    Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
    (ByVal lpApplicationName As Integer,
        ByVal lpKeyName As Integer, ByVal lpDefault As String,
        ByVal lpReturnedString() As Byte,
        ByVal nSize As Integer, ByVal lpFileName As String)
  Function DeepClone(Of T)(ByRef orig As T) As T

    ' Don't serialize a null object, simply return the default for that object
    If (Object.ReferenceEquals(orig, Nothing)) Then Return Nothing

    Dim formatter As New BinaryFormatter()
    Dim stream As New MemoryStream()

    formatter.Serialize(stream, orig)
    stream.Seek(0, SeekOrigin.Begin)

    Return CType(formatter.Deserialize(stream), T)

  End Function

  Public Function drToString(dr As DataRow) As String
    Dim vo As String = ""
    For Each dc As DataColumn In dr.Table.Columns
      vo = vo & " | " & Nz(dr(dc.ColumnName), "").ToString
    Next
    Return vo
  End Function
  Public Sub StringToClipBoard(s As String)
    My.Computer.Clipboard.Clear()
    My.Computer.Clipboard.SetText(s, TextDataFormat.Text)
  End Sub
  Public Sub DataTableToClipBoard(dt As DataTable)
    My.Computer.Clipboard.Clear()

    If Not dtEOF(dt) Then
      Dim Sb As New System.Text.StringBuilder
      For Each dr As DataRow In dt.Rows
        For Each dc As DataColumn In dt.Columns
          Sb.Append(Nz(dr(dc.ColumnName), "").ToString & vbTab)
        Next
        Sb.AppendLine()
      Next
      My.Computer.Clipboard.SetText(Sb.ToString, TextDataFormat.Text)
    End If

  End Sub
  Public Sub SetNothing(Of T)(ByRef obj As T)
    ' Dispose of the object if possible
    Try
      If ((obj IsNot Nothing) AndAlso (TypeOf obj Is IDisposable)) Then
        DirectCast(obj, IDisposable).Dispose()
      End If

      ' Decrease the reference counter, if it's a COM object
      GC.Collect()
      GC.WaitForPendingFinalizers()
      GC.Collect()
      GC.WaitForPendingFinalizers()

      GC.Collect()
      GC.WaitForPendingFinalizers()

      If (Marshal.IsComObject(obj)) Then
        Marshal.ReleaseComObject(obj)
        Marshal.FinalReleaseComObject(obj)
      End If

      obj = Nothing
    Catch
    End Try

  End Sub

  Public Sub ScriviFile(ByVal Dove As String, ByVal Cosa As String)
    Try
      IO.File.AppendAllText(Dove, Cosa)
    Catch ex As System.Exception
    End Try
  End Sub

  Public Sub CopiaFile(ByVal CheFile As String, ByVal A As String)
    Try
      System.IO.File.Copy(CheFile, A, True)
    Catch ex As System.Exception
    End Try
  End Sub

  Public Sub RenameFile(ByVal da As String, ByVal A As String)
    Try
      IO.File.Move(da, A)
    Catch ex As System.Exception
    End Try
  End Sub
  Public Sub CreaCartella(path As String)
    Try
      IO.Directory.CreateDirectory(path)
    Catch ex As Exception

    End Try
  End Sub
  Public Sub CancellaFile(ByVal CheFile As String)
    Try
      System.IO.File.Delete(CheFile)
    Catch ex As System.Exception
    End Try
  End Sub

  Public Function GetIesimoCampo(ByVal C() As String, ByVal Idx As Integer) As String
    Dim R As String = ""

    If (Idx < C.Length) Then
      R = C(Idx)
    Else
      R = ""
    End If

    Return R
  End Function

  Public Sub ApriFileProg(ByVal ChePath As String)
    'Dim P As System.Diagnostics.Process


    If (ChePath <> "") Then
      Try
        System.Diagnostics.Process.Start(ChePath, "")
      Catch
      End Try
    End If


  End Sub

  Public Function TestScritturaCartellaOK(ByVal chePath As String) As Boolean
    Dim Ok As Boolean = False

    Try

      If (Not IO.Directory.Exists(chePath)) Then
        IO.Directory.CreateDirectory(chePath)
      End If

      IO.File.WriteAllText(IO.Path.Combine(chePath, "_TEST_SCRITTURA.TXT"), "PROVA")

      If (IO.File.Exists(IO.Path.Combine(chePath, "_TEST_SCRITTURA.TXT"))) Then
        If (IO.File.ReadAllText(IO.Path.Combine(chePath, "_TEST_SCRITTURA.TXT")) = "PROVA") Then
          Ok = True
        End If
      End If

    Catch ex As System.Exception
    Finally
      CancellaFile(IO.Path.Combine(chePath, "_TEST_SCRITTURA.TXT"))
    End Try

    Return Ok
  End Function

  Public Function Nz(ByVal Cosa As Object, ByVal Def As Object) As Object
    If (System.Convert.IsDBNull(Cosa)) Then
      Nz = Def
    Else
      Nz = Cosa
    End If
  End Function
  Public Function RecFromString(Sin) As System.Drawing.Rectangle

    Dim R() As String = Split(Sin, ",")
    Dim r1 As New System.Drawing.Rectangle(R(0), R(1), R(2), R(3))

    Return r1

  End Function
  Public Function RectToStr(r As System.Drawing.Rectangle) As String
    Return Math.Round(r.X, 0) & "," & Math.Round(r.Y, 0) & "," & Math.Round(r.Width, 0) & "," & Math.Round(r.Height, 0)
  End Function
  Public Function LeggiFile(ByVal Quale As String) As String
    Dim C As String = ""
    Try
      C = IO.File.ReadAllText(Quale)
    Catch ex As Exception
      C = ""
    End Try
    LeggiFile = C
  End Function
  Public Function LeggiINI(ByVal Sezione As String, ByVal Chiave As String, ByVal FINI As String) As String

    If (Not System.IO.File.Exists(FINI)) Then
      LeggiINI = ""
      Exit Function
    End If

    Dim sr As StreamReader = New StreamReader(FINI, System.Text.Encoding.Default)
    Dim AllINI As String = sr.ReadToEnd()
    Dim Pu As Integer

    sr.Close()
    'Dim AllINI As String = System.IO.File.OpenText(FINI).ReadToEnd

    Dim splitedLine() As String
    Dim sCorr As String
    Dim sC As String
    Dim sV As String
    Dim I As Integer

    sCorr = ""
    sV = ""
    Sezione = Sezione.ToUpper
    Chiave = Chiave.ToUpper
    'AllINI = AllINI.ToUpper
    splitedLine = Split(AllINI, vbCrLf)
    'splitedLine = AllINI.Split(vbCrLf)

    For I = 0 To splitedLine.Length - 1
      If (splitedLine(I).Trim.StartsWith("[") = True) Then
        If (splitedLine(I).Trim Like "[[]*[]]") Then
          sCorr = UCase(splitedLine(I).Trim)
        End If
      Else
        'riga normale
        sC = ""
        sV = ""
        If (sCorr = "[" & UCase(Sezione) & "]") Then
          Pu = InStr(1, splitedLine(I), "=")

          If (Pu <> 0) Then
            sC = Left(splitedLine(I), Pu - 1)
            sV = Mid(splitedLine(I), Pu + 1)
          End If

          If (UCase(sC) = Chiave) Then
            Exit For
          End If
        End If
      End If
      sC = ""
      sV = ""
    Next
    LeggiINI = sV

  End Function

  Public Sub Logga(ByVal cosa As String)

    cosa = Now.ToString & " " & My.User.Name & " " & cosa
    If IO.File.Exists(Parametri.FileLog) Then
      Dim fileSize As Long
      fileSize = My.Computer.FileSystem.GetFileInfo(Parametri.FileLog).Length
      If fileSize > 5880000 Then
        CopiaFile(Parametri.FileLog, Parametri.FileLog & ".bak")
        CancellaFile(Parametri.FileLog)
      End If
    End If
    Try
      IO.File.AppendAllText(Parametri.FileLog, cosa & vbCrLf)
    Catch ex As Exception

    End Try


  End Sub
  Public Function DT_Filtra(dtIn As DataTable, swhere As String) As DataTable
    Dim dv As DataView = dtIn.DefaultView
    dv.RowFilter = swhere
    Return dv.ToTable
  End Function

  Public Function dtEOF(dtin As DataTable) As Boolean
    If IsNothing(dtin) OrElse dtin.Rows.Count <= 0 Then
      Return True
    Else
      Return False
    End If
  End Function
  Public Function cloneDT(dtIn As DataTable) As DataTable
    Dim Dt1 As DataTable = Nothing

    If Not IsNothing(dtIn) Then

      Dt1 = dtIn.Clone

      For Each dr As DataRow In dtIn.Rows
        Dt1.ImportRow(dr)
      Next
    End If
    Return Dt1
  End Function

  Public Function GetChiavi(ByVal CheFile As String, ByVal CheSezione As String) As Collection
    ' Returns a string from your INI file
    Dim intCharCount As Integer
    Dim i As Integer
    Dim objResult(MaxBuffer) As Byte
    Dim Sb As System.Text.StringBuilder

    intCharCount = GetPrivateProfileString(CheSezione, 0, "", objResult, objResult.Length, CheFile)
    GetChiavi = New Collection

    If (intCharCount > 0) Then
      Sb = New System.Text.StringBuilder()
      For i = 0 To intCharCount - 1
        If (objResult(i) <> 0) Then
          Sb.Append(ChrW(objResult(i)))
        Else
          If (Sb.Length > 0) Then
            GetChiavi.Add(Sb.ToString(), Sb.ToString())
            Sb = New System.Text.StringBuilder()
          End If
        End If
      Next
    End If

    Sb = Nothing
  End Function
  Public Sub writeReg(chiave As String, Valore As String)
    Dim keyName As String = "HKEY_CURRENT_USER\" & Parametri.AppName
    My.Computer.Registry.SetValue(keyName, chiave, Valore, Microsoft.Win32.RegistryValueKind.String)
  End Sub
  Public Function readReg(chiave As String) As String
    Dim keyName As String = "HKEY_CURRENT_USER\" & Parametri.AppName
    Return CStr(My.Computer.Registry.GetValue(keyName, chiave, ""))
  End Function
  Public Function getCellValue(ws As OfficeOpenXml.ExcelWorksheet, ir As Integer, ic As Integer) As String
    If IsNothing(ws.Cells(ir, ic).Value) Then
      Return ""
    Else

      If TypeOf ws.Cells(ir, ic).Value Is OfficeOpenXml.ExcelErrorValue Then
        Return ""
      Else
        Dim appo As String = ws.Cells(ir, ic).Value

        If appo.Contains(";") Then
          appo = Replace(appo, ";", " ")
        End If
        If appo.Contains(vbCrLf) Then
          appo = Replace(appo.ToString, vbCrLf, " ")
        End If
        If appo.Contains(vbCr) Then
          appo = Replace(appo.ToString, vbCr, " ")
        End If
        If appo.Contains(vbLf) Then
          appo = Replace(appo, vbLf, " ")
        End If
        appo = Trim(appo)

        Return appo
      End If


    End If
  End Function
  Public Function ExcelToDataTable(FileIN As String) As DataTable
    Dim Dt As New DataTable

    Dim iniziaDallaRiga As Integer = Val(LeggiINI("Generale", "iniziaDallaRiga", Parametri.FileINI))
    If iniziaDallaRiga = 0 Then iniziaDallaRiga = 1



    Using package As OfficeOpenXml.ExcelPackage = New OfficeOpenXml.ExcelPackage(New IO.FileInfo(FileIN))
      Dim wsc As OfficeOpenXml.ExcelWorksheet = package.Workbook.Worksheets(1)
      Dim MaxRows As Long = wsc.Dimension.End.Row
      Dim MaxCols As Integer = wsc.Dimension.End.Column

      For ic As Integer = 1 To MaxCols
        Dt.Columns.Add("C_" & ic - 1)
      Next

      For Ir As Long = iniziaDallaRiga To MaxRows
        Dim dr As DataRow = Dt.NewRow

        For ic As Integer = 1 To MaxCols
          dr(ic - 1) = getCellValue(wsc, Ir, ic)


        Next
        Dt.Rows.Add(dr)
      Next

    End Using



    Return Dt

  End Function
  Public Function DataTableToExcel(FileOut As String, dt As DataTable) As String


    If IsNothing(dt) OrElse dt.Rows.Count <= 0 Then
      Return "Nessun dato da estrarre"
    End If
    CancellaFile(FileOut)

    If FileInUse(FileOut) Then
      Return "File " & FileOut & " in uso"
    End If
    Dim newFile = New IO.FileInfo(FileOut)
    Using package As OfficeOpenXml.ExcelPackage = New OfficeOpenXml.ExcelPackage(newFile)
      Dim worksheet As OfficeOpenXml.ExcelWorksheet = package.Workbook.Worksheets.Add(dt.TableName)
      Dim CC As Integer = 0
      For Each dc As DataColumn In dt.Columns
        CC = CC + 1
        worksheet.Cells(1, CC).Value = dc.ColumnName
      Next


      Dim Cr As Integer = 1
      For Each dr As DataRow In dt.Rows

        Cr = Cr + 1
        CC = 0
        For Each dc As DataColumn In dt.Columns
          CC = CC + 1
          worksheet.Cells(Cr, CC).Value = dr(dc.ColumnName)
        Next

      Next
      worksheet.Cells(worksheet.Dimension.Address).AutoFitColumns()
      package.SaveAs(newFile)
    End Using
    Return ""
  End Function
  Public Function FileInUse(ByVal sFile As String) As Boolean
    Dim thisFileInUse As Boolean = False
    If System.IO.File.Exists(sFile) Then
      Try
        Using f As New IO.FileStream(sFile, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
          ' thisFileInUse = False
        End Using
      Catch
        thisFileInUse = True
      End Try
    End If
    Return thisFileInUse
  End Function
End Module
