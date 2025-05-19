using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Directorio : Componente
    {
        private IList<Componente> _hijos;
        public Directorio(string nombre) : base(nombre)
        {
            Nombre = nombre;
            _hijos = new List<Componente>();
        }
        public override IList<Componente> Hijos
        {
            get
            {
                return _hijos.ToArray();
            }

        }
        public override void VaciarHijos()
        {
            _hijos = new List<Componente>();
        }
        public override void AgregarHijo(Componente c)
        {
            c.Padre = this;
            _hijos.Add(c);
        }
        public override int ObtenerTamaño()
        {
            return _hijos.Sum(h => h.ObtenerTamaño());
        }
    }
}
