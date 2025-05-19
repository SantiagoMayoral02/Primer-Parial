using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Archivo : Componente
    {
        public int Tamaño { get; set; }
        public Archivo(string nombre, int tamaño) : base(nombre)
        {
            Tamaño = tamaño;
        }
        public override IList<Componente> Hijos
        {
            get
            {
                return new List<Componente>();
            }

        }
        public override void AgregarHijo(Componente c)
        {

        }
        public override void VaciarHijos()
        {

        }
        public override int ObtenerTamaño()
        {
            return Tamaño;
        }
    }
}
