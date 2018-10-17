using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace Capa_de_Presentacion
{
    public class CrearTicket
    {
        StringBuilder linea = new StringBuilder();
        int maxCar = 40, cortar;

        public String lineasGuion()
        {
            String lineasGuion = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasGuion += "-";
            }
            return linea.AppendLine(lineasGuion).ToString();
        }

        public String lineasAsteriscos()
        {
            String lineasAsteriscos = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasAsteriscos += "-";
            }
            return linea.AppendLine(lineasAsteriscos).ToString();
        }

        public String lineasIgual()
        {
            String lineasIgual = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasIgual += "-";
            }
            return linea.AppendLine(lineasIgual).ToString();
        }

        public void EncabezadoVenta()
        {
            linea.AppendLine("ARTICULO            |CANT|PRECIO|IMPORTE");
        }

        public void TextoIzquierda(String texto)
        {
            if (texto.Length>maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto-=maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual,maxCar));
                    caracterActual += maxCar;
                }
                linea.AppendLine(texto.Substring(caracterActual,texto.Length-caracterActual));
            }
            else
            {
                linea.AppendLine(texto);
            }
        }

        public void TextoDerecha(String texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }

                String espacios = "";
                for (int i = 0; i < (maxCar - texto.Substring(caracterActual,texto.Length-caracterActual).Length); i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                String espacios = "";
                for (int i = 0; i < (maxCar - texto.Length); i++)
                {
                    espacios += " ";
                }
                linea.AppendLine(espacios + texto);

            }
        }

        public void TextoCentro(String texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }

                String espacios = "";
                int centrar = (maxCar - texto.Substring(caracterActual,texto.Length-caracterActual).Length)/2;
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {

                String espacios = "";
                int centrar = (maxCar - texto.Length) / 2;
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";
                }

                linea.AppendLine(espacios + texto);

            }
        }      
        
        public void TextoExtremos(String textoIzquierdo,String textoDerecho)
        {
            String textoIzq, textoDer, textoCompleto = "", espacios = "";
            if (textoIzquierdo.Length > 18)
            {
                cortar = textoIzquierdo.Length - 18;
                textoIzq = textoIzquierdo.Remove(18,cortar);
            }
            else
            {
                textoIzq = textoIzquierdo;
            }

            textoCompleto = textoIzq;

            if (textoDerecho.Length>20)
            {
                cortar = textoDerecho.Length - 20;
                textoDer = textoDerecho.Remove(20, cortar);

            }
            else
            {
                textoDer = textoDerecho;
            }
            int nroEspacios = maxCar - (textoIzq.Length + textoDer.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + textoDerecho;
            linea.AppendLine(textoCompleto);

        }

        public void AgregarTotales(String texto,decimal total)
        {
            String resumen, valor, textoCompleto, espacios = "";
            if (texto.Length>25)
            {
                cortar = texto.Length - 25;
                resumen = texto.Remove(25,cortar);
            }
            else { resumen = texto; }

            textoCompleto = resumen;
            valor = total.ToString("#,#.00");


            int nroEspacios = maxCar - (resumen.Length+valor.Length);

            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + valor;
            linea.AppendLine(textoCompleto);
        }

        public void AgregarArticulo(String articulo, int cant, decimal precio, decimal importe)
        {
            if(cant.ToString().Length<= 5 && precio.ToString().Length <=7 && importe.ToString().Length <= 8)
            {
                String elemento = "", espacios = "";
                bool bandera = false;
                int nroEspacios = 0;
                if (articulo.Length > 20)
                {
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + precio.ToString();

                    nroEspacios = (8 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + importe.ToString();

                    int caracterActual = 0;
                    for (int longitudTexto = articulo.Length; longitudTexto > 20; longitudTexto -= 20)
                    {
                        if (bandera == false)
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 20) + elemento);
                            bandera = true;
                        }
                        else linea.AppendLine(articulo.Substring(caracterActual, 20));

                        caracterActual += 20;
                    }
                }
                else
                {
                    for (int i = 0; i < (20-articulo.Length); i++)
                    {
                        espacios += " ";
                    }
                    elemento = articulo + espacios;

                    nroEspacios = (5 -cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    nroEspacios = (7-precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + precio.ToString();

                    nroEspacios = (8-importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + importe.ToString();

                    linea.AppendLine(elemento);

                }







                }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportadas por este.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportadas por este.");
            
            }
        }

        public void CortaTicket()
        {
            linea.AppendLine("\x1B"+"m");
            linea.AppendLine("\x1B"+"d"+"\x09");
        }

        public void AbreCajon()
        {
            linea.AppendLine("\x1B"+"p"+"\x00"+"\x0F"+"\x96");
        }

        public void ImprimirTicket(String impresora)
        {
            RawPrinterHelper.SendStringToPrinter(impresora,linea.ToString());
            linea.Clear();
        }





    }
}
