using System.Security.Cryptography;
using System.Text;

namespace GDifare.Portal.Humalab.Seguridad.Utils
{
    internal static class EncriptadorProvider
    {
        private readonly static byte[] mbytKey = new byte[8];
        private readonly static byte[] mbytIV = new byte[8];

        private static bool ConvertirClave(string clave)
        {
            try
            {
                byte[] bp = new byte[clave.Length - 1 + 1];
                var aEnc = new ASCIIEncoding();
                aEnc.GetBytes(clave, 0, clave.Length, bp, 0);

                var sha = new SHA1CryptoServiceProvider();
                byte[] bpHash = sha.ComputeHash(bp);

                int i;
                for (i = 0; i <= 7; i++)
                    mbytKey[i] = bpHash[i];
                for (i = 8; i <= 15; i++)
                    mbytIV[i - 8] = bpHash[i];

                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static string EncriptarValor(string valor)
        {
            string resultado;

            if (valor.Length > 92160)
            {
                resultado = "Error. Data String too large. Keep within 90Kb.";
                return resultado;
            }

            if (!ConvertirClave("Llave"))
            {
                resultado = "Error. Fail to generate key for encryption";
                return resultado;
            }

            valor = string.Format("{0,5:00000}" + valor, valor.Length);

            byte[] rbData = new byte[valor.Length - 1 + 1];
            var aEnc = new ASCIIEncoding();
            aEnc.GetBytes(valor, 0, valor.Length, rbData, 0);

            var descsp = new DESCryptoServiceProvider();

            var desEncrypt = descsp.CreateEncryptor(mbytKey, mbytIV);

            var mStream = new MemoryStream(rbData);
            var cs = new CryptoStream(mStream, desEncrypt, CryptoStreamMode.Read);
            var mOut = new MemoryStream();

            int bytesRead;
            byte[] output = new byte[1024];
            do
            {
                bytesRead = cs.Read(output, 0, 1024);
                if (!(bytesRead == 0))
                    mOut.Write(output, 0, bytesRead);
            }

            while (bytesRead > 0);

            if (mOut.Length == 0)
                resultado = "";
            else
                resultado = Convert.ToBase64String(mOut.GetBuffer(), 0, Convert.ToInt32(mOut.Length));
            return resultado;
        }

        internal static string DesencriptarValor(string valor)
        {
            string resultado;

            if (!(ConvertirClave("Llave")))
            {
                resultado = "Error. Fail to generate key for decryption";
                return resultado;
            }

            var descsp = new DESCryptoServiceProvider();
            var desDecrypt = descsp.CreateDecryptor(mbytKey, mbytIV);

            var mOut = new MemoryStream();
            var cs = new CryptoStream(mOut, desDecrypt, CryptoStreamMode.Write);
            byte[] bPlain;
            try
            {
                bPlain = Convert.FromBase64CharArray(valor.ToCharArray(), 0, valor.Length);
            }
            catch
            {
                resultado = "Error. Input Data is not base64 encoded.";
                return resultado;
            }

            long lRead = 0;
            long lTotal = valor.Length;

            try
            {
                while (lTotal >= lRead)
                {
                    cs.Write(bPlain, 0, bPlain.Length);
                    var lReadNow = Convert.ToInt64(((bPlain.Length / (double)descsp.BlockSize) * descsp.BlockSize));
                    lRead = lReadNow + lRead;
                }

                var aEnc = new ASCIIEncoding();
                resultado = aEnc.GetString(mOut.GetBuffer(), 0, Convert.ToInt32(mOut.Length));

                string strLen = resultado.Substring(0, 5);
                int nLen = Convert.ToInt32(strLen);
                resultado = resultado.Substring(5, nLen);
                var nReturn = Convert.ToInt32(mOut.Length);

                return resultado;
            }
            catch
            {
                resultado = "Error. Decryption Failed. Possibly due to incorrect Key or corrupted data";
            }

            return resultado;
        }
    }
}
