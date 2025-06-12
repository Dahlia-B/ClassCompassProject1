# Quick Socket Server Verification
Write-Host "🚀 Starting ClassCompass with Socket Support..." -ForegroundColor Cyan

# Start the server in background
$job = Start-Job -ScriptBlock {
    Set-Location "C:\ClassCompassProject"
    dotnet run --urls=http://localhost:5004
}

Write-Host "⏳ Waiting for server to start..." -ForegroundColor Yellow
Start-Sleep -Seconds 10

# Test the socket endpoint
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5004/api/socket/status" -Method Get -TimeoutSec 5
    Write-Host "✅ Socket server is running!" -ForegroundColor Green
    Write-Host "   Status: $($response.status)" -ForegroundColor White
    Write-Host "   Connected Clients: $($response.connectedClients)" -ForegroundColor White
} catch {
    Write-Host "⚠️  Socket endpoint not ready yet. Manual check needed." -ForegroundColor Yellow
    Write-Host "   Error: $($_.Exception.Message)" -ForegroundColor Gray
}

# Stop the background job
Stop-Job $job
Remove-Job $job

Write-Host "`n🎯 Next steps:" -ForegroundColor Cyan
Write-Host "1. Run: dotnet run --urls=http://localhost:5004" -ForegroundColor White
Write-Host "2. Test: http://localhost:5004/api/socket/status" -ForegroundColor White
Write-Host "3. View Swagger: http://localhost:5004/swagger" -ForegroundColor White
