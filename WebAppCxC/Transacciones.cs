//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppCxC
{
    using System.ComponentModel.DataAnnotations;

    public partial class Transacciones
    {
        public int Id_transaccion { get; set; }
        [Display(Name = "Tipo de movimiento")]
        public string Tipo_movimiento { get; set; }
        [Display(Name = "Tipo de documento")]
        public int Id_tipoDocumento { get; set; }
        [Display(Name = "No. documento")]
        public int Numero_documento { get; set; }
        public System.DateTime Fecha { get; set; }
        [Display(Name = "No. Identificación")]
        public int Id_cliente { get; set; }
        public decimal Monto { get; set; }
    
        public virtual Clientes Clientes { get; set; }
        public virtual Tipo_Documentos Tipo_Documentos { get; set; }
    }
}
