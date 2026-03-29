param(
    [Parameter(Mandatory = $true)]
    [string]$NewName,

    [Parameter(Mandatory = $false)]
    [string]$NewDbContext = "AppDbContext",

    [Parameter(Mandatory = $false)]
    [string]$NewDbName = "AppDb"
)

$ErrorActionPreference = "Stop"

$OldName = "Gestion.Ganadera"
$OldDbContext = "AppDbContext"
$OldDbName = "AppDb"

foreach ($value in @($NewName, $NewDbContext, $NewDbName)) {
    if ($value.Contains("/") -or $value.Contains("\")) {
        throw "Los valores no deben contener separadores de ruta."
    }
}

Write-Host "[0/4] Eliminando carpeta .git si existe..."

if (Test-Path ".git") {
    Remove-Item ".git" -Recurse -Force
    Write-Host ".git eliminada."
} else {
    Write-Host "No existe carpeta .git."
}

Write-Host "[1/4] Reemplazando texto en archivos..."

$files = Get-ChildItem -Recurse -File | Where-Object {
    $_.FullName -notmatch "\\bin\\" -and
    $_.FullName -notmatch "\\obj\\"
}

foreach ($file in $files) {
    try {
        $content = Get-Content $file.FullName -Raw
        $updated = $content.Replace($OldName, $NewName).
                            Replace($OldDbContext, $NewDbContext).
                            Replace($OldDbName, $NewDbName)

        if ($updated -ne $content) {
            Set-Content -Path $file.FullName -Value $updated -NoNewline
        }
    }
    catch {
        Write-Warning "No se pudo procesar archivo: $($file.FullName)"
    }
}

Write-Host "[2/4] Renombrando archivos..."

Get-ChildItem -Recurse -File |
Sort-Object FullName -Descending |
ForEach-Object {

    $newFileName = $_.Name.Replace($OldName, $NewName).
                           Replace($OldDbContext, $NewDbContext).
                           Replace($OldDbName, $NewDbName)

    if ($newFileName -ne $_.Name) {
        Rename-Item -Path $_.FullName -NewName $newFileName
    }
}

Write-Host "[3/4] Renombrando carpetas..."

Get-ChildItem -Recurse -Directory |
Sort-Object FullName -Descending |
ForEach-Object {

    $newDirName = $_.Name.Replace($OldName, $NewName).
                          Replace($OldDbContext, $NewDbContext).
                          Replace($OldDbName, $NewDbName)

    if ($newDirName -ne $_.Name) {
        Rename-Item -Path $_.FullName -NewName $newDirName
    }
}

Write-Host "Proceso terminado."
