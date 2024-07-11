using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace img_externas
{
    internal class Querys
    {
        public readonly string sp_firmas = "CALL DBA.sp_firmas_digitales(p_tipo = ?,p_codigo_cta = ?,p_compania = ?, p_observacion=? ,p_firma = ?)";

    }
}
