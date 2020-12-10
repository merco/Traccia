Namespace My
  ' Per MyApplication sono disponibili gli eventi seguenti:
  ' Startup: generato all'avvio dell'applicazione, prima della creazione del form di avvio.
  ' Shutdown: generato dopo la chiusura di tutti i form dell'applicazione. Questo evento non viene generato se l'applicazione termina in modo anomalo.
  ' UnhandledException: generato se nell'applicazione si verifica un'eccezione non gestita.
  ' StartupNextInstance: generato all'avvio di un'applicazione a istanza singola se l'applicazione è già attiva. 
  ' NetworkAvailabilityChanged: generato quando la connessione di rete viene connessa o disconnessa.
  Partial Friend Class MyApplication
    Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
      Logga("FINE")
    End Sub

    Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
      CaricaParametri()
      Logga("AVVIO")
      frmMain.Show()
    End Sub

    Private Sub MyApplication_UnhandledException(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
      MsgBox("ERRORE APPLICATIVO", MsgBoxStyle.Critical)
      MsgBox(e.Exception.Message)
      Logga("MyApplication_UnhandledException: " & e.Exception.Message)
      Logga(e.Exception.StackTrace)
      If Not IsNothing(e.Exception.InnerException) Then
        Logga(e.Exception.InnerException.Message)
      End If
      e.ExitApplication = True
    End Sub
  End Class
End Namespace
