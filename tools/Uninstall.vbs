addinFilename = "DBCExporter.xla"
addinName = "DBC Data Exporter"

result = MsgBox("This will uninstall the '"+addinName+"' Add-In in Microsoft Excel. Are you sure you want to continue?", vbQuestion + vbYesNo + vbDefaultButton1, "Uninstall " + addinName)

Select Case result
Case vbYes
	Set fso = CreateObject("Scripting.FileSystemObject")
	Set oShell = WScript.CreateObject("WScript.Shell") 
	addinSourcePath = oShell.CurrentDirectory + "\" + addinFilename
	
	If fso.FileExists(addinSourcePath) = True Then
		Set oExcel = CreateObject("Excel.Application")
		
		For Each oAddin In oExcel.AddIns
			If oAddin.Name = addinFilename Then
				oAddin.Installed = False
			End If
		Next
		
		addinInstallPath = oExcel.UserLibraryPath & addinFilename
		
		If fso.FileExists(addinInstallPath) Then
			fso.DeleteFile addinInstallPath, True ' force delete
		End If
		
		MsgBox "The Add-In was successfully uninstalled!", vbInformation, "Uninstallation Complete"
		
		oExcel.Quit
		Set oExcel = Nothing
		Set oWorkbooks = Nothing
		Set oAddin = Nothing
		Set fso = Nothing
		Set oShell = Nothing
	End If
End Select
