function Stop-ProcessesUsingPorts {
    param(
        [int[]] $Ports
    )

    foreach ($port in $Ports) {
        try {
            $connections = Get-NetTCPConnection -State Listen -ErrorAction Stop `
                | Where-Object { $_.LocalPort -eq $port } `
                | Select-Object -ExpandProperty OwningProcess -Unique
        }
        catch {
            Write-Warning "Unable to inspect port ${port}: $($_)"
            continue
        }

        foreach ($processId in $connections) {
            try {
                $process = Get-Process -Id $processId -ErrorAction Stop
                Write-Host "Stopping $($process.ProcessName) (PID $processId) listening on port $port..." -ForegroundColor DarkYellow
                Stop-Process -Id $processId -Force
            }
            catch {
                Write-Warning "Could not stop PID ${processId} for port ${port}: $($_)"
            }
        }
    }
}

$ErrorActionPreference = 'Stop'

Stop-ProcessesUsingPorts -Ports @(5173, 5174)

Write-Host "Launching backend (dotnet run)..." -ForegroundColor Cyan
$backend = Start-Process -FilePath "dotnet" `
    -ArgumentList "run", "--project", "FormsApp/FormsApp.csproj" `
    -WorkingDirectory $PSScriptRoot `
    -PassThru

Write-Host "Launching frontend (npm run dev)..." -ForegroundColor Cyan
$frontend = Start-Process -FilePath "npm" `
    -ArgumentList "run", "dev", "--prefix", "ClientApp" `
    -WorkingDirectory $PSScriptRoot `
    -PassThru

Start-Sleep -Seconds 2
try {
    Start-Process "http://localhost:5173/"
    Write-Host "Opened browser at http://localhost:5173/" -ForegroundColor Cyan
}
catch {
    Write-Warning "Could not open browser automatically: $_"
}

Write-Host ""
Write-Host "Backend PID: $($backend.Id)" -ForegroundColor Green
Write-Host "Frontend PID: $($frontend.Id)" -ForegroundColor Green
Write-Host "Close either process window or press Ctrl+C here to stop everything." -ForegroundColor Yellow

try {
    Wait-Process -Id @($backend.Id, $frontend.Id)
}
finally {
    foreach ($proc in @($backend, $frontend)) {
        if ($proc -and -not $proc.HasExited) {
            Write-Host "Stopping PID $($proc.Id)..." -ForegroundColor DarkYellow
            Stop-Process -Id $proc.Id -Force
        }
    }
}
