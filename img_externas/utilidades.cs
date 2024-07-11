using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_externas
{
    public class Utilidades
    {
        public byte[] ConvertirBitmap_tojpeg(Bitmap bitmapp, int width, int height, long calidad)
        {
            using (Bitmap Bitmap_reescalado = new Bitmap(bitmapp, new Size(width, height)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ImageCodecInfo ici = GetEncoder(ImageFormat.Jpeg);
                    EncoderParameters parametros = new EncoderParameters(1);
                    EncoderParameter parametro = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidad);
                    parametros.Param[0] = parametro;

                    Bitmap_reescalado.Save(ms, ici, parametros);
                    return ms.ToArray();
                }
            }
        }

        public byte[] CalidadBitmap_tojpeg(Bitmap bitmapp, long calidad)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                ImageCodecInfo ici = GetEncoder(ImageFormat.Jpeg);
                EncoderParameters parametros = new EncoderParameters(1);
                EncoderParameter parametro = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, calidad);
                parametros.Param[0] = parametro;

                bitmapp.Save(ms, ici, parametros);
                return ms.ToArray();
            }

        }

        public From_sisc credenciales()
        {
            From_sisc fs = new From_sisc();
            string path = "C:\\SISC\\Addons\\DFirmas\\ENLACE.txt";

            if (File.Exists(path))
            {
                string[] cred = File.ReadAllLines(path);
                if (cred.Length == 0)
                {
                    return null;
                }

                foreach (var item in cred)
                {
                    string[] partesTexto = item.Split(',');
                    foreach (var cre_entero in partesTexto)
                    {
                        string[] creds_ = cre_entero.Split(':');
                        switch (creds_[0].Trim())
                        {
                            case "CTA":
                                fs.CTA = creds_[1].Trim();
                                break;
                            case "CIA":
                                fs.CIA = creds_[1].Trim();
                                break;
                            case "ACCION":
                                fs.TIPO_TRANS = int.Parse(creds_[1].Trim());
                                break;
                            case "OBS":
                                fs.OBS = creds_[1].Trim();
                                break;

                            default:
                                break;
                        }
                    }



                }

            }





            return fs;
        }


        private ImageCodecInfo GetEncoder(ImageFormat formato)
        {
            //decodificar
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo decodec in codecs)
            {
                if (decodec.FormatID == formato.Guid)
                {
                    return decodec;
                }
            }
            return null;
        }
    }
}
