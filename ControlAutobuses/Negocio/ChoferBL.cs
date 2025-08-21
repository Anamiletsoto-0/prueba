using System;
using System.Collections.Generic;
using ControlAutobuses.Datos;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Negocio
{
    public class ChoferBL
    {
        private readonly ChoferRepository choferRepository;

        public ChoferBL()
        {
            choferRepository = new ChoferRepository();
        }

        public bool CrearChofer(Chofer chofer)
        {
            // Validaciones de negocio
            if (string.IsNullOrEmpty(chofer.Nombre) || string.IsNullOrEmpty(chofer.Apellido))
                throw new Exception("Nombre y apellido son requeridos");

            if (chofer.FechaNacimiento == DateTime.MinValue)
                throw new Exception("Fecha de nacimiento es requerida");

            if (string.IsNullOrEmpty(chofer.Cedula))
                throw new Exception("Cédula es requerida");

            // Validar edad mínima (21 años)
            int edad = DateTime.Now.Year - chofer.FechaNacimiento.Year;
            if (DateTime.Now.DayOfYear < chofer.FechaNacimiento.DayOfYear)
                edad--;

            if (edad < 21)
                throw new Exception("El chofer debe tener al menos 21 años");

            // Validar formato de cédula (ejemplo básico)
            if (chofer.Cedula.Length < 11)
                throw new Exception("La cédula debe tener un formato válido");

            return choferRepository.Crear(chofer);
        }

        public List<Chofer> ObtenerChoferes()
        {
            return choferRepository.ObtenerTodos();
        }

        public List<Chofer> ObtenerChoferesDisponibles()
        {
            return choferRepository.ObtenerDisponibles();
        }

        public bool ActualizarChofer(Chofer chofer)
        {
            // Validaciones similares a CrearChofer
            return choferRepository.Actualizar(chofer);
        }

        public bool EliminarChofer(int id)
        {
            // Verificar si el chofer tiene asignaciones activas
            if (choferRepository.TieneAsignacionesActivas(id))
                throw new Exception("No se puede eliminar el chofer porque tiene asignaciones activas");

            return choferRepository.Eliminar(id);
        }

        public Chofer ObtenerChoferPorId(int id)
        {
            return choferRepository.ObtenerPorId(id);
        }
    }
}