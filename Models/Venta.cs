using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrimerTrabajoConAPI.Models
{
    public class Venta

    //Clase Venta
    //Cada objeto de Venta, tiene una lista de productos que son los que se vendieron en esa venta.
    {
        private string _comentario;
        private int _idVenta;
        private int _idUsuario;
        private List<ProductoVendido> _listaProductosVendidos = new List<ProductoVendido>();

        //Properties

        public string Comentario { get { return _comentario; } set { _comentario = value; } }
        public int Id { get { return _idVenta; } set { _idVenta = value; } }
        public int IdUsuario { get { return _idUsuario; } set { _idUsuario = value; } }
        public List <ProductoVendido> ListaProductosVendidos { get { return _listaProductosVendidos; } set { _listaProductosVendidos = value; } }

        //Constructor por defecto
        public Venta()
        {
            _comentario = String.Empty;
            _idVenta = 0;
            _idUsuario = 0;
            _listaProductosVendidos = new List<ProductoVendido>();
        }



        public Venta(int idUsuario, int idVenta, string comentario, List<ProductoVendido> listaProductosVendidos )
        //Constructor con toda la info
        {

            _idUsuario = idUsuario;
            _comentario = comentario;
            _idVenta = idVenta;
            _listaProductosVendidos = listaProductosVendidos;
        
        }
    }
}
