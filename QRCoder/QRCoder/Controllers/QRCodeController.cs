using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using SixLabors.ImageSharp.Formats.Jpeg;
using Image = SixLabors.ImageSharp.Image;
using System.Net;
using System.Net.Http.Headers;

namespace QRCodeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        // GET: api/<QRCodeController>
        [HttpGet]
        public IActionResult Get(string value)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("test", QRCodeGenerator.ECCLevel.Q);
            
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            BitmapByteQRCode byteQrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = byteQrCode.GetGraphic(20);

            if (qrCodeAsBitmapByteArr != null)
                return File(qrCodeAsBitmapByteArr, "application/octet-stream", "qrcode.png");

            return NoContent();
        }


        // POST api/<QRCodeController>
        [HttpPost]
        public IActionResult Post(QrCodeRequest request)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(request.Text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode byteQrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeAsBitmapByteArr = byteQrCode.GetGraphic(20);

            if (qrCodeAsBitmapByteArr != null)
                return File(qrCodeAsBitmapByteArr, "application/octet-stream", "qrcode.png");

            return NoContent();

        }

    }

    public class QrCodeRequest
    {
        public string Text { get; set; }
    }
}
