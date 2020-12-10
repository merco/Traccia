Imports iTextSharp.text.pdf

Public Class frmMain
  Dim dt As DataTable = Nothing
  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    Dim ofd As New OpenFileDialog
    ofd.Filter = "File excel|*.xlsx"


    If ofd.ShowDialog = DialogResult.OK Then
      If FileInUse(ofd.FileName) Then
        MsgBox("Chiudi il file excel", MsgBoxStyle.Exclamation)
      Else
        dt = ExcelToDataTable(ofd.FileName)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.ClearSelection()

        Me.cboR1.Items.Clear() : Me.cboR2.Items.Clear() : Me.cboR3.Items.Clear()

        For Each dc As DataColumn In dt.Columns
          Me.cboR1.Items.Add(dc.ColumnName)
          Me.cboR2.Items.Add(dc.ColumnName)
          Me.cboR3.Items.Add(dc.ColumnName)
          Me.cboColOrd.Items.Add(dc.ColumnName)
        Next
      End If

    End If
    ofd.Dispose()

  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    Dim ofd As New OpenFileDialog
    ofd.Filter = "File jpg|*.jpg"


    If ofd.ShowDialog = DialogResult.OK Then
      Me.PictureBox1.Image = Image.FromFile(ofd.FileName)
    End If


    ofd.Dispose()
  End Sub


  Private Sub pdf_SetImmagineBottone(_Fields As AcroFields, ByVal NomeBottone As String, ByVal PathCompleto As String, img As iTextSharp.text.Image)
    Dim bt As PushbuttonField = Nothing

    If Not IsNothing(img) Then
      If _Fields.Fields.ContainsKey(NomeBottone) Then
        bt = _Fields.GetNewPushbuttonFromField(NomeBottone)
        bt.Layout = PushbuttonField.LAYOUT_ICON_ONLY
        bt.ProportionalIcon = True
        bt.Image = img
        _Fields.ReplacePushbuttonField(NomeBottone, bt.Field)
      End If
    Else
      If System.IO.File.Exists(PathCompleto) Then
        If _Fields.Fields.ContainsKey(NomeBottone) Then
          bt = _Fields.GetNewPushbuttonFromField(NomeBottone)
          bt.Layout = PushbuttonField.LAYOUT_ICON_ONLY
          bt.ProportionalIcon = True
          bt.Image = iTextSharp.text.Image.GetInstance(PathCompleto)
          _Fields.ReplacePushbuttonField(NomeBottone, bt.Field)
        End If
      End If

    End If



  End Sub
  Private Sub pdf_ImpostaCampo(_Fields As AcroFields, ByVal NomeCampo As String, ByVal Valore As String)
    If Not IsNothing(_Fields) Then
      If _Fields.Fields.ContainsKey(NomeCampo) Then
        _Fields.SetField(NomeCampo, Valore)
      End If
    End If
  End Sub
  Private Sub GeneraEtichette()
    Dim NEti As Integer = Val(txtNEti.Text)
    If dtEOF(dt) Then
      MsgBox("Nessun dato da stampare", vbCritical)
      Exit Sub
    End If
    If NEti <= 0 Then
      MsgBox("Nessun dato da stampare", vbCritical)
      Exit Sub
    End If
    If (cboR1.SelectedIndex = -1 And cboR2.SelectedIndex = -1) Then
      MsgBox("Nessun valore da stampare", vbCritical)
      Exit Sub
    End If
    Dim onlySel As Boolean = False
    If Me.DataGridView1.SelectedRows.Count > 0 Then
      If MsgBox("Vuoi stampare solo i " & Me.DataGridView1.SelectedRows.Count & " record selezionati ?", MsgBoxStyle.YesNo Or vbQuestion) = MsgBoxResult.Yes Then
        onlySel = True
      End If
    End If

    Dim saveFileNAme As String = ""
    Dim sfd As New SaveFileDialog
    sfd.Title = "Salva etichetta con nome..."
    sfd.Filter = "File PDF|*.pdf"

    If sfd.ShowDialog = Windows.Forms.DialogResult.Cancel Then
      Exit Sub
    End If
    saveFileNAme = sfd.FileName
    sfd.Dispose()

    CancellaFile(saveFileNAme)

    If IO.File.Exists(saveFileNAme) Then
      MsgBox("File etichette in uso!", vbCritical)
      Exit Sub
    End If

    Dim masterFile As String = System.IO.Path.Combine(Parametri.AppPath, "masterEtichetteA4.pdf")
    If Not IO.File.Exists(masterFile) Then
      MsgBox("File di stampa " + masterFile & " non trovato", vbCritical)
      Exit Sub
    End If


    Dim netixAt As Integer = Val(Me.TextBoxNEtixAtleta.Text)
    If netixAt <= 0 Then
      netixAt = 1
    Else
      netixAt = netixAt - 1
    End If

    Dim iniziaDa As Integer = Val(Val(txtDaRiga.Text))
    If iniziaDa <= 0 Then iniziaDa = 1

    Dim dtClonato As DataTable


    dtClonato = dt.Clone



    If onlySel Then

      For Each row As DataGridViewRow In Me.DataGridView1.SelectedRows
        Dim dr As DataRow = CType(row.DataBoundItem, DataRowView).Row

        dtClonato.ImportRow(dr)
      Next

    Else
      For ir As Integer = iniziaDa - 1 To dt.Rows.Count - 1
        Dim dr As DataRow = dt.Rows(ir)
        dtClonato.ImportRow(dr)
      Next
    End If





    If netixAt > 1 Then
      Dim arlOri As New ArrayList
      For Each dr As DataRow In dtClonato.Rows
        arlOri.Add(dr)
      Next
      For Each dr As DataRow In arlOri
        For i As Integer = 1 To netixAt
          dtClonato.ImportRow(dr)
        Next
      Next

      dtClonato.DefaultView.Sort = cboColOrd.Text

      dtClonato = dtClonato.DefaultView.ToTable
    End If

    Dim nPagine As Integer = dtClonato.Rows.Count \ NEti
    If dtClonato.Rows.Count Mod NEti <> 0 Then
      nPagine = nPagine + 1
    End If



    Dim sb As New System.Text.StringBuilder

    Dim arl As New ArrayList
    For ip As Integer = 1 To nPagine
      Dim tmpFileName As String = IO.Path.Combine(IO.Path.GetTempPath, "etip_" & ip & ".pdf")
      CancellaFile(tmpFileName)

      Dim _reader As New PdfReader(masterFile)
      Dim _Dest As New PdfStamper(_reader, New IO.FileStream(tmpFileName, IO.FileMode.CreateNew), PdfWriter.VERSION_1_6)


      Dim startIndex As Integer = (ip - 1) * NEti
      Dim endindex As Integer = startIndex + NEti - 1
      Dim cntCampi As Integer = 0
      For ieti = startIndex To endindex
        Dim dr As DataRow = Nothing
        If ieti < dtClonato.Rows.Count Then
          dr = dtClonato.Rows(ieti)

          If cboR1.SelectedIndex <> -1 Then
            Dim bcstr As String = Nz(dr(cboR1.Text), "")
            If bcstr <> "" Then
              Dim bc As New BarcodeQRCode(bcstr, 1, 1, Nothing)
              Dim img As iTextSharp.text.Image = bc.GetImage
              pdf_SetImmagineBottone(_Dest.AcroFields, "B_" & cntCampi, "", img)
            End If
          End If

          If Not IsNothing(Me.PictureBox1.Image) Then
            Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Me.PictureBox1.Image, Imaging.ImageFormat.Jpeg)
            pdf_SetImmagineBottone(_Dest.AcroFields, "logo_" & cntCampi, "", img)
          End If

          If cboR2.SelectedIndex <> -1 Then
            Dim bcstr As String = Nz(dr(cboR2.Text), "")
            If bcstr <> "" Then
              pdf_ImpostaCampo(_Dest.AcroFields, "R1_" & cntCampi, bcstr)
            End If
          End If

          If cboR3.SelectedIndex <> -1 Then
            Dim bcstr As String = Nz(dr(cboR3.Text), "")
            If bcstr <> "" Then
              pdf_ImpostaCampo(_Dest.AcroFields, "R2_" & cntCampi, bcstr)
            End If
          End If
        End If



        cntCampi = cntCampi + 1
      Next
      _Dest.FormFlattening = True
      _Dest.Close()
      _Dest.Dispose()

      _reader.Close()

      arl.Add(tmpFileName)
      sb.Append(" " & Chr(34) & tmpFileName & Chr(34))
    Next

    sb.Append(" cat output " & Chr(34) & saveFileNAme & Chr(34))

    'file1.pdf file2.pdf file3.pdf cat output newfile.pdf
    Dim pf As String = System.IO.Path.Combine(Parametri.AppPath, "pdftk", "pdftk.exe")

    If IO.File.Exists(pf) Then
      Dim si As New Diagnostics.ProcessStartInfo(pf, sb.ToString)
      si.WindowStyle = ProcessWindowStyle.Hidden
      Dim p As Process = Process.Start(si)
      p.WaitForExit()
      If IO.File.Exists(saveFileNAme) Then
        ApriFileProg(saveFileNAme)
      End If


    End If
    For Each ff As String In arl
      CancellaFile(ff)
    Next
  End Sub

  Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    Me.Enabled = False
    GeneraEtichette()
    Me.Enabled = True
    MsgBox("Operazione conclusa!", MsgBoxStyle.Information)
  End Sub

  Private Sub cboR1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboR1.SelectedIndexChanged
    Me.cboColOrd.SelectedIndex = Me.cboR1.SelectedIndex
  End Sub
End Class
