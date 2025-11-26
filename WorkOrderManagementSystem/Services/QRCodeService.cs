namespace WorkOrderManagementSystem.Services;

public class QRCodeService
{
    // Generate unique QR code identifier
    public string GenerateQRCode()
    {
        return $"ASSET-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";
    }
    
    // Validate QR code format
    public bool IsValidQRCode(string qrCode)
    {
        return !string.IsNullOrWhiteSpace(qrCode) && qrCode.StartsWith("ASSET-");
    }
    
    // Get QR code URL for display (using a free QR code API)
    public string GetQRCodeImageUrl(string qrCode)
    {
        // Using QR Server API (free, no API key needed)
        var encodedData = Uri.EscapeDataString(qrCode);
        return $"https://api.qrserver.com/v1/create-qr-code/?size=200x200&data={encodedData}";
    }
}
