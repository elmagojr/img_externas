namespace img_externas
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Utilidades ut = new Utilidades();
            From_sisc credecniales_ = ut.credenciales();

            try
            {
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.InitialDirectory = "c:\\";
                    fd.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    fd.FilterIndex = 2;
                    fd.RestoreDirectory = true;
                    if (fd.ShowDialog() == DialogResult.OK)
                    {

                        string filepth = fd.FileName;
                        Bitmap fot = new Bitmap(filepth);
                        byte[] byte_imagen;
                        if (fot.Size.Width > 400 && fot.Size.Height > 400)
                        {
                            byte_imagen = ut.ConvertirBitmap_tojpeg(fot, 400, 400, 50L);
                        }
                        else
                        {
                            byte_imagen = ut.CalidadBitmap_tojpeg(fot, 50L);
                        }

                        //insertar  
                        Dictionary<string, object> imagenDic = new Dictionary<string, object>
                        {
                            {"@tipo",  credecniales_.TIPO_TRANS},
                            {"@cta",  credecniales_.CTA},
                            {"@cia",  credecniales_.CIA},
                            {"@OBS",  credecniales_.OBS},
                            {"@imagen",  byte_imagen},
                        };
                        Querys sql = new Querys();
                        OD_BC con = new OD_BC();
                        int EXITO = con.EjecutarParametrizada(sql.sp_firmas, imagenDic);
               
                    }
                }
            }
            catch (Exception EX)
            {

                MessageBox.Show(EX.Message);
            }
            Application.Exit();
        }
    }
}
