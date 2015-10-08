Dim oXL, oAddin, oWk, Str, Str1, Fso, f2

Res = MsgBox("This will install 'DBC Data Export' plug-in into Excel, continue?", vbQuestion + vbYesNo + vbDefaultButton1, "Confirm")

Select Case Res
Case 6

        Set Fso = CreateObject("scripting.filesystemobject")
        Str = Left(wscript.scriptfullname, instrrev(wscript.scriptfullname, "\")) + "DBCExporter.xla"
        
        If Fso.FileExists(Str) = True Then
                Set oXL = CreateObject("Excel.Application")
                Set oWk = oXL.Workbooks.Add

                For Each oAddin In oXL.AddIns
                        If oAddin.Name = "DBCExporter" Then oAddin.Installed = False
                Next
                
                Str1 = oXL.UserLibraryPath & "DBCExporter.xla"
                If Str <> Str1 And Fso.FileExists(Str1) = True Then
                       Set f2 = Fso.GetFile(Str1)
                       f2.Delete
                End If
                
                
                Set oAddin = oXL.AddIns.Add(Str, True)
				oAddin.Installed = True
				oXL.Quit
                
                Set oXL = Nothing
                Set Fso = Nothing
                'Set oWk= Nothing

                MsgBox "The plug-in installed successfully! Please open your Excel program, check plug-in panel.", vbInformation, "Information"
        
        Else
                MsgBox "The plug-in is not exist! " + Str, vbCritical, "Error"
        End If

End Select
