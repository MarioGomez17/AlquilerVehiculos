using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresAlquilador
    {
        public void GenerarReporteAlquileresAlquilador(int IdUsuario)
        {
            PdfWriter PdrWriter = new("./wwwroot/Reportes/DocumentosIniciales/ReporteAlquileresAlquilador.pdf");
            PdfDocument PDF = new(PdrWriter);
            Document Documento = new(PDF, new PageSize(1200, 600));
            Documento.SetMargins(60, 20, 55, 20);
            PdfFont FuenteColumnas = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont FuenteContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            string[] Columnas = ["#", "FECHA INICIO", "FECHA FIN", "PRECIO", "VEHÍCULO", "LUGAR", "MÉTODO DE PAGO", "ESTADO DEL PAGO", "SEGURO", "ESTADO"];
            float[] Tamanos = [1, 3, 3, 3, 8, 5, 5, 5, 7, 3];
            Table Tabla = new(UnitValue.CreatePercentArray(Tamanos));
            Tabla.SetWidth(UnitValue.CreatePercentValue(100));
            Tabla.SetMarginTop(80);
            foreach (string Columna in Columnas)
            {
                Tabla.AddHeaderCell(new Cell().Add(new Paragraph(Columna).SetFont(FuenteColumnas).SetTextAlignment(TextAlignment.CENTER)));
            }
            ModeloAlquilador ModeloAlquilador = new();
            if (!ModeloAlquilador.ValidarAlquilador(IdUsuario))
            {
                ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(IdUsuario);
                string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                    "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                    "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                    "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                    "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                    "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                    "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                    "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago, " +
                    "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                    "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler " +
                    "FROM alquiler_vehiculos.alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                    "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                    "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.seguro_alquiler " +
                    "ON alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = alquiler_vehiculos.alquiler.Seguro_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.metodo_pago " +
                    "ON alquiler_vehiculos.metodo_pago.Id_MetodoPago = alquiler_vehiculos.alquiler.MeotodoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                    "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.vehiculo " +
                    "ON alquiler_vehiculos.vehiculo.Id_Vehiculo = alquiler_vehiculos.alquiler.Vehiculo_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                    "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                    "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                    "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                    "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                    "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                    "WHERE alquiler_vehiculos.alquiler.Alquilador_Alquiler = " + ModeloAlquilador.Id + " " +
                    "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler DESC";
                MySqlConnection ConexionBD = ModeloConexion.Conect();
                try
                {
                    ConexionBD.Open();
                    MySqlCommand Comando = new(ConsultaSQL, ConexionBD);
                    MySqlDataReader Lector = Comando.ExecuteReader();
                    if (Lector.HasRows)
                    {
                        int Indice = 1;
                        while (Lector.Read())
                        {
                            Tabla.AddCell(new Cell().Add(new Paragraph(Indice.ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetFloat(2).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(3) + " " + Lector.GetString(4) + " " + Lector.GetString(5)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(6)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(7)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(8)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(9) + " (" + Lector.GetFloat(10).ToString() + ")").SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(11)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                            Indice++;
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception) {}
                finally
                {
                    ConexionBD.Close();
                }
                Documento.Add(Tabla);
                Documento.Close();
                var RutaArchivo = "D:/UNIVERSIDAD/WEB/ALQUILER_VEHICULOS/wwwroot/imagenes/Logo.png";
                var Logo = new Image(ImageDataFactory.Create(RutaArchivo)).SetWidth(80).SetHeight(80);
                var PLogo = new Paragraph("").Add(Logo);
                var Titulo = new Paragraph("REPORTE ALQUILERES ALQUILADOR");
                Titulo.SetTextAlignment(TextAlignment.CENTER);
                Titulo.SetFontSize(15);
                var Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                var Hora = DateTime.Now.ToString("hh:mm:ss");
                var FechaHora = new Paragraph("FECHA: " + Fecha + " Hora: " + Hora);
                FechaHora.SetFontSize(12);
                PdfDocument PDFDoc = new(new PdfReader("./wwwroot/Reportes/DocumentosIniciales/ReporteAlquileresAlquilador.pdf"), new PdfWriter("./wwwroot/Reportes/ReportesFinalizados/ReporteAlquileresAlquilador.pdf"));
                Document Doc = new(PDFDoc);
                int Numeros = PDFDoc.GetNumberOfPages();
                for (int i = 1; i <= Numeros; i++)
                {
                    float Y = PDFDoc.GetPage(i).GetPageSize().GetTop() - 15;
                    Doc.ShowTextAligned(PLogo, 80, Y, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    Doc.ShowTextAligned(Titulo, 350, Y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    Doc.ShowTextAligned(FechaHora, 670, Y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    Doc.ShowTextAligned(new Paragraph(string.Format("PÁGINA {0} DE {1}", i, Numeros)), PDFDoc.GetPage(i).GetPageSize().GetWidth() / 2, PDFDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                }
                Doc.Close();
            }
        }
    }
}